using AppWeb.Models;
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

        public void InsertUser(User user)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                string query = $"INSERT INTO UserTbl(Usuario, Nombre, Apellido, Clave)values('{user.username}','{user.name}','{user.lastname}','{user.password}')";
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
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