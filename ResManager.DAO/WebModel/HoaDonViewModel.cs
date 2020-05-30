using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.DAO.WebModel
{
    public class HoaDonViewModel
    {
        public List<MonDaGoi> lisMonAn { get; set; } = new List<MonDaGoi>();
        public C00_BanAn BanAn { get; set; } = new C00_BanAn();
        public decimal? TongPhaiThu { get; set; }
        public decimal? TongThucThu { get; set; }
        public decimal? TongDuNo { get; set; }
        public string NgayTao { get; set; }
        public decimal? TrietKhau { get; set; }
        public string MaVoucher { get; set; }
    }
}
