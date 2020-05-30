using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ResManager.DAO.Databases;
using ResManager.DAO.WebModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ResManager.Controllers
{
    public class DatBanController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        private string title = "Hoạt động nhà hàng";

        // GET: DatBan
        public ActionResult Index()
        {
            List<string> lis = new List<string>();
            if (TempData["ErrorMessageRedirect"] != null)
            {
                List<string> message = TempData["ErrorMessageRedirect"] as List<string>;
                lis.AddRange(message);
            }
            ViewBag.Title = title;

            try
            {
                var c00_BanAn = db.C00_BanAn.Include(c => c.C00_LoaiBan).Include(c => c.C00_KhuVucAn).Include(c => c.Root_QuanLyTrangThai);
                return View(c00_BanAn.ToList());
            }
            catch (Exception ex)
            {
                lis.Add(ex.Message);
            }
            TempData["ErrorMessage"] = lis;
            return View(new List<C00_BanAn>());
        }

        public ActionResult SuDung(int? id)
        {
            var BanAn = db.C00_BanAn.FirstOrDefault(ptr => ptr.Id == id);
            if (BanAn.IdTrangThai == 1)
            {
                BanAn.IdTrangThai = 4;
                db.SaveChanges();
            }
            DatBanViewModel datBanViewModel = new DatBanViewModel()
            {
                idTrangThai = BanAn.IdTrangThai,
                idBanAn = id,
                lisMonSuDung = db.C02_PhucVu.Include(ptr => ptr.C01_Mon).Where(ptr => ptr.C02_LichSuDungBanAn.IdBanAn == id && ptr.C02_LichSuDungBanAn.IsSuDung == true).Select(ptr => new MonDaGoi()
                {
                    TenMon = ptr.C01_Mon.TenMon,
                    DonGia = ptr.DonGia.ToString(),
                    DonVi = ptr.C01_Mon.DonVi,
                    SoLuong = ptr.SoLuong.ToString()
                }).ToList(),
                lisMenu = db.C01_Menu.ToList(),
                lisMonAn = db.C01_Mon.ToList(),
            };
            return View(datBanViewModel);
        }

        [HttpPost]
        public string MonDaGoi(int? idBanAn)
        {
            List<MonDaGoi> lisMonDaGoi = new List<MonDaGoi>();
            lisMonDaGoi = db.C02_PhucVu.Include(ptr => ptr.C01_Mon).Where(ptr =>
                ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn &&
                ptr.C02_LichSuDungBanAn.IsSuDung == true &&
                ptr.ThanhToan == false).Select(ptr => new MonDaGoi()
                {
                    IdMon = ptr.IdMon,
                    TenMon = ptr.C01_Mon.TenMon,
                    DonGia = ptr.DonGia.Value.ToString(),
                    DonVi = ptr.C01_Mon.DonVi,
                    SoLuong = ptr.SoLuong.Value.ToString()
                }).ToList();
            return JsonConvert.SerializeObject(lisMonDaGoi, Formatting.Indented);
        }

        [HttpPost]
        public string GoiMon(int? idMon, int? idBanAn)
        {
            var checkTable = db.C00_BanAn.FirstOrDefault(ptr => ptr.Id == idBanAn);

            if (checkTable.IdTrangThai != 2 && checkTable.IdTrangThai != 5)
            {
                return "";
            }

            var checkExist = db.C02_PhucVu.Where(ptr => ptr.IdMon == idMon && ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn && ptr.C02_LichSuDungBanAn.IsSuDung == true && ptr.ThanhToan == false);

            if (checkExist.Count() > 0)
            {
                checkExist.FirstOrDefault().SoLuong += 1;
                db.SaveChanges();
            }
            else
            {
                C02_PhucVu phucVu = new C02_PhucVu()
                {
                    IdMon = idMon,
                    IdLichSuDungBanAn = db.C02_LichSuDungBanAn.FirstOrDefault(ptr => ptr.IdBanAn == idBanAn && ptr.IsSuDung == true).Id,
                    NgayTao = DateTime.Now,
                    ThanhToan = false,
                    DonGia = db.C01_Mon.Where(ptr => ptr.Id == idMon).FirstOrDefault().DonGia.Value,
                    SoLuong = 1,
                };
                db.C02_PhucVu.Add(phucVu);
                db.SaveChanges();
            }

            return JsonConvert.SerializeObject(lisMonDaGoi(idBanAn), Formatting.Indented);
        }

        [HttpPost]
        public string HuyMon(int? idMon, int? idBanAn)
        {
            var checkExist = db.C02_PhucVu.Include(ptr => ptr.C02_LichSuDungBanAn).
                FirstOrDefault(ptr =>
                    ptr.IdMon == idMon &&
                    ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn &&
                    ptr.C02_LichSuDungBanAn.IsSuDung == true &&
                    ptr.ThanhToan == false
                );

            if (checkExist != null)
            {
                if (checkExist.SoLuong == 1)
                {
                    db.C02_PhucVu.Remove(checkExist);
                }
                else
                {
                    checkExist.SoLuong -= 1;
                }
                db.SaveChanges();
            }

            return JsonConvert.SerializeObject(lisMonDaGoi(idBanAn), Formatting.Indented);
        }

        [HttpPost]
        public string ThanhToan(int? idMon, int? idBanAn)
        {
            var checkExist = db.C02_PhucVu.Where(ptr =>
                ptr.IdMon == idMon &&
                ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn &&
                ptr.C02_LichSuDungBanAn.IsSuDung == true &&
                ptr.ThanhToan == false
            );

            if (checkExist.Count() > 0)
            {
                checkExist.ForEach(ptr => ptr.ThanhToan = true);
                decimal PhaiThu = checkExist.Sum(ptr => ptr.DonGia * ptr.SoLuong).Value;
                C02_HoaDon hoaDon = new C02_HoaDon()
                {
                    ChoNo = 0,
                    IdLichSuBan = checkExist.FirstOrDefault().IdLichSuDungBanAn,
                    PhaiThu = PhaiThu,
                    ThucThu = PhaiThu
                };
                db.C02_HoaDon.Add(hoaDon);

                C02_ChiTietHoaDon chiTietHoaDon = new C02_ChiTietHoaDon()
                {
                    IdHoaDon = hoaDon.Id,
                    IdMonAn = checkExist.FirstOrDefault().IdMon,
                    DonGia = checkExist.FirstOrDefault().DonGia,
                    SoLuong = checkExist.FirstOrDefault().SoLuong,
                    DonVi = checkExist.FirstOrDefault().C01_Mon.DonVi
                };

                db.SaveChanges();
            }

            return JsonConvert.SerializeObject(lisMonDaGoi(idBanAn), Formatting.Indented);
        }

        [HttpPost]
        public string DanhSachMonDaThanhToan(int? idBanAn)
        {
            C02_LichSuDungBanAn lichSuDungBanAn = db.C02_LichSuDungBanAn.Where(ptr => ptr.IsSuDung == true && ptr.IdBanAn == idBanAn).ToList().LastOrDefault();

            if (lichSuDungBanAn == null)
            {
                return JsonConvert.SerializeObject(new List<MonDaGoi>(), Formatting.Indented);
            }

            List<MonDaGoi> lisMonDaGoi = new List<MonDaGoi>();
            lisMonDaGoi = db.C02_PhucVu.Include(ptr => ptr.C01_Mon).Where(ptr =>
                ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn &&
                ptr.C02_LichSuDungBanAn.Id == lichSuDungBanAn.Id &&
                ptr.ThanhToan == true
            ).Select(ptr => new MonDaGoi()
            {
                IdMon = ptr.IdMon,
                TenMon = ptr.C01_Mon.TenMon,
                DonGia = ptr.DonGia.Value.ToString(),
                DonVi = ptr.C01_Mon.DonVi,
                SoLuong = ptr.SoLuong.Value.ToString()
            }).ToList();
            return JsonConvert.SerializeObject(lisMonDaThanhToan(idBanAn), Formatting.Indented);
        }

        private List<MonDaGoi> lisMonDaThanhToan(int? idBanAn)
        {
            C02_LichSuDungBanAn lichSuDungBanAn = db.C02_LichSuDungBanAn.Where(ptr => ptr.IsSuDung == true && ptr.IdBanAn == idBanAn).ToList().LastOrDefault();

            if (lichSuDungBanAn == null)
            {
                return new List<MonDaGoi>();
            }

            List<MonDaGoi> lisMonDaGoi = new List<MonDaGoi>();
            lisMonDaGoi = db.C02_PhucVu.Include(ptr => ptr.C01_Mon).Where(ptr =>
                ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn &&
                ptr.C02_LichSuDungBanAn.Id == lichSuDungBanAn.Id &&
                ptr.ThanhToan == true
            ).Select(ptr => new MonDaGoi()
            {
                IdMon = ptr.IdMon,
                TenMon = ptr.C01_Mon.TenMon,
                DonGia = ptr.DonGia.Value.ToString(),
                DonVi = ptr.C01_Mon.DonVi,
                SoLuong = ptr.SoLuong.Value.ToString()
            }).ToList();
            return lisMonDaGoi;
        }

        private List<MonDaGoi> lisMonDaGoi(int? idBanAn)
        {
            List<MonDaGoi> lisMonDaGoi = new List<MonDaGoi>();
            lisMonDaGoi = db.C02_PhucVu.Include(ptr => ptr.C01_Mon).Where(ptr =>
                ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn &&
                ptr.C02_LichSuDungBanAn.IsSuDung == true &&
                ptr.ThanhToan == false).Select(ptr => new MonDaGoi()
                {
                    IdMon = ptr.IdMon,
                    TenMon = ptr.C01_Mon.TenMon,
                    DonGia = ptr.DonGia.Value.ToString(),
                    DonVi = ptr.C01_Mon.DonVi,
                    SoLuong = ptr.SoLuong.Value.ToString()
                }).ToList();
            return lisMonDaGoi;
        }

        public void SuDungBan(int? idBanAn)
        {
            var checkTable = db.C00_BanAn.FirstOrDefault(ptr => ptr.Id == idBanAn);

            if (checkTable.IdTrangThai == 4 || checkTable.IdTrangThai == 5 || checkTable.IdTrangThai == 1)
            {
                checkTable.IdTrangThai = 2;
                C02_LichSuDungBanAn c02_LichSuDungBanAn = new C02_LichSuDungBanAn()
                {
                    IdBanAn = idBanAn,
                    IsSuDung = true,
                    NgayTao = DateTime.Now
                };
                db.C02_LichSuDungBanAn.Add(c02_LichSuDungBanAn);
                db.SaveChanges();
            }
        }

        public void DatTruoc(int? idBanAn)
        {
            var checkTable = db.C00_BanAn.FirstOrDefault(ptr => ptr.Id == idBanAn);

            if (checkTable.IdTrangThai == 4)
            {
                checkTable.IdTrangThai = 5;
                C02_LichSuDungBanAn c02_LichSuDungBanAn = new C02_LichSuDungBanAn()
                {
                    IdBanAn = idBanAn,
                    IsSuDung = true,
                    NgayTao = DateTime.Now
                };
                db.C02_LichSuDungBanAn.Add(c02_LichSuDungBanAn);
                db.SaveChanges();
            }
        }

        public void HuyBan(int? idBanAn)
        {
            var checkCancel = db.C02_PhucVu.Where(ptr => ptr.C02_LichSuDungBanAn.IdBanAn == idBanAn && ptr.C02_LichSuDungBanAn.IsSuDung == true && ptr.ThanhToan == false).ToList();

            if (checkCancel.Count() > 0)
            {
                db.C02_PhucVu.RemoveRange(checkCancel);
            }

            db.C02_LichSuDungBanAn.Where(ptr => ptr.IdBanAn == idBanAn).ForEach(ptr => ptr.IsSuDung = false);
            var checkTable = db.C00_BanAn.FirstOrDefault(ptr => ptr.Id == idBanAn);
            checkTable.IdTrangThai = 4;
            db.SaveChanges();
        }

        public ActionResult HuyBanA(int? id)
        {
            HuyBan(id);
            return RedirectToAction("index");
        }
    }
}