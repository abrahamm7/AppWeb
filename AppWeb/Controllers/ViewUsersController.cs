using AppWeb.Models;
using AppWeb.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class ViewUsersController : Controller
    {
        // GET: ViewUsers
        IProductModule ProductData = new ProductModule();
        IPurchase PurchaseData = new PurchaseModule();
        public ActionResult Index()
        {
            try
            {
                var obtainproducts = ProductData.GetAllProducts();
                string btnclick = Request["searchitem"];
                string txtinput = Request["itemtxt"];
                if (btnclick == "Search")
                {
                    var ProductResult = SearchItem(txtinput);

                    if (ProductResult.Count == 0)
                    {
                        Debug.WriteLine("Empty list");
                        return View(ProductResult);

                    }
                    else
                    {
                        return View(ProductResult);
                    }
                   
                }
                return View(obtainproducts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return View();
            }
        }
        [HttpGet]
        public ActionResult BuyProduct(int id) //Buy item//
        {
            try
            {
                HttpCookie reqCookies = Request.Cookies["cookie"];
                var userid = Convert.ToInt32(reqCookies["iduser"]);
                if (id != null && User != null)
                {
                    PurchaseData.ProcessPurchase(userid, id);
                    return RedirectToAction("Index", "ViewUsers");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RedirectToAction("Index", "ViewUsers");
            }
        }
   
        public List<Product> SearchItem(string input) //Search item in list//
        {
            var productsearch = ProductData.FilterProductByCategories(input);
            return productsearch;
        }
    }
}