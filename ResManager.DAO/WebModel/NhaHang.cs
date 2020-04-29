using ResManager.Common.Message;
using ResManager.DAO.Databases;
using System.Linq;

namespace ResManager.DAO.WebModel
{
    public class NhaHang
    {
        public C00_NhaHang GetThongTinNhaHang()
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            C00_NhaHang thongTinNhaHang = new C00_NhaHang();
            thongTinNhaHang = db.C00_NhaHang.FirstOrDefault();
            if (thongTinNhaHang == null)
            {
                throw new System.Exception(SystemExceptionMessage.CM_SY_001);
            }
            if (thongTinNhaHang.DiaChi == null)
            {
                thongTinNhaHang.DiaChi = string.Empty;
            }
            if (thongTinNhaHang.TenNhaHang == null)
            {
                thongTinNhaHang.TenNhaHang = string.Empty;
            }
            return thongTinNhaHang;
        }

        public void SaveThongTinNhaHang(C00_NhaHang thongTinNhaHangNew)
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            C00_NhaHang thongTinNhaHangOld = db.C00_NhaHang.FirstOrDefault();
            if (thongTinNhaHangOld == null)
            {
                db.C00_NhaHang.Add(new C00_NhaHang()
                {
                    TenNhaHang = thongTinNhaHangNew.TenNhaHang,
                    DiaChi = thongTinNhaHangNew.DiaChi
                });
                db.SaveChanges();
            }
            else
            {
                thongTinNhaHangOld.TenNhaHang = thongTinNhaHangNew.TenNhaHang;
                thongTinNhaHangOld.DiaChi = thongTinNhaHangNew.DiaChi;
                db.SaveChanges();
            }
        }
    }
}
