using ResManager.DAO.Databases;
using ResManager.DAO.WebModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ResManager.Controllers
{
    public class CustomerController : Controller
    {
        private QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
        // GET: Customer
        public ActionResult Index()
        {
            ViewBag.Title = "Quản lý món ăn";
            var c01_Mon = db.C01_Mon;
            return View(c01_Mon.ToList());
        }

        public ActionResult DanhGia(int? id)
        {
            ViewBag.Title = "Khách hàng đánh giá";
            var c01_Mon = db.C01_Mon.FirstOrDefault(ptr => ptr.Id == id);
            var NhanXet = db.C03_CustomerReview.Where(ptr => ptr.IdMon == id).ToList();
            List<Review> lisReview = new List<Review>();
            var lisDate = NhanXet.Select(ptr => ptr.NgayTao.Value.ToShortDateString()).GroupBy(ptr => ptr).ToList();

            foreach (var item in lisDate)
            {
                DateTime date = DateTime.Parse(item.FirstOrDefault());
                Review review = new Review()
                {
                    ngayTao = date,
                    lisReview = NhanXet.Where(ptr => ptr.NgayTao.Value.ToShortDateString().Equals(date.ToShortDateString())).ToList()
                };
                lisReview.Add(review);
            }
            lisReview.Reverse();
            DanhGiaViewModel danhGiaViewModel = new DanhGiaViewModel()
            {
                mon = c01_Mon,
                lisReview = lisReview,
            };
            return View(danhGiaViewModel);
        }

        public int GuiDanhGia(C03_CustomerReview c03_CustomerReview)
        {
            C03_CustomerReview c03_CustomerReview1 = new C03_CustomerReview()
            {
                IdMon = c03_CustomerReview.IdMon,
                NgayTao = DateTime.Now,
                Rating = c03_CustomerReview.Rating,
                Email = c03_CustomerReview.Email,
                Review = c03_CustomerReview.Review,
            };
            try
            {
                db.C03_CustomerReview.Add(c03_CustomerReview1);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return 1;
            }
            return 0;
        }
    }
}