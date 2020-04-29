using ResManager.DAO.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.BUS.QuanLyThongTin
{
    public class QuanLyTrangThai
    {
        public List<TrangThai> GetTrangThai(int idTrangThai)
        {
            return new TrangThai().GetTrangThaiKhuVucAn(idTrangThai);
        }
    }
}
