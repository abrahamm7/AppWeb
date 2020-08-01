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
    public class AllUsersController : Controller
    {
        DataAccessDB Data = new DataDB();
      
        // GET: AllUsers
        public ActionResult Index()
        {
            var obtainUsers = Data.GetAllUsers();
            return View(obtainUsers);
        }      
        //Go to view for edit user//
        public ActionResult EditUser(int? iduser)
        {
            if (iduser != null)
            {
                var finduser = Data.GetAllUsers().Find(elem => elem.UserId == iduser);
                return View(finduser);
            }
            else
            {
                return RedirectToAction("Index", "AllUsers");
            }
           
        }

        public ActionResult ViewUser(int? iduser)
        {
            if (iduser != null)
            {
                var finduser = Data.GetAllUsers().Find(elem => elem.UserId == iduser);
                return View(finduser);
            }
            else
            {
                return RedirectToAction("Index", "AllUsers");
            }
        }
        //Delete Users//
        [HttpGet]
        public ActionResult DeleteUser(int iduser)
        {
            Data.DeleteUser(iduser);
            return RedirectToAction("Index", "AllUsers");            
        }

        ////Edit user//
        [HttpPost]
        public ActionResult EditUser(User model)
        {
            Data.EditUser(model);
            return RedirectToAction("Index", "AllUsers");
        }
    }
}