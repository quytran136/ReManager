using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResManager.BUS.QuanLyThongTin;
using ResManager.DAO.CommonModel;
using ResManager.DAO.Databases;

namespace ResManager.Controllers
{
    public class BanAnController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        private string title = "Quản lý bàn ăn";

        // GET: BanAn
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

        // GET: BanAn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C00_BanAn c00_BanAn = db.C00_BanAn.Find(id);
            if (c00_BanAn == null)
            {
                return HttpNotFound();
            }
            return View(c00_BanAn);
        }

        // GET: BanAn/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm bàn ăn";
            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(2);
            ViewBag.IdLoaiBan = new SelectList(db.C00_LoaiBan, "Id", "TenLoaiBan");
            ViewBag.IdLoaiKhuVuc = new SelectList(db.C00_KhuVucAn, "Id", "TenLoaiKhuVuc");
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai");
            return View();
        }

        // POST: BanAn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,TenBanAn,SoBanAn,SoGhe,IdLoaiBan,IdLoaiKhuVuc,IdTrangThai,NgayTao")] C00_BanAn c00_BanAn)
        {
            ViewBag.Title = "Thêm bàn ăn";
            if (ModelState.IsValid)
            {
                c00_BanAn.NgayTao = DateTime.Now;
                db.C00_BanAn.Add(c00_BanAn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(2);
            ViewBag.IdLoaiBan = new SelectList(db.C00_LoaiBan.Where(ptr => ptr.Root_QuanLyTrangThai.IdTrangThai == 2), "Id", "TenLoaiBan", c00_BanAn.IdLoaiBan);
            ViewBag.IdLoaiKhuVuc =  new SelectList(db.C00_KhuVucAn.Where(ptr => ptr.Root_QuanLyTrangThai.IdTrangThai == 2), "Id", "TenLoaiKhuVuc", c00_BanAn.IdLoaiKhuVuc);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai", c00_BanAn.IdTrangThai);
            return View(c00_BanAn);
        }

        // GET: BanAn/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = title;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C00_BanAn c00_BanAn = db.C00_BanAn.Find(id);
            if (c00_BanAn == null)
            {
                return HttpNotFound();
            }

            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(2);
            ViewBag.IdLoaiBan = new SelectList(db.C00_LoaiBan.Where(ptr => ptr.Root_QuanLyTrangThai.IdTrangThai == 2), "Id", "TenLoaiBan", c00_BanAn.IdLoaiBan);
            ViewBag.IdLoaiKhuVuc =  new SelectList(db.C00_KhuVucAn.Where(ptr => ptr.Root_QuanLyTrangThai.IdTrangThai == 2), "Id", "TenLoaiKhuVuc", c00_BanAn.IdLoaiKhuVuc);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai", c00_BanAn.IdTrangThai);
            return View(c00_BanAn);
        }

        // POST: BanAn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,TenBanAn,SoBanAn,SoGhe,IdLoaiBan,IdLoaiKhuVuc,IdTrangThai,NgayTao")] C00_BanAn c00_BanAn)
        {
            ViewBag.Title = title;
            if (ModelState.IsValid)
            {
                c00_BanAn.NgayTao = DateTime.Now;
                db.Entry(c00_BanAn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(2);
            ViewBag.IdLoaiBan = new SelectList(db.C00_LoaiBan.Where(ptr => ptr.Root_QuanLyTrangThai.IdTrangThai == 2), "Id", "TenLoaiBan", c00_BanAn.IdLoaiBan);
            ViewBag.IdLoaiKhuVuc = new SelectList(db.C00_KhuVucAn.Where(ptr => ptr.Root_QuanLyTrangThai.IdTrangThai == 2), "Id", "TenLoaiKhuVuc", c00_BanAn.IdLoaiKhuVuc);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai", c00_BanAn.IdTrangThai);
            return View(c00_BanAn);
        }

        // GET: BanAn/Delete/5
        public ActionResult Delete(int? id)
        {
            List<string> lis = new List<string>();
            C00_BanAn c00_BanAn = db.C00_BanAn.Find(id);
            db.C00_BanAn.Remove(c00_BanAn);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                lis.Add(ex.Message);
            }
            TempData["ErrorMessage"] = lis;
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
