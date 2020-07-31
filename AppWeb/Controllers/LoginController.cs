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
        DataAccessDB Data = new DataDB();
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
                    var finduser = Data.GetAllUsers().Where(element => element.Usuario == user && element.Clave == password).ToList();
                    if (finduser != null)
                    {                        
                        return RedirectToAction("Index", "Categories");
                    }
                    else
                    {
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
    }
}