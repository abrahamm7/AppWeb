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
    public class ProfileUserController : Controller
    {
        IDataAccessDB Data = new DataDB();

        // GET: ProfileUser
        public ActionResult Index()
        {
            HttpCookie reqCookies = Request.Cookies["cookie"];
            var userid = Convert.ToInt32(reqCookies["iduser"]);
            var getuser = Data.GetAllUsers().Where(elem => elem.UserId == userid).ToList();
            return View(getuser);
        }
        public ActionResult EditUser(int? model)
        {
            try
            {
                var finduser = Data.GetAllUsers().Find(elem => elem.UserId == model);
                if (finduser != null)
                {
                    return View(finduser);
                }
                else
                {
                    return RedirectToAction("Index", "AllUsers");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }

        }
              
        [HttpPost]
        public ActionResult EditUser(User id)
        {
            try
            {
                Data.EditUser(id);
                return RedirectToAction("Index", "ProfileUser");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
    }
}