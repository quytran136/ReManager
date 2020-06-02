using ResManager.DAO.Databases;
using ResManager.DAO.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResManager.Controllers
{
    public class BaoCaoController : Controller
    {
        // GET: BaoCao
        public ActionResult BaoCaoDoanhThu()
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

            Decimal tongDuNo = 0;
            Decimal tongThu = 0;

            List<C02_LichSuDungBanAn> lisPhucVu = db.C02_LichSuDungBanAn.Where(ptr => ptr.NgayTao.Value.Month == DateTime.Now.Month).ToList();
            List<C02_HoaDon> HoaDon = db.C02_HoaDon.Where(ptr => ptr.C02_LichSuDungBanAn.NgayTao.Value.Month == DateTime.Now.Month).ToList();
            if (HoaDon != null)
            {
                tongDuNo = HoaDon.Sum(ptr => ptr.ChoNo).Value;
                tongThu = HoaDon.Sum(ptr => ptr.ThucThu).Value;
            }

            List<BaoCaoHoaDon> baoCaoHoaDon = new List<BaoCaoHoaDon>();
            baoCaoHoaDon = (from a in db.C02_LichSuDungBanAn
                            where a.NgayTao.Value.Month == DateTime.Now.Month
                            select new BaoCaoHoaDon
                            {
                                DoanhSo = db.C02_HoaDon.Where(ptr => ptr.IdLichSuBan == a.Id).Count(),
                                Ngay = a.NgayTao,
                                TongDuNo = db.C02_HoaDon.Where(ptr => ptr.IdLichSuBan == a.Id).Sum(ptr => ptr.ChoNo).Value,
                                TongThu = db.C02_HoaDon.Where(ptr => ptr.IdLichSuBan == a.Id).Sum(ptr => ptr.ThucThu).Value
                            }).ToList();


            BaoCaoDoanhThu baoCaoDoanhThu = new BaoCaoDoanhThu()
            {
                Thang = DateTime.Now.Month,
                DoanhSo = lisPhucVu.Count,
                TongDuNo = tongDuNo,
                TongThu = tongThu,
                baoCaoHoaDon = baoCaoHoaDon
            };

            return View(baoCaoDoanhThu);
        }
    }
}