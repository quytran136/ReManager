using ResManager.Common.Message;
using ResManager.DAO.WebModel;
using System;
using System.Collections.Generic;

namespace ResManager.BUS.QuanLyThongTin
{
    public class QuanLyKhuVuc
    {
        public List<KhuVucAn> GetDanhSachKhuVuc()
        {
            KhuVucAn khuVucAn = new KhuVucAn();
            List<KhuVucAn> lisKhuVucAn = new List<KhuVucAn>();

            lisKhuVucAn = khuVucAn.GetKhuVucAn();
            if (lisKhuVucAn.Count == 0)
            {
                throw new Exception(SystemExceptionMessage.CM_SY_001);
            }

            return lisKhuVucAn;
        }

        public void AddKhuVucAn(KhuVucAn khuVucAnMoi)
        {
            KhuVucAn khuVucAn = new KhuVucAn();
            khuVucAnMoi.NgayTao = DateTime.Now;
            khuVucAn.AddKhuVucAn(khuVucAnMoi);
            return;
        }

    }
}
