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
        IDataAccessDB Data = new DataDB();
        // GET: Product
        public ActionResult Index()
        {
            var obtainprodcuts = Data.GetAllProducts();
            return View(obtainprodcuts);
        }      
       
        public ActionResult NewProduct()
        {
            Product product = new Product();
            var obtaincategories = Data.GetAllCategories();
            selectListItems = new List<SelectListItem>();
            foreach (var item in obtaincategories)
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = (item.CategoriaId == item.CategoriaId),
                    Text = item.Categoria,
                    Value = item.CategoriaId.ToString()
                });
            }
            ViewBag.categ = selectListItems;
            try
            {
                string btnclick = Request["addproduct"];
                if (btnclick == "Create")
                {
                    product.Nombre = Request["nametxt"];
                    product.Precio = Request["pricetxt"];
                    product.Categoria = Request["category"];

                    //var p = selectListItems.Find(elem => elem.Value);
                    //product.CategoriaId = 
                    if (!string.IsNullOrEmpty(product.Nombre) || !string.IsNullOrEmpty(product.Precio) || !string.IsNullOrEmpty(product.CategoriaId.ToString()))
                    {
                        Data.InsertProduct(product);
                    }                   
                    return RedirectToAction("Index", "Product");
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
            try
            {
                Data.DeleteProduct(id);
                return RedirectToAction("Index", "Product");
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
                var x = Data.GetAllProducts().Find(elem => elem.ProductoId == id);
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