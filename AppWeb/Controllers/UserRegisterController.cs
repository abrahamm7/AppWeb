using AppWeb.Models;
using AppWeb.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class UserRegisterController : Controller
    {
        IDataAccessDB Data = new DataDB();
        // GET: UserRegister
        public ActionResult Index()
        {
            try
            {
                User user = new User();
                string btnclick = Request["signuser"];
                user.Nombre = Request["nametxt"];
                user.Apellido = Request["lasttxt"];
                user.Usuario = Request["usertxt"];
                user.Clave = Request["passtxt"];
                if (btnclick == "SignUp")
                {
                    Data.InsertUser(user);
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