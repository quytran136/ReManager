using ResManager.Common.Message;
using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ResManager.DAO.WebModel
{
    public class KhuVucAn
    {

        private string tenLoaiKhuVuc;

        private DateTime? ngayTao;

        private string tenTrangThai;

        private int idTrangThai;

        private int id;

        [Display(Name = "Tên khu vực")]
        public string TenLoaiKhuVuc
        {
            get => tenLoaiKhuVuc;
            set => tenLoaiKhuVuc = value;
        }

        [Display(Name = "Ngày tạo")]
        public DateTime? NgayTao
        {
            get => ngayTao;
            set => ngayTao = value;
        }

        [Display(Name = "Trạng thái")]
        public string TenTrangThai
        {
            get => tenTrangThai;
            private set => TenTrangThai = value;
        }

        [Display(Name = "IdTrangThai")]
        public int IdTrangThai
        {
            get => idTrangThai;
            set => idTrangThai = value;
        }

        public int ID
        {
            get => id;
        }


        public List<KhuVucAn> GetKhuVucAn()
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            List<KhuVucAn> khuVucAn = new List<KhuVucAn>();
            List<C00_KhuVucAn> c00_KhuVucAn = db.C00_KhuVucAn.ToList();

            foreach (C00_KhuVucAn item in c00_KhuVucAn)
            {
                khuVucAn.Add(new KhuVucAn()
                {
                    tenLoaiKhuVuc = item.TenLoaiKhuVuc,
                    ngayTao = item.NgayTao.Value,
                    tenTrangThai = item.Root_QuanLyTrangThai.Root_TrangThai.TenTrangThai,
                    id = item.Id,
                    idTrangThai = item.IdTrangThai.Value,
                });
            }

            return khuVucAn;
        }

        public void AddKhuVucAn(KhuVucAn khuVucAn)
        {
            QuanLyNhaHangEntities db = new QuanLyNhaHangEntities();
            C00_KhuVucAn c00_KhuVucAn = new C00_KhuVucAn()
            {
                TenLoaiKhuVuc = khuVucAn.TenLoaiKhuVuc,
                IdTrangThai = khuVucAn.IdTrangThai,
                NgayTao = DateTime.Now
            };

            db.C00_KhuVucAn.Add(c00_KhuVucAn);
            db.SaveChanges();
        }
    }
}
