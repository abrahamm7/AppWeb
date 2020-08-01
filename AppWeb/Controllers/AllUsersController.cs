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
        IDataAccessDB Data = new DataDB();
      
        // GET: AllUsers
        public ActionResult Index()
        {
            try
            {
                var obtainUsers = Data.GetAllUsers();
                return View(obtainUsers);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }      
        //Go to view for edit user//
        public ActionResult EditUser(int? iduser)
        {
            try
            {
                var finduser = Data.GetAllUsers().Find(elem => elem.UserId == iduser);
                if (finduser != null)
                {
                    return View(finduser);
                }
                else
                {
                    return RedirectToAction("Index", "AllUsers");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
           
        }

        public ActionResult ViewUser(int iduser)
        {
            try
            {
                var finduser = Data.GetAllUsers().Find(elem => elem.UserId == iduser);
                if (finduser != null)
                {
                    return View(finduser);
                }
                else
                {
                    return RedirectToAction("Index", "AllUsers");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }        
        }
        //Delete Users//
        [HttpGet]
        public ActionResult DeleteUser(int iduser)
        {
            try
            {
                Data.DeleteUser(iduser);
                return RedirectToAction("Index", "AllUsers");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }       
        }

        ////Edit user//
        [HttpPost]
        public ActionResult EditUser(User model)
        {
            try
            {
                Data.EditUser(model);
                return RedirectToAction("Index", "AllUsers");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
    }
}