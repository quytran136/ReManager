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
    public class HomeController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Tổng quan hoạt động";
            return View();
        }

        public string GetTinhHinhHoatDongTrongThang()
        {
            var getHoatDong = db.C02_HoaDon.Where(ptr =>
            ptr.C02_LichSuDungBanAn.NgayTao.Value.Day >= 1 &&
            ptr.C02_LichSuDungBanAn.NgayTao.Value.Month == DateTime.Now.Month &&
            ptr.C02_LichSuDungBanAn.NgayTao.Value.Year == DateTime.Now.Year &&
            ptr.C02_LichSuDungBanAn.NgayTao.Value.Day <= 30).ToList();

            if (getHoatDong.Count() > 0)
            {
                TinhHinhHoatDongTrongThang tinhHinhHoatDongTrongThang = new TinhHinhHoatDongTrongThang();
                foreach (var item in getHoatDong)
                {
                    tinhHinhHoatDongTrongThang.DoanhThuTrongThang.Add(new TinhHinhHoatDongTrongNgay()
                    {
                    });
                }
                return JsonConvert.SerializeObject(getHoatDong, Formatting.Indented);
            }
            return JsonConvert.SerializeObject("", Formatting.Indented); ;
        }

        public string GetTinhHinhHoatDongTrongNgay()
        {
            var getHoatDong = db.C02_HoaDon.Where(ptr => ptr.C02_LichSuDungBanAn.NgayTao.Value.ToString("dd/MM/yyyy").Equals(DateTime.Now.ToString("dd/MM/yyyy")));

            if (getHoatDong.Count() > 0)
            {
                return JsonConvert.SerializeObject(getHoatDong, Formatting.Indented);
            }
            return JsonConvert.SerializeObject("", Formatting.Indented); ;
        }

        public string TinhTrangPhucVu()
        {
            var getHoatDong = db.C00_KhuVucAn.Include(c => c.C00_BanAn);
            if (getHoatDong.Count() > 0)
            {
                return JsonConvert.SerializeObject(getHoatDong, Formatting.Indented);
            }
            return JsonConvert.SerializeObject("", Formatting.Indented); ;
        }
    }
}