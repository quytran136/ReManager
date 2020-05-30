using ResManager.DAO.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.DAO.WebModel
{
    public class Customer
    {
        List<C01_Mon> lisMon { get; set; } = new List<C01_Mon>();
        List<C01_CongThuc> lisCongThuc { get; set; } = new List<C01_CongThuc>();
    }
}
