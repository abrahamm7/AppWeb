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
        private List<SelectListItem> selectListItems;

        // GET: UserRegister
        public ActionResult Index()
        {
            FillDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user) //Register User in DB//
        {
            try
            {
                FillDropDownList();
                var searchuser = Data.GetAllUsers().Where(elem => elem.UserName == user.UserName).ToList(); //Search user with the same username//
                user.Role = Request["Roles"];
                if (searchuser.Count == 0)
                {
                    if (string.IsNullOrEmpty(user.Name) ||
                   string.IsNullOrEmpty(user.LastName) ||
                   string.IsNullOrEmpty(user.Password) ||
                   string.IsNullOrEmpty(user.UserName) ||
                   string.IsNullOrEmpty(user.Role))
                    {
                        ViewBag.Message = "Empty fields";
                    }
                    else
                    {
                        Data.InsertUser(user); //Method for insert user//
                        ViewBag.Message = "Success!";
                    }
                }
                else
                {
                    ViewBag.Message = $"A user with username: {user.UserName} already exists";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
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