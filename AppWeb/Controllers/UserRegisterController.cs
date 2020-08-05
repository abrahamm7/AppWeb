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
        User user = new User();
        private List<SelectListItem> selectListItems;

        // GET: UserRegister
        public ActionResult Index()
        {
            try
            {
                string btnclick = Request["signuser"];
                List<string> roles = new List<string>() { "ADMINISTRADOR", "USUARIO" };
                selectListItems = new List<SelectListItem>();
                foreach (var item in roles)
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Selected = true,
                        Text = item,
                        Value = item
                    });
                }
               
                ViewBag.categ = selectListItems;
              

                if (btnclick == "SignUp")
                {
                    user.Nombre = Request["nametxt"];
                    user.Apellido = Request["lasttxt"];
                    user.Usuario = Request["usertxt"];
                    user.Clave = Request["passtxt"];
                    user.Rol = Request["Roles"];

                    if (string.IsNullOrEmpty(user.Nombre) ||
                        string.IsNullOrEmpty(user.Apellido) ||
                        string.IsNullOrEmpty(user.Clave) ||
                        string.IsNullOrEmpty(user.Usuario) ||
                        string.IsNullOrEmpty(user.Rol))
                    {
                        Debug.WriteLine("Empty fields");
                    }
                    else
                    {
                        Data.InsertUser(user);
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