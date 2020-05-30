using ResManager.DAO.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ResManager.Controllers
{
    public class LoginController : Controller
    {
        

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public int AccessControl(string email, string pass)
        {
            return checkValid(email, pass);
        }

        private int checkValid(string email, string pass)
        {
            AcountAccess access = new AcountAccess();
            foreach (var item in access.listUser)
            {
                if (item.email.Equals(email) && item.pass.Equals(pass))
                {
                    return item.id.Value;
                }
            }
            return 0;
        }
    }
}