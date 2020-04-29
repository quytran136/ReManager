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
    public class CongThucController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        // GET: CongThuc
        public ActionResult Index(int? id)
        {
            int? idMonx = id;
            if (TempData["IdMon"] != null)
            {
                if (int.TryParse(TempData["IdMon"].ToString(), out int x))
                {
                    idMonx = x;
                }
            }
            else
            {
                TempData["IdMon"] = id;
            }
            var c01_CongThuc = db.C01_CongThuc.Where(p => p.IdMon == idMonx).Include(c => c.C01_Mon);
            return View(c01_CongThuc.ToList());
        }

        // GET: CongThuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_CongThuc c01_CongThuc = db.C01_CongThuc.Find(id);
            if (c01_CongThuc == null)
            {
                return HttpNotFound();
            }
            return View(c01_CongThuc);
        }

        // GET: CongThuc/Create
        public ActionResult Create()
        {
            TempData["IdMon"] = TempData["IdMon"];
            return View();
        }

        // POST: CongThuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,IdMon,GioiThieu,ChiTietMon")] C01_CongThuc c01_CongThuc)
        {
            if (ModelState.IsValid)
            {
                if (int.TryParse(TempData["IdMon"].ToString(), out int x))
                {
                    c01_CongThuc.IdMon = x;
                }
                c01_CongThuc.NgayTao = DateTime.Now;
                c01_CongThuc.SuaLanCuoi = DateTime.Now;
                db.C01_CongThuc.Add(c01_CongThuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(c01_CongThuc);
        }

        // GET: CongThuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_CongThuc c01_CongThuc = db.C01_CongThuc.Find(id);
            if (c01_CongThuc == null)
            {
                return HttpNotFound();
            }
            return View(c01_CongThuc);
        }

        // POST: CongThuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,GioiThieu,ChiTietMon")] C01_CongThuc c01_CongThuc)
        {
            if (ModelState.IsValid)
            {
                c01_CongThuc.SuaLanCuoi = DateTime.Now;
                db.Entry(c01_CongThuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c01_CongThuc);
        }

        // GET: CongThuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_CongThuc c01_CongThuc = db.C01_CongThuc.Find(id);
            if (c01_CongThuc == null)
            {
                return HttpNotFound();
            }
            return View(c01_CongThuc);
        }

        // POST: CongThuc/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            C01_CongThuc c01_CongThuc = db.C01_CongThuc.Find(id);
            db.C01_CongThuc.Remove(c01_CongThuc);
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
