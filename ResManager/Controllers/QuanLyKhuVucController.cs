using ResManager.BUS.Message;
using ResManager.BUS.QuanLyThongTin;
using ResManager.DAO.CommonModel;
using ResManager.DAO.Databases;
using ResManager.DAO.WebModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;

namespace ResManager.Controllers
{
    public class QuanLyKhuVucController : Controller
    {
        private string title = "Quản lý khu vực bàn ăn";
        // GET: QuanLyKhuVuc
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = title;
            List<string> lis = new List<string>();
            QuanLyKhuVuc quanLyKhuVuc = new QuanLyKhuVuc();
            List<KhuVucAn> lisKhuVucAn = new List<KhuVucAn>();

            try
            {
                lisKhuVucAn = quanLyKhuVuc.GetDanhSachKhuVuc();
            }
            catch (Exception ex)
            {
                lis.Add(ex.Message);
            }

            TempData["ErrorMessage"] = lis;

            return View(lisKhuVucAn);
        }

        [HttpGet]
        public ActionResult DatailArea(int? id)
        {
            ViewBag.Title = title;
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C00_KhuVucAn c00_KhuVucAn = db.C00_KhuVucAn.Find(id);
            if (c00_KhuVucAn == null)
            {
                return HttpNotFound();
            }
            return View(c00_KhuVucAn);
        }

        [HttpGet]
        public ActionResult AddNewAreaView()
        {
            ViewBag.Title = "Thêm khu vực bàn ăn";
            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(1);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai");

            List<string> lis = new List<string>();

            if (TempData["ErrorMessageRedirect"] != null)
            {
                List<string> message = TempData["ErrorMessageRedirect"] as List<string>;
                lis.AddRange(message);
            }

            TempData["ErrorMessage"] = lis;

            return View();
        }

        [HttpPost]
        public ActionResult AddNewArea(KhuVucAn khuVucAn)
        {
            List<string> lis = new List<string>();
            QuanLyKhuVuc quanLyKhuVuc = new QuanLyKhuVuc();

            if (string.IsNullOrEmpty(khuVucAn.TenLoaiKhuVuc) || string.IsNullOrWhiteSpace(khuVucAn.TenLoaiKhuVuc))
            {
                lis.Add(BusinessExceptionMessage.BU_TTNH_004);
            }

            if (!string.IsNullOrEmpty(khuVucAn.TenLoaiKhuVuc) && khuVucAn.TenLoaiKhuVuc.Length > 128)
            {
                lis.Add(BusinessExceptionMessage.BU_TTNH_005);
            }

            if (lis.Count > 0)
            {
                TempData["ErrorMessageRedirect"] = lis;
                return RedirectToAction("AddNewAreaView");
            }

            quanLyKhuVuc.AddKhuVucAn(khuVucAn);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditAreaView(int? id)
        {
            ViewBag.Title = title;
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C00_KhuVucAn c00_KhuVucAn = db.C00_KhuVucAn.Find(id);
            if (c00_KhuVucAn == null)
            {
                return HttpNotFound();
            }
            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(1);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai", c00_KhuVucAn.IdTrangThai);
            return View(c00_KhuVucAn);
        }

        [HttpPost]
        public ActionResult EditArea([Bind(Include = "Id,TenLoaiKhuVuc,NgayTao,IdTrangThai")] C00_KhuVucAn c00_KhuVucAn)
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            if (ModelState.IsValid)
            {
                c00_KhuVucAn.NgayTao = DateTime.Now;
                db.Entry(c00_KhuVucAn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteArea(int? id)
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            C00_KhuVucAn c00_KhuVucAn = db.C00_KhuVucAn.Find(id);
            db.C00_KhuVucAn.Remove(c00_KhuVucAn);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}