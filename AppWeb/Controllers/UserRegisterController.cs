using AppWeb.Models;
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
        // GET: UserRegister
        public ActionResult Index()
        {
            try
            {
                User user = new User();
                string btnclick = Request["signupuser"];
                user.name = Request["nametxt"];
                user.lastname = Request["lasttxt"];
                user.username = Request["usertxt"];
                user.password = Request["passtxt"];
                if (btnclick == "SignUp")
                {
                    InsertUser(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en: {ex.Message}");
            }
            return View();
        }

        //Add User to db//
        public void InsertUser(User user)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand("InsertUser", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@nombre", SqlDbType.VarChar);
                    command.Parameters["@nombre"].Value = user.name; 
                    command.Parameters.Add("@apellido", SqlDbType.VarChar);
                    command.Parameters["@apellido"].Value = user.lastname; 
                    command.Parameters.Add("@clave", SqlDbType.VarChar);
                    command.Parameters["@clave"].Value = user.password; 
                    command.Parameters.Add("@usuario", SqlDbType.VarChar);
                    command.Parameters["@usuario"].Value = user.username;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
                Debug.WriteLine("Usuario Registrado");
            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }

        }
    }
}