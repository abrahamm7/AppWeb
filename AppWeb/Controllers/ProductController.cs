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
    public class ProductController : Controller
    {
        private List<SelectListItem> selectListItems;
        // GET: Product
        public ActionResult Index()
        {
            var x = GetProducts();           
            return View(x);
        }
        
        //Get all products from db//
        public List<Product> GetProducts()
        {
            try
            {
                List<Product> products = new List<Product>();
                string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    using (SqlCommand command = new SqlCommand("GetAllProducts", con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Product product = new Product();

                                    if (reader[0] != System.DBNull.Value)
                                    {
                                        product.IDproduct = Convert.ToInt32(reader[0]);
                                    }
                                    if (reader[1] != System.DBNull.Value)
                                    {
                                        product.IDCategory = Convert.ToInt32(reader[1]);
                                    }
                                    if (reader[2] != System.DBNull.Value)
                                    {
                                        product.Name = reader[2].ToString();
                                    }
                                    if (reader[3] != System.DBNull.Value)
                                    {
                                        product.Price = reader[3].ToString();
                                    }
                                    if (reader[4] != System.DBNull.Value)
                                    {
                                        product.IDCategoryC = Convert.ToInt32(reader[4]);
                                    }
                                    if (reader[5] != System.DBNull.Value)
                                    {
                                        product.Category = reader[5].ToString();
                                    }
                                    products.Add(product);
                                }
                            }
                        }
                    }
                    con.Close();
                    return products;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
       
        //Get all categories from db//
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
       
        //Insert product in db//
        public void InsertProduct(Product product)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand("InsertProduct", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@name", SqlDbType.VarChar);
                    command.Parameters["@name"].Value = product.Name;
                    command.Parameters.Add("@price", SqlDbType.Int);
                    command.Parameters["@price"].Value = product.Price;
                    command.Parameters.Add("@category", SqlDbType.Int);
                    command.Parameters["@category"].Value = product.IDCategory;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
                Debug.WriteLine("Producto agregado");
                RedirectToAction("Index", "Product");
            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }
            
        }    
        public ActionResult NewProduct()
        {
            Product product = new Product();
            var xy = GetCategories();
            var obj = xy;
            selectListItems = new List<SelectListItem>();
            foreach (var item in obj)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = item.categorie,
                    Value = item.idcategorie.ToString()
                });
            }
            ViewBag.categ = selectListItems;
            try
            {
                string btnclick = Request["addproduct"];
                if (btnclick == "Create")
                {                  
                    product.Name  = Request["nametxt"];
                    product.Price = Request["pricetxt"];
                    var p = selectListItems.Select(elem => elem.Value).ToList();
                    product.IDCategory = Convert.ToInt32(p.FirstOrDefault());
                    InsertProduct(product);                    
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en: {ex.Message}");
            }
            return View();
        }
        //Delete product in db//
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand("DeleteProduct", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@idproduct", SqlDbType.Int);
                    command.Parameters["@idproduct"].Value = id;
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
                Debug.WriteLine("Producto eliminado");

            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }
            return RedirectToAction("Index", "Product");
        }
        
        //Edit product//
        public ActionResult EditProduct(int? id)
        {
            if (id != null)
            {
                var x = GetProducts().Find(elem => elem.IDproduct == id);
                return View(x);
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
        
        //Edit product in db//
        [HttpPost]
        public ActionResult EditProduct(Product model)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            try
            {
                SqlConnection sqlConnection = new SqlConnection(cs);
                using (SqlCommand command = new SqlCommand("UpdateProduct", sqlConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@idproduct", SqlDbType.Int);
                    command.Parameters["@idproduct"].Value = model.IDproduct;
                    command.Parameters.Add("@name", SqlDbType.VarChar);
                    command.Parameters["@name"].Value = model.Name;
                    command.Parameters.Add("@precio", SqlDbType.Int);
                    command.Parameters["@precio"].Value = model.Price;                
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
                Debug.WriteLine("Producto editado");

            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
            }
            return RedirectToAction("Index", "Product");
        }

    }
}