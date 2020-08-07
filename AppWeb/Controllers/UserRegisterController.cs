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
        IUserModule Data = new UserModule();
        User user = new User();
        private List<SelectListItem> selectListItems;

        // GET: UserRegister
        public ActionResult Index()
        {
            try
            {
                string btnclick = Request["signuser"];

                FillDropDownList();             

                if (btnclick == "SignUp")
                {
                    user.Nombre = Request["nametxt"];
                    user.Apellido = Request["lasttxt"];
                    user.Usuario = Request["usertxt"];
                    user.Clave = Request["passtxt"];
                    user.Rol = Request["Roles"];

                    var searchuser = Data.GetAllUsers().Where(elem => elem.Usuario == user.Usuario).ToList(); //Search user with the same username//
                    if (searchuser.Count == 0)
                    {
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
                    else
                    {
                        Response.Write($"<script>alert('A user with {user.Usuario} already exists');</script>");
                    }                   
                   
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en: {ex.Message}");
            }
            return View();
        }

        public void FillDropDownList() //This is for fill the dropdownlist//
        {
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
        }
    }
}