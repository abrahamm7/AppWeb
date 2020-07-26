﻿using AppWeb.Models;
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
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }
        
        public List<Categorie> GetCategories()
        {
            try
            {               
                List<Categorie> categories = new List<Categorie>();

                string name = Request["usertxt"];
                string password = Request["passtxt"];
                string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string query = $"SELECT * FROM CategoriaTbl";
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
                                    Categorie categorie = new Categorie();

                                    if (reader[0] != System.DBNull.Value)
                                    {
                                        categorie.idcategorie = Convert.ToInt32(reader[0]);
                                    }
                                    if (reader[1] != System.DBNull.Value)
                                    {
                                        categorie.categorie = reader[1].ToString();
                                    }                                   
                                    categories.Add(categorie);
                                }
                            }
                        }
                    }
                    con.Close();
                    return categories;
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