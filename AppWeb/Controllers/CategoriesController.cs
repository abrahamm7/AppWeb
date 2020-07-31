using AppWeb.Models;
using AppWeb.Services;
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
       
        Category Category = new Category();
        DataAccessDB Data = new DataDB();
        // GET: Categories
        public ActionResult Index()
        {
            string btnclick = Request["btncategory"];
            if (btnclick == "Add")
            {
                var text = Request["categorytxt"];
                if (!string.IsNullOrEmpty(text))
                {
                    Category.Categoria = text;
                }
                Data.InsertCategory(Category);                
            }
            try
            {
                var obtaincategories = Data.GetAllCategories();
                if (obtaincategories.Count != 0)
                {
                    return View(obtaincategories);
                }
                else
                {
                    return View();
                }               
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return View();
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
                var x = Data.GetAllCategories().Find(elem => elem.CategoriaId == id);
                return View(x);               
            }
            else
            {
                return RedirectToAction("Index", "Categories");
            }           
        }

        //Update catgory method//
        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                var x = Data.GetAllCategories().Find(elem => elem.CategoriaId == model.CategoriaId);
                if (x != null)
                {
                    SqlConnection sqlConnection = new SqlConnection(cs);
                    using (SqlCommand command = new SqlCommand("UpdateCategory", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@idcate", SqlDbType.Int);
                        command.Parameters["@idcate"].Value = model.CategoriaId;
                        command.Parameters.Add("@categoria", SqlDbType.VarChar);
                        command.Parameters["@categoria"].Value = model.Categoria;
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