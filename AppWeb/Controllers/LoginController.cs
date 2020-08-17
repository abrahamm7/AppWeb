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
        public ActionResult LoginUser(User user) //Login for users//
        {
            try
            {
                if (user.UserName == null || user.Password == null) //If model is empty, return a error message//
                {
                    ViewBag.Message = "Empty fields"; 
                    return View("Index");
                }
                else
                {
                    //Search if the user exist in DB//
                    var finduser = Data.GetAllUsers().Where(element => element.UserName == user.UserName && element.Password == user.Password).ToList();

                    
                    if (finduser.Count != 0 && finduser.First().Role == "ADMINISTRADOR") //If user is admin, navigate to page for CRUD operations//
                    {
                        cookie["iduser"] = finduser.First().UserId.ToString();
                        return RedirectToAction("Index", "Categories");
                    }
                    else if (finduser.Count != 0 && finduser.First().Role == "USUARIO") //If user is usuario, navigate to page for purchase and view products//
                    {
                        cookie["iduser"] = finduser.First().UserId.ToString();
                        return RedirectToAction("Index", "ViewUsers");
                    }

                    if (finduser.Count == 0 || finduser == null) //If list is empty, return a error message//
                    {
                        ViewBag.Message = "User not found";
                        return View("Index");
                    }
                }
                
                return View("Index");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View("Index");
            }
           
        }
      
    }
}