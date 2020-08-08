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
                    user.Name = Request["nametxt"];
                    user.LastName = Request["lasttxt"];
                    user.UserName = Request["usertxt"];
                    user.Password = Request["passtxt"];
                    user.Role = Request["Roles"];

                    var searchuser = Data.GetAllUsers().Where(elem => elem.UserName == user.UserName).ToList(); //Search user with the same username//
                    if (searchuser.Count == 0)
                    {
                        if (string.IsNullOrEmpty(user.Name) ||
                       string.IsNullOrEmpty(user.LastName) ||
                       string.IsNullOrEmpty(user.Password) ||
                       string.IsNullOrEmpty(user.UserName) ||
                       string.IsNullOrEmpty(user.Role))
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
                        Response.Write($"<script>alert('A user with {user.UserName} already exists');</script>");
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