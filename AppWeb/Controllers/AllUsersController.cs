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
        //[HttpGet]
        //public ActionResult DeleteUser(int iduser)
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //    try
        //    {
        //        SqlConnection sqlConnection = new SqlConnection(cs);
        //        using (SqlCommand command = new SqlCommand("DeleteUser", sqlConnection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@iduser", SqlDbType.Int);
        //            command.Parameters["@iduser"].Value = iduser;
        //            sqlConnection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //        sqlConnection.Close();
        //        Debug.WriteLine("Usuario eliminado");

        //    }
        //    catch (Exception ea)
        //    {
        //        Debug.WriteLine($"Error: {ea.Message}");
        //    }
        //    return RedirectToAction("Index", "AllUsers");           
        //}
    
        ////Edit user//
        //[HttpPost]
        //public ActionResult EditUser(User model)
        //{
        //    string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //    try
        //    {
        //        SqlConnection sqlConnection = new SqlConnection(cs);
        //        using (SqlCommand command = new SqlCommand("UpdateUser", sqlConnection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.Add("@iduser", SqlDbType.Int);
        //            command.Parameters["@iduser"].Value = model.iduser;
        //            command.Parameters.Add("@name", SqlDbType.VarChar);
        //            command.Parameters["@name"].Value = model.name;
        //            command.Parameters.Add("@lastname", SqlDbType.VarChar);
        //            command.Parameters["@lastname"].Value = model.lastname;
        //            command.Parameters.Add("@pass", SqlDbType.Int);
        //            command.Parameters["@pass"].Value = model.password;
        //            command.Parameters.Add("@username", SqlDbType.VarChar);
        //            command.Parameters["@username"].Value = model.username;
        //            sqlConnection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //        sqlConnection.Close();
        //        Debug.WriteLine("Usuario editado");

        //    }
        //    catch (Exception ea)
        //    {
        //        Debug.WriteLine($"Error: {ea.Message}");
        //    }
        //    return RedirectToAction("Index", "AllUsers");
        //}


        ////Get all users from db//
        //public void GetUser()
        //{
            
        //}
    }
}