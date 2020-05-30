using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.DAO.WebModel
{
    public class TinhHinhHoatDongTrongThang
    {
        public List<TinhHinhHoatDongTrongNgay> DoanhThuTrongThang { get; set; } = new List<TinhHinhHoatDongTrongNgay>();
    }

    public class TinhHinhHoatDongTrongNgay
    {
        public string DoanhThu { get; set; }
        public string SoBanDangSuDung { get; set; }
        public string DuKienDoanhThu { get; set; }
    }

    public class TinhTrangPhucVuTheoKhuVuc
    {
        public string TenKhuVuc { get; set; }
        public int SoBanDaDung { get; set; }
    }
}
