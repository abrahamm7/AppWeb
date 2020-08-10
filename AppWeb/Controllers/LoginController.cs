using AppWeb.Models;
using AppWeb.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class LoginController : Controller
    {
        IUserModule Data = new UserModule();        
        HttpCookie cookie = new HttpCookie("cookie");     

        // GET: Login
        public ActionResult Index()
        {          
            return View();            
        }

        [HttpPost]
        public ActionResult Index(User user) //Login for users//
        {
            try
            {
                if (user.UserName == null || user.Password == null)
                {
                    ViewBag.Message = "Empty fields";
                    return View();
                }
                else
                {
                    var finduser = Data.GetAllUsers().Where(element => element.UserName == user.UserName && element.Password == user.Password).ToList();
                    if (finduser.Count != 0 && finduser.First().Role == "ADMINISTRADOR")
                    {
                        cookie["iduser"] = finduser.First().UserId.ToString();
                        return RedirectToAction("Index", "Categories");
                    }
                    else if (finduser.Count != 0 && finduser.First().Role == "USUARIO")
                    {
                        cookie["iduser"] = finduser.First().UserId.ToString();
                        return RedirectToAction("Index", "ViewUsers");
                    }

                    if (finduser.Count == 0 || finduser == null)
                    {
                        ViewBag.Message = "User not found";
                        return View();
                    }
                }
                
                return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
           
        }
      
    }
}