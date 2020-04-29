using ResManager.BUS.QuanLyThongTin;
using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ResManager.BUS.Message;

namespace ResManager.Controllers
{
    public class ThongTinNhaHangController : Controller
    {
        // GET: ThongTinNhaHang
        public ActionResult Index()
        {
            List<string> lis = new List<string>();
            ViewBag.Title = "Thông tin nhà hàng";
            ThongTinNhaHang thongTinNhaHang = new ThongTinNhaHang();
            C00_NhaHang nhaHang = new C00_NhaHang();

            if (TempData["ErrorMessageRedirect"] != null)
            {
                lis.AddRange(TempData["ErrorMessageRedirect"] as List<string>);
            }

            try
            {
                nhaHang = thongTinNhaHang.GetThongTinNhaHang();
            }
            catch (Exception ex)
            {
                lis.Add(ex.Message);
            }

            if (lis.Count == 0)
            {
                ViewBag.TenNhaHang = nhaHang.TenNhaHang;
                ViewBag.ThongTinMoTa = nhaHang.DiaChi;
            }

            TempData["ErrorMessage"] = lis;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(string txtTenNhaHang, string txtThongTinMoTa)
        {
            List<string> lis = new List<string>();

            if (txtTenNhaHang == string.Empty || txtTenNhaHang == null)
            {
                txtTenNhaHang = string.Empty;
                lis.Add(BusinessExceptionMessage.BU_TTNH_001);
            }

            if (txtThongTinMoTa == string.Empty || txtThongTinMoTa == null)
            {
                txtThongTinMoTa = string.Empty;
            }

            if (txtTenNhaHang.Length > 128)
            {
                lis.Add(BusinessExceptionMessage.BU_TTNH_002);
            }

            if (txtThongTinMoTa.Length > 256)
            {
                lis.Add(BusinessExceptionMessage.BU_TTNH_003);
            }

            if (lis.Count == 0)
            {
                C00_NhaHang nhaHang = new C00_NhaHang()
                {
                    TenNhaHang = txtTenNhaHang,
                    DiaChi = txtThongTinMoTa
                };

                ThongTinNhaHang thongTinNhaHang = new ThongTinNhaHang();
                thongTinNhaHang.SaveThongTinNhaHang(nhaHang);
            }

            TempData["ErrorMessageRedirect"] = lis;
            return RedirectToAction("index");
        }
    }
}