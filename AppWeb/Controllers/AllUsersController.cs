﻿using AppWeb.Models;
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
        // GET: AllUsers
        public ActionResult Index()
        {
            var x = GetUser();
            if (x.Count != 0)
            {
                return View(x);
            }
            else
            {
                return View();
            }
        }

        public List<User> GetUser()
        {
            try
            {
                List<User> listado = new List<User>();
                string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand("GetAllUser", con))
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
                                        users.username = reader[1].ToString();
                                    }
                                    if (reader[2] != System.DBNull.Value)
                                    {
                                        users.name = reader[2].ToString();
                                    }
                                    if (reader[3] != System.DBNull.Value)
                                    {
                                        users.lastname = reader[3].ToString();
                                    }
                                    if (reader[4] != System.DBNull.Value)
                                    {
                                        users.password = reader[4].ToString();
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
    
        public ActionResult EditUser()
        {
            return View();
        }

        //Delete Users//
        [HttpGet]
        public ActionResult DeleteUser(int iduser)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand("DeleteUser", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@iduser", SqlDbType.Int);
                    command.Parameters["@iduser"].Value = iduser;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
                Debug.WriteLine("Usuario eliminado");

            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }
            return RedirectToAction("Index", "AllUsers");           
        }
    }
}