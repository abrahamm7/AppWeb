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
    public class CategoriesController : Controller
    {
       
        Categorie categorie = new Categorie();
        // GET: Categories
        public ActionResult Index()
        {
            var x = GetCategories();
            try
            {
                string btnclick = Request["AddCategory"];
                if (btnclick == "Add")
                {
                    categorie.categorie = Request["categorytxt"];
                    InsertCategorie(categorie);
                    return View(x);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en: {ex.Message}");
            }
            return View(x);
        }
        
        public List<Categorie> GetCategories()
        {
            try
            {               
                List<Categorie> categories = new List<Categorie>();
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

        public void InsertCategorie(Categorie categorie)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                string query = $"INSERT INTO CategoriaTbl(Categoria)values('{categorie.categorie}')";
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
                Debug.WriteLine("Categoria Registrada");
            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }

        }
    }
}