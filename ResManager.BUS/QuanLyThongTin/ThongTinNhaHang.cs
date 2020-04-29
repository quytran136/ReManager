using ResManager.BUS.Message;
using ResManager.DAO.Databases;
using ResManager.DAO.WebModel;
using System;

namespace ResManager.BUS.QuanLyThongTin
{
    public class ThongTinNhaHang
    {

        public C00_NhaHang GetThongTinNhaHang()
        {
            NhaHang nhaHang = new NhaHang();
            try
            {
                C00_NhaHang thongTinNhaHang = nhaHang.GetThongTinNhaHang();
                return thongTinNhaHang;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveThongTinNhaHang(C00_NhaHang thongTinNhaHangNew)
        {
            NhaHang nhaHang = new NhaHang();
            nhaHang.SaveThongTinNhaHang(thongTinNhaHangNew);
        }
    }
}
