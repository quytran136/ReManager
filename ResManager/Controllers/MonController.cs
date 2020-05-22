using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using ResManager.BUS.QuanLyThongTin;
using ResManager.DAO.CommonModel;
using ResManager.DAO.Databases;

namespace ResManager.Controllers
{
    public class MonController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();

        // GET: Mon
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý món ăn";
            var c01_Mon = db.C01_Mon.Include(c => c.Root_QuanLyTrangThai);
            return View(c01_Mon.ToList());
        }

        // GET: Mon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Mon c01_Mon = db.C01_Mon.Find(id);
            if (c01_Mon == null)
            {
                return HttpNotFound();
            }
            return View(c01_Mon);
        }

        // GET: Mon/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Thêm món ăn";
            List<TrangThai> listTrangThai = new QuanLyTrangThai().GetTrangThai(3);
            ViewBag.IdTrangThai = new SelectList(listTrangThai, "Id", "TenTrangThai");
            return View();
        }

        // POST: Mon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,TenMon,IdTrangThai,DonVi,DonGia")] C01_Mon c01_Mon)
        {
            if (ModelState.IsValid)
            {
                db.C01_Mon.Add(c01_Mon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTrangThai = new SelectList(db.Root_QuanLyTrangThai, "Id", "Id", c01_Mon.IdTrangThai);
            return View(c01_Mon);
        }

        // GET: Mon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Mon c01_Mon = db.C01_Mon.Find(id);
            if (c01_Mon == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTrangThai = new SelectList(db.Root_QuanLyTrangThai, "Id", "Id", c01_Mon.IdTrangThai);
            return View(c01_Mon);
        }

        // POST: Mon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,TenMon,IdTrangThai,DonVi,DonGia")] C01_Mon c01_Mon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c01_Mon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTrangThai = new SelectList(db.Root_QuanLyTrangThai, "Id", "Id", c01_Mon.IdTrangThai);
            return View(c01_Mon);
        }

        // GET: Mon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Mon c01_Mon = db.C01_Mon.Find(id);
            if (c01_Mon == null)
            {
                return HttpNotFound();
            }
            return View(c01_Mon);
        }

        // POST: Mon/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            C01_Mon c01_Mon = db.C01_Mon.Find(id);
            db.C01_Mon.Remove(c01_Mon);
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

        [HttpPost]
        public string GetMonAn(int? idThucDon)
        {
            List<test> lisMonAn = new List<test>();
            lisMonAn = (from sapxepmenu in db.C01_SapXepMenu
                        join mon in db.C01_Mon on sapxepmenu.IdMon equals mon.Id
                        where sapxepmenu.IdMenu == idThucDon
                        select new test
                        {
                            Id = mon.Id,
                            TenMon = mon.TenMon,
                            DonGia = mon.DonGia.Value,
                            DonVi = mon.DonVi
                        }).ToList();
            return JsonConvert.SerializeObject(lisMonAn, Formatting.Indented);
        }
    }

    public class test
    {
        public int? Id { get; set; }

        public string TenMon { get; set; }

        public Decimal DonGia { get; set; }

        public string DonVi { get; set; }
    }
}
