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
    public class ProductController : Controller
    {
        private List<SelectListItem> selectListItems;
        DataAccessDB Data = new DataDB();
        // GET: Product
        public ActionResult Index()
        {
            var obtainprodcuts = Data.GetAllProducts();
            return View(obtainprodcuts);
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
                    command.Parameters["@name"].Value = product.Nombre;
                    command.Parameters.Add("@price", SqlDbType.Int);
                    command.Parameters["@price"].Value = product.Precio;
                    command.Parameters.Add("@category", SqlDbType.Int);
                    command.Parameters["@category"].Value = product.CategoriaId;
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
            //Product product = new Product();
            //var xy = GetCategories();
            //var obj = xy;
            //selectListItems = new List<SelectListItem>();
            //foreach (var item in obj)
            //{
            //    selectListItems.Add(new SelectListItem
            //    {
            //        Text = item.categorie,
            //        Value = item.idcategorie.ToString()
            //    });
            //}
            //ViewBag.categ = selectListItems;
            //try
            //{
            //    string btnclick = Request["addproduct"];
            //    if (btnclick == "Create")
            //    {                  
            //        product.Nombre = Request["nametxt"];
            //        product.Precio = Request["pricetxt"];
            //        var p = selectListItems.Select(elem => elem.Value).ToList();
            //        product.CategoriaId = Convert.ToInt32(p.FirstOrDefault());
            //        InsertProduct(product);                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"Error en: {ex.Message}");
            //}
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
            //if (id != null)
            //{
            //    var x = GetProducts().Find(elem => elem.IDproduct == id);
            //    return View(x);
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Product");
            //}
            return View();
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
                    command.Parameters["@idproduct"].Value = model.ProductoId;
                    command.Parameters.Add("@name", SqlDbType.VarChar);
                    command.Parameters["@name"].Value = model.Nombre;
                    command.Parameters.Add("@precio", SqlDbType.Int);
                    command.Parameters["@precio"].Value = model.Precio;                
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