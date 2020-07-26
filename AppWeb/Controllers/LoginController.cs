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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            try
            {
                string btnclick = Request["signuser"];
                if (btnclick == "Login")
                {
                    var x = GetUser();
                    if (x != null)
                    {
                        Debug.WriteLine($"Este usuario existe: {x.First().name + x.First().iduser}");
                        //return RedirectToAction("", "");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en: {ex.Message}");
            }
            return View();
            
        }

        public List<User> GetUser()
        {
            try
            {
                List<User> listado = new List<User>();

                string name = Request["usertxt"];
                string password = Request["passtxt"];                
                string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = $"SELECT * FROM UserTbl WHERE Usuario = '{name}' and Clave = '{password}'";
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    User users = new User();

                                    if (reader[0] != System.DBNull.Value)
                                    {
                                        users.iduser = Convert.ToInt32(reader[0]);
                                    }
                                    if (reader[1] != System.DBNull.Value)
                                    {
                                        users.name = reader[1].ToString();
                                    }
                                    if (reader[2] != System.DBNull.Value)
                                    {
                                        users.password = reader[2].ToString();
                                    }
                                    listado.Add(users);
                                }
                            }
                        }
                    }
                    con.Close();
                    return listado;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
           
        }
    }
}