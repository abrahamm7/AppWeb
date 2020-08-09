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
            try
            {
                string btnclick = Request["signuser"];               
                if (btnclick == "Login")
                {
                    string user = Request["usertxt"];
                    string password = Request["passtxt"];                
                    Response.Cookies.Add(cookie); //This coockie is for save the username//
                   
                    var finduser = Data.GetAllUsers().Where(element => element.UserName == user && element.Password == password).ToList();                   

                    if (finduser.Count != 0 && finduser.First().Role == "ADMINISTRADOR")
                    {
                        cookie["iduser"] = finduser.First().UserId.ToString();
                        return RedirectToAction("Index", "Categories");
                    }
                    else if(finduser.Count != 0 && finduser.First().Role == "USUARIO")
                    {
                        cookie["iduser"] = finduser.First().UserId.ToString();
                        return RedirectToAction("Index", "ViewUsers");
                    }
                    if (finduser.Count == 0)
                    {
                        Response.Write($"<script>alert('This user: {user} not found');</script>");
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en: {ex.Message}");
            }           
            return View();
            
        }
        
        public ActionResult Login()
        {
            string user = Request["usertxt"];
            string password = Request["passtxt"];
            Response.Cookies.Add(cookie); //This coockie is for save the username//

            var finduser = Data.GetAllUsers().Where(element => element.UserName == user && element.Password == password).ToList();

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
            if (finduser.Count == 0)
            {
                return View();
            }
            return View();
        }
    }
}