using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.DAO.WebModel
{
   public class DanhGiaViewModel
    {
        public C01_Mon mon { get; set; } = new C01_Mon();

        public List<Review> lisReview { get; set; } = new List<Review>();
    }

    public class Review
    {
        public DateTime? ngayTao { get; set; } = new DateTime();
        public List<C03_CustomerReview> lisReview { get; set; } = new List<C03_CustomerReview>();
    }
}
