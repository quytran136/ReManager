using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.DAO.WebModel
{
    public class DatBanViewModel
    {
        public int? idTrangThai { get; set; }
        public int? idBanAn { get; set; }
        public List<C01_Mon> lisMonAn { get; set; } = new List<C01_Mon>();
        public List<MonDaGoi> lisMonSuDung { get; set; } = new List<MonDaGoi>();
        public List<C01_Menu> lisMenu { get; set; } = new List<C01_Menu>();
    }


    public class MonDaGoi
    {
        public int? IdMon { get; set; }
        public string TenMon { get; set; }
        public string DonVi { get; set; }
        public string DonGia { get; set; }
        public string SoLuong { get; set; }
    }
}
