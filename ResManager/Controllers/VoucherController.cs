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
    public class VoucherController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        // GET: Voucher
        public ActionResult Index()
        {
            return View(db.C02_Voucher.ToList());
        }

        // GET: Voucher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02_Voucher c02_Voucher = db.C02_Voucher.Find(id);
            if (c02_Voucher == null)
            {
                return HttpNotFound();
            }
            return View(c02_Voucher);
        }

        // GET: Voucher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Voucher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,MaGiamGia,NgayHieuLuc,NgayHetHan,SoLuongKichHoat,SoLuongToiDa")] C02_Voucher c02_Voucher)
        {
            if (ModelState.IsValid)
            {
                c02_Voucher.SoLuongKichHoat = 0;
                db.C02_Voucher.Add(c02_Voucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c02_Voucher);
        }

        // GET: Voucher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02_Voucher c02_Voucher = db.C02_Voucher.Find(id);
            if (c02_Voucher == null)
            {
                return HttpNotFound();
            }
            return View(c02_Voucher);
        }

        // POST: Voucher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,MaGiamGia,NgayHieuLuc,NgayHetHan,SoLuongKichHoat,SoLuongToiDa")] C02_Voucher c02_Voucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c02_Voucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c02_Voucher);
        }

        // GET: Voucher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02_Voucher c02_Voucher = db.C02_Voucher.Find(id);
            if (c02_Voucher == null)
            {
                return HttpNotFound();
            }
            return View(c02_Voucher);
        }

        // POST: Voucher/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            C02_Voucher c02_Voucher = db.C02_Voucher.Find(id);
            db.C02_Voucher.Remove(c02_Voucher);
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
