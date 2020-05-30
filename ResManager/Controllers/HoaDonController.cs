using Newtonsoft.Json;
using ResManager.DAO.Databases;
using ResManager.DAO.WebModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResManager.Controllers
{
    public class HoaDonController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: HoaDon
        public ActionResult ThanhToan(int? idBanAn)
        {
            return View(XuatHoaDon(idBanAn));
        }

        [HttpPost]
        public string LapHoaDon(int? idBanAn)
        {
            HoaDonViewModel hoaDonViewModel = XuatHoaDon(idBanAn);

            var checkExist = db.C02_LichSuDungBanAn.FirstOrDefault(ptr => ptr.IdBanAn == idBanAn && ptr.IsSuDung == true);

            if (checkExist != null)
            {
                try
                {
                    checkExist.IsSuDung = false;
                    C02_HoaDon c02_HoaDon = new C02_HoaDon()
                    {
                        IdLichSuBan = checkExist.Id,
                        MaHoaDon = DateTime.Now.ToString("ddMMyy"),
                        PhaiThu = hoaDonViewModel.TongPhaiThu,
                        ThucThu = hoaDonViewModel.TongPhaiThu,
                        ChoNo = hoaDonViewModel.TongDuNo
                    };
                    db.C02_HoaDon.Add(c02_HoaDon);

                    List<C02_ChiTietHoaDon> chiTietHoaDon = new List<C02_ChiTietHoaDon>();

                    var listMonDaGoi = db.C02_PhucVu.Where(ptr => ptr.IdLichSuDungBanAn == checkExist.Id).ToList();

                    foreach (var item in listMonDaGoi)
                    {
                        C02_ChiTietHoaDon c02_ChiTietHoaDon = new C02_ChiTietHoaDon()
                        {
                            IdHoaDon = c02_HoaDon.Id,
                            IdMonAn = item.IdMon,
                            DonGia = item.DonGia,
                            SoLuong = item.SoLuong,
                            DonVi = item.C01_Mon.DonVi,
                            GhiChu = "",
                        };
                        item.ThanhToan = true;
                        chiTietHoaDon.Add(c02_ChiTietHoaDon);
                    }

                    db.C02_ChiTietHoaDon.AddRange(chiTietHoaDon);

                    var banAn = db.C00_BanAn.First(ptr => ptr.Id == idBanAn);
                    banAn.IdTrangThai = 4;

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                return JsonConvert.SerializeObject("Lập hóa đơn thành công", Formatting.Indented);
            }

            return JsonConvert.SerializeObject("", Formatting.Indented);
        }

        public HoaDonViewModel XuatHoaDon(int? idBanAn)
        {
            var dsMonAnPhucVu = db.C02_PhucVu.Include(c => c.C01_Mon).Where(ptr => ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn && ptr.C02_LichSuDungBanAn.IsSuDung == true);

            List<MonDaGoi> dsMon = new List<MonDaGoi>();

            decimal? TongPhaiThu = 0;
            decimal? TongThucThu = 0;
            decimal? TongDuNo = 0;

            foreach (C02_PhucVu item in dsMonAnPhucVu)
            {
                MonDaGoi monDaGoi = new MonDaGoi()
                {
                    IdMon = item.IdMon,
                    TenMon = item.C01_Mon.TenMon,
                    DonGia = item.DonGia.Value.ToString(),
                    DonVi = item.C01_Mon.DonVi,
                    SoLuong = item.SoLuong.Value.ToString(),
                    ThanhTien = item.DonGia.Value * item.SoLuong.Value
                };
                dsMon.Add(monDaGoi);
                TongPhaiThu += (decimal)(int.Parse(monDaGoi.DonGia) * int.Parse(monDaGoi.SoLuong));
            }

            HoaDonViewModel hoaDonViewModel = new HoaDonViewModel()
            {
                BanAn = db.C00_BanAn.Where(ptr => ptr.Id == idBanAn).FirstOrDefault(),
                lisMonAn = dsMon,
                TongDuNo = TongDuNo,
                TongPhaiThu = TongPhaiThu,
                TongThucThu = TongThucThu,
                TrietKhau = 0,
                MaVoucher = "",
                NgayTao = DateTime.Now.ToString("dd/MM/yyyy"),
            };

            return hoaDonViewModel;
        }

        [HttpPost]
        public string SuDungVoucher(int? idBanAn, string VoucherCode)
        {
            HoaDonViewModel hoaDonViewModel = new HoaDonViewModel();
            hoaDonViewModel = this.XuatHoaDon(idBanAn);

            var checkVoucher = db.C02_Voucher.FirstOrDefault(ptr => ptr.MaGiamGia == VoucherCode);
            if (checkVoucher == null)
            {
                return JsonConvert.SerializeObject(new { code = 1, message = "Voucher không đúng!", record = new HoaDonViewModel() }, Formatting.Indented);
            }
            return JsonConvert.SerializeObject(new
            {
                code = 0,
                message = "",
                record = new HoaDonViewModel()
                {
                    TongDuNo = hoaDonViewModel.TongDuNo,
                    TongPhaiThu = hoaDonViewModel.TongPhaiThu,
                    TongThucThu = hoaDonViewModel.TongThucThu - checkVoucher.KhauTru - (checkVoucher.PhanTramKhauTru.Value / 100 * hoaDonViewModel.TongPhaiThu),
                    TrietKhau = checkVoucher.KhauTru + (checkVoucher.PhanTramKhauTru.Value/100 * hoaDonViewModel.TongPhaiThu),
                    MaVoucher = VoucherCode,
                    NgayTao = DateTime.Now.ToString("dd/MM/yyyy"),
                }
            }, Formatting.Indented);
        }
    }
}