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
        IProductModule Data = new ProductModule();
        ICategoryModule CategoryData = new CategoryModule();
        // GET: Product
        public ActionResult Index()
        {
            try
            {
                var obtainprodcuts = Data.GetAllProducts();
                return View(obtainprodcuts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }      
       
        //New product//
        public ActionResult NewProduct()
        {           
            Product product = new Product();
            FillDropDownList();
            try
            {
                string btnclick = Request["addproduct"];
                if (btnclick == "Create")
                {
                    product.NameProduct = Request["nametxt"];
                    product.PriceProduct = Request["pricetxt"];
                    product.Stock = Convert.ToInt32(Request["stocktxt"]);
                    product.IDCategory = Convert.ToInt32(Request["category"]);

                    if (!string.IsNullOrEmpty(product.NameProduct) && !string.IsNullOrEmpty(product.Stock.ToString()) && !string.IsNullOrEmpty(product.PriceProduct) && !string.IsNullOrEmpty(product.IDCategory.ToString()))
                    {
                        Data.InsertProduct(product);
                        return RedirectToAction("Index", "Product");
                    }
                    return View();
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
                if (id != null)
                {
                    Data.DeleteProduct(id);
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Product");

            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
                return RedirectToAction("Index", "Product");
            }            
        }
        
        //Edit product//
        public ActionResult EditProduct(int? id)
        {
            try
            {
                FillDropDownList();

                if (id != null)
                {
                    var x = Data.GetAllProducts().Find(elem => elem.IDProduct == id);
                    return View(x);
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
                return RedirectToAction("Index", "Product");
            }
        }
        
        //Edit product in db//
        [HttpPost]
        public ActionResult EditProduct(Product model)
        {
            try
            {
                model.IDCategory = Convert.ToInt32(Request["category"]);
                if (model.CategoryName != null && model.PriceProduct != null && model.IDCategory != 0)
                {
                    Data.UpdateProduct(model);
                }
                else
                {
                   return RedirectToAction("Index", "Product");
                }
                return RedirectToAction("Index", "Product");
            }
            catch (Exception ea)
            {
                Debug.WriteLine($"Error: {ea.Message}");
                return RedirectToAction("Index", "Product");
            }
            
        }
        //Fill Dropdown//
        public void FillDropDownList()
        {
            var obtaincategories = CategoryData.GetAllCategories();
            selectListItems = new List<SelectListItem>();
            foreach (var item in obtaincategories)
            {
                selectListItems.Add(new SelectListItem
                {
                    Selected = true,
                    Text = item.CategoryName,
                    Value = item.IDCategory.ToString()
                });
            }
            ViewBag.categ = selectListItems;
        }
    }
}