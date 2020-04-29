using ResManager.DAO.Databases;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ResManager.Controllers
{
    public class MenuController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        // GET: Menu
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý menu";
            var c01_Menu = db.C01_Menu.Include(c => c.C01_LoaiThucDon);
            return View(c01_Menu.ToList());
        }

        // GET: Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Menu c01_Menu = db.C01_Menu.Find(id);
            if (c01_Menu == null)
            {
                return HttpNotFound();
            }
            return View(c01_Menu);
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Tạo mới";
            ViewBag.IdLoaiThucDon = new SelectList(db.C01_LoaiThucDon, "Id", "TenLoai");
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,TenMenu,SoLuongMon,IdLoaiThucDon")] C01_Menu c01_Menu)
        {
            ViewBag.Title = "Tạo mới";
            if (ModelState.IsValid)
            {
                c01_Menu.NgayTao = DateTime.Now;
                c01_Menu.SuaLanCuoi = DateTime.Now;
                c01_Menu.SoLuongMon = 0;
                db.C01_Menu.Add(c01_Menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdLoaiThucDon = new SelectList(db.C01_LoaiThucDon, "Id", "TenLoai", c01_Menu.IdLoaiThucDon);
            return View(c01_Menu);
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Menu c01_Menu = db.C01_Menu.Find(id);
            if (c01_Menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdLoaiThucDon = new SelectList(db.C01_LoaiThucDon, "Id", "TenLoai", c01_Menu.IdLoaiThucDon);
            return View(c01_Menu);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,TenMenu,IdLoaiThucDon")] C01_Menu c01_Menu)
        {
            if (ModelState.IsValid)
            {
                c01_Menu.SuaLanCuoi = DateTime.Now;
                db.Entry(c01_Menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLoaiThucDon = new SelectList(db.C01_LoaiThucDon, "Id", "TenLoai", c01_Menu.IdLoaiThucDon);
            return View(c01_Menu);
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Menu c01_Menu = db.C01_Menu.Find(id);
            if (c01_Menu == null)
            {
                return HttpNotFound();
            }
            return View(c01_Menu);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            C01_Menu c01_Menu = db.C01_Menu.Find(id);
            db.C01_Menu.Remove(c01_Menu);
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
