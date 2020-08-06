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
        IDataAccessDB Data = new DataDB();        
        public ActionResult Index()
        {
            try
            {
                var obtainproducts = Data.GetAllProducts();
                string btnclick = Request["searchitem"];
                string txtinput = Request["itemtxt"];
                if (btnclick == "Search")
                {
                    var ProductResult = SearchItem(obtainproducts, txtinput);
                    if (ProductResult.Count == 0)
                    {
                        Response.Write($"<script>alert('Category not found');</script>");

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
                    Data.ProcessPurchase(userid, id);
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
   
        public List<Product> SearchItem(List<Product> products, string input) //Search item in list//
        {
            var productsearch = products.Where(elem => elem.Categoria == input).ToList();
            return productsearch;
        }
    }
}