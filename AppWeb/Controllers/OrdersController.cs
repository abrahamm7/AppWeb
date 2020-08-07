using AppWeb.Models;
using AppWeb.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class OrdersController : Controller
    {
        IReceipModule Data = new ReceipModule();

        // GET: Orders
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["cookie"];
            var userid = Convert.ToInt32(reqCookies["iduser"]);
            try
            {
                var itemspurchased = Data.ItemsPurchased().Where(elem => elem.UsuarioID == userid).ToList();
                return View(itemspurchased);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
    }
}