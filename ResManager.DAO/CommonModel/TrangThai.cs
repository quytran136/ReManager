using ResManager.Common.Message;
using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ResManager.DAO.CommonModel
{
    public class TrangThai
    {
        private int id;

        private string tenTrangThai;

        private string tenNhomTrangThai;

        public int Id
        {
            get => id;
            set => id = value;
        }

        [Display(Name = "Tên trạng thái")]
        public string TenTrangThai
        {
            get => tenTrangThai;
            set => tenTrangThai = value;
        }

        [Display(Name = "Tên nhóm trạng thái")]
        public string TenNhomTrangThai
        {
            get => tenNhomTrangThai;
            set => tenNhomTrangThai = value;
        }

        public List<TrangThai> GetTrangThaiKhuVucAn(int idTrangThai)
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            List<TrangThai> lisTrangThai = new List<TrangThai>();
            lisTrangThai = (from quanLyTrangThai in db.Root_QuanLyTrangThai
                            join trangThai in db.Root_TrangThai on quanLyTrangThai.IdTrangThai equals trangThai.Id
                            where quanLyTrangThai.IdNhomTrangThai == idTrangThai
                            select new TrangThai()
                            {
                                Id = trangThai.Id,
                                TenTrangThai = trangThai.TenTrangThai,
                                TenNhomTrangThai = quanLyTrangThai.Root_NhomTrangThai.TenTrangThai
                            }).ToList<TrangThai>();

            if (lisTrangThai == null)
            {
                throw new Exception(SystemExceptionMessage.CM_SY_001);
            }

            return lisTrangThai;
        }
    }
}
