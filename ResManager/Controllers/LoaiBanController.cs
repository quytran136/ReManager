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
    public class LoaiBanController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        private string title = "Quản lý loại bàn";
        // GET: LoaiBan
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
                var c00_LoaiBan = db.C00_LoaiBan.Include(c => c.Root_QuanLyTrangThai);
                return View(c00_LoaiBan.ToList());
            }
            catch (Exception ex)
            {
                lis.Add(ex.Message);
            }
            TempData["ErrorMessage"] = lis;
            return View(new List<C00_LoaiBan>());
        }

        // GET: LoaiBan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C00_LoaiBan c00_LoaiBan = db.C00_LoaiBan.Find(id);
            if (c00_LoaiBan == null)
            {
                return HttpNotFound();
            }
            return View(c00_LoaiBan);
        }

        // GET: LoaiBan/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm loại bàn";
            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(6);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai");
            return View();
        }

        // POST: LoaiBan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,TenLoaiBan,NgayTao,IdTrangThai")] C00_LoaiBan c00_LoaiBan)
        {
            ViewBag.Title = "Thêm bàn ăn";
            if (ModelState.IsValid)
            {
                c00_LoaiBan.NgayTao = DateTime.Now;
                db.C00_LoaiBan.Add(c00_LoaiBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(2);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai", c00_LoaiBan.IdTrangThai);
            return View(c00_LoaiBan);
        }

        // GET: LoaiBan/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = title;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C00_LoaiBan c00_LoaiBan = db.C00_LoaiBan.Find(id);
            if (c00_LoaiBan == null)
            {
                return HttpNotFound();
            }

            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(6);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai", c00_LoaiBan.IdTrangThai);
            return View(c00_LoaiBan);
        }

        // POST: LoaiBan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,TenLoaiBan,NgayTao,IdTrangThai")] C00_LoaiBan c00_LoaiBan)
        {
            if (ModelState.IsValid)
            {
                c00_LoaiBan.NgayTao = DateTime.Now;
                db.Entry(c00_LoaiBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(2);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai", c00_LoaiBan.IdTrangThai);
            return View(c00_LoaiBan);
        }

        // GET: LoaiBan/Delete/5
        public ActionResult Delete(int? id)
        {
            List<string> lis = new List<string>();
            C00_LoaiBan c00_LoaiBan = db.C00_LoaiBan.Find(id);
            db.C00_LoaiBan.Remove(c00_LoaiBan);
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
