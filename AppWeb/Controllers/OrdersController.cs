using AppWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class OrdersController : Controller
    {
        IDataAccessDB Data = new DataDB();

        // GET: Orders
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["cookie"];
            var userid = Convert.ToInt32(reqCookies["iduser"]);
            var itemspurchased = Data.ItemsPurchased().Where(elem => elem.UsuarioID == userid).ToList();
            return View(itemspurchased);
        }
    }
}