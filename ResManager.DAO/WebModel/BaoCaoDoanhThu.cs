using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.DAO.WebModel
{
    public class BaoCaoDoanhThu
    {
        public int? Thang { get; set; }
        public decimal? DoanhSo { get; set; }
        public decimal? TongThu { get; set; }
        public decimal? TongDuNo { get; set; }
        public List<BaoCaoHoaDon> baoCaoHoaDon { get; set; } = new List<BaoCaoHoaDon>();
    }

    public class BaoCaoHoaDon
    {
        public DateTime? Ngay { get; set; }
        public decimal? DoanhSo { get; set; }
        public decimal? TongThu { get; set; }
        public decimal? TongDuNo { get; set; }
    }
}
