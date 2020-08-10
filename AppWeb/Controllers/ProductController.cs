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
       
       
        public ActionResult NewProduct()   //New product//
        {           
            FillDropDownList();
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(Product product) //Post new product//
        {
            FillDropDownList();
            product.IDCategory = Convert.ToInt32(Request["category"]);
            if (string.IsNullOrEmpty(product.NameProduct) ||
                product.Stock == 0 ||
                string.IsNullOrEmpty(product.PriceProduct) ||
                product.IDCategory == 0)
            {
                ViewBag.Message = "Empty fields";
                return View();
            }
            else
            {
                Data.InsertProduct(product); //Method for insert new product in DB//
                ViewBag.Message = "Success";
                return View();
            }
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