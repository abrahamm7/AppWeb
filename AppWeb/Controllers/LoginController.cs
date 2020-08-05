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
        IDataAccessDB Data = new DataDB();
        private List<SelectListItem> selectListItems;
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

                    var finduser = Data.GetAllUsers().Where(element => element.Usuario == user && element.Clave == password).ToList();
                    cookie["iduser"] = finduser.First().UserId.ToString();

                    if (finduser.Count != 0 && finduser.First().Rol == "ADMINISTRADOR")
                    {                        
                        return RedirectToAction("Index", "Categories");
                    }
                    else if(finduser.Count != 0 && finduser.First().Rol == "USUARIO")
                    {
                        return RedirectToAction("Index", "ViewUsers");
                    }
                    if (finduser.Count == 0)
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