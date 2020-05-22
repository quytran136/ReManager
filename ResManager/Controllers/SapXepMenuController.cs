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
    public class SapXepMenuController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        // GET: SapXepMenu
        public ActionResult Index()
        {
            var c01_SapXepMenu = db.C01_SapXepMenu.Include(c => c.C01_Menu).Include(c => c.C01_Mon);
            return View(c01_SapXepMenu.ToList());
        }

        // GET: SapXepMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_SapXepMenu c01_SapXepMenu = db.C01_SapXepMenu.Find(id);
            if (c01_SapXepMenu == null)
            {
                return HttpNotFound();
            }
            return View(c01_SapXepMenu);
        }

        // GET: SapXepMenu/Create
        public ActionResult Create()
        {
            ViewBag.IdMenu = new SelectList(db.C01_Menu, "Id", "TenMenu");
            ViewBag.IdMon = new SelectList(db.C01_Mon, "Id", "TenMon");
            return View();
        }

        // POST: SapXepMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "Id,IdMenu,IdMon")] C01_SapXepMenu c01_SapXepMenu)
        {
            if (ModelState.IsValid)
            {
                db.C01_SapXepMenu.Add(c01_SapXepMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMenu = new SelectList(db.C01_Menu, "Id", "TenMenu", c01_SapXepMenu.IdMenu);
            ViewBag.IdMon = new SelectList(db.C01_Mon, "Id", "TenMon", c01_SapXepMenu.IdMon);
            return View(c01_SapXepMenu);
        }

        // GET: SapXepMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_SapXepMenu c01_SapXepMenu = db.C01_SapXepMenu.Find(id);
            if (c01_SapXepMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMenu = new SelectList(db.C01_Menu, "Id", "TenMenu", c01_SapXepMenu.IdMenu);
            ViewBag.IdMon = new SelectList(db.C01_Mon, "Id", "TenMon", c01_SapXepMenu.IdMon);
            return View(c01_SapXepMenu);
        }

        // POST: SapXepMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "Id,IdMenu,IdMon")] C01_SapXepMenu c01_SapXepMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c01_SapXepMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMenu = new SelectList(db.C01_Menu, "Id", "TenMenu", c01_SapXepMenu.IdMenu);
            ViewBag.IdMon = new SelectList(db.C01_Mon, "Id", "TenMon", c01_SapXepMenu.IdMon);
            return View(c01_SapXepMenu);
        }

        // GET: SapXepMenu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_SapXepMenu c01_SapXepMenu = db.C01_SapXepMenu.Find(id);
            if (c01_SapXepMenu == null)
            {
                return HttpNotFound();
            }
            return View(c01_SapXepMenu);
        }

        // POST: SapXepMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        
        public ActionResult DeleteConfirmed(int id)
        {
            C01_SapXepMenu c01_SapXepMenu = db.C01_SapXepMenu.Find(id);
            db.C01_SapXepMenu.Remove(c01_SapXepMenu);
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
