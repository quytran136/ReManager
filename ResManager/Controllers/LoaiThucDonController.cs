using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ResManager.DAO.Databases;

namespace ResManager.Controllers
{
    public class LoaiThucDonController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        // GET: LoaiThucDon
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý loại thực đơn";
            return View(db.C01_LoaiThucDon.ToList());
        }

        // GET: LoaiThucDon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_LoaiThucDon c01_LoaiThucDon = db.C01_LoaiThucDon.Find(id);
            if (c01_LoaiThucDon == null)
            {
                return HttpNotFound();
            }
            return View(c01_LoaiThucDon);
        }

        // GET: LoaiThucDon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiThucDon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,TenLoai")] C01_LoaiThucDon c01_LoaiThucDon)
        {
            if (ModelState.IsValid)
            {
                db.C01_LoaiThucDon.Add(c01_LoaiThucDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c01_LoaiThucDon);
        }

        // GET: LoaiThucDon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_LoaiThucDon c01_LoaiThucDon = db.C01_LoaiThucDon.Find(id);
            if (c01_LoaiThucDon == null)
            {
                return HttpNotFound();
            }
            return View(c01_LoaiThucDon);
        }

        // POST: LoaiThucDon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,TenLoai")] C01_LoaiThucDon c01_LoaiThucDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c01_LoaiThucDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c01_LoaiThucDon);
        }

        // GET: LoaiThucDon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_LoaiThucDon c01_LoaiThucDon = db.C01_LoaiThucDon.Find(id);
            if (c01_LoaiThucDon == null)
            {
                return HttpNotFound();
            }
            return View(c01_LoaiThucDon);
        }

        // POST: LoaiThucDon/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            C01_LoaiThucDon c01_LoaiThucDon = db.C01_LoaiThucDon.Find(id);
            db.C01_LoaiThucDon.Remove(c01_LoaiThucDon);
            db.SaveChanges();
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
