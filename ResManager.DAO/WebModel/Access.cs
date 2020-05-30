using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResManager.DAO.WebModel
{
    public class Access
    {
        public string email { get; set; }
        public string pass { get; set; }
        public int? id { get; set; }
    };
    public class AcountAccess
    {
        public List<Access> listUser { get; set; } = new List<Access>()
        {
            new Access()
            {
                email = "admin",
                pass = "123",
                id = 1,
            },
            new Access()
            {
                email = "nhanvien1",
                pass = "123",
                id = 2,
            },
            new Access()
            {
                email = "nhanvien2",
                pass = "123",
                id = 3,
            }
        };
    }
}

