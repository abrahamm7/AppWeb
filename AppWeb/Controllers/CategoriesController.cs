using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                string btnclick = Request["btncategory"];
                if (btnclick == "Add")
                {
                    categorie.categorie = Request["categorytxt"];
                    InsertCategory(categorie);
                    return View(x);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en: {ex.Message}");
            }
            return View(x);
        }
        //Get All categories from the DB//
        public List<Categorie> GetCategories()
        {
            try
            {               
                List<Categorie> categories = new List<Categorie>();
                string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand("GetAllCategories", con))
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

        //Insert new Category//
        public void InsertCategory(Categorie categorie)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {                
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand("InsertCategory", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@categoria", SqlDbType.VarChar);
                    command.Parameters["@categoria"].Value = categorie.categorie;                   
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
    
        //Delete Category//
        [HttpGet]
        public ActionResult DeleteCategory(string id)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand("DeleteCategory", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Category", SqlDbType.VarChar);
                    command.Parameters["@Category"].Value = id;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();                    
                Debug.WriteLine("Categoria eliminada");
              
            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }
            return RedirectToAction("Index","Categories");
        }

        //Redirect to edit the category//
        public ActionResult EditCategory(int? id)
        {
            if (id != null)
            {
                var x = GetCategories().Find(elem => elem.idcategorie == id);
                return View(x);               
            }
            else
            {
                return RedirectToAction("Index", "Categories");
            }           
        }

        //Update catgory method//
        [HttpPost]
        public ActionResult EditCategory(Categorie model)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                var x = GetCategories().Find(elem => elem.idcategorie == model.idcategorie);
                if (x != null)
                {
                    SqlConnection sqlConnection = new SqlConnection(cs);
                    using (SqlCommand command = new SqlCommand("UpdateCategory", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idcate", SqlDbType.Int);
                        command.Parameters["@idcate"].Value = model.idcategorie;
                        command.Parameters.Add("@categoria", SqlDbType.VarChar);
                        command.Parameters["@categoria"].Value = model.categorie;
                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                    Debug.WriteLine("Categoria editada");
                }
                else
                {
                    return RedirectToAction("EditCategory", "Categories");
                }               

            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }
            return RedirectToAction("Index", "Categories");
            
        }

    }
}