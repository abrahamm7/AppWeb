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
    public class CreateCategoryController : Controller
    {
        ICategoryModule Data = new CategoryModule();

        // GET: CreateCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (!string.IsNullOrEmpty(category.CategoryName) || !string.IsNullOrEmpty(category.DescriptionCategory))
            {
                category.DateCreated = DateTime.Now;
                Data.InsertCategory(category);
                return RedirectToAction("Index", "Categories");
            }
            else
            {
                return View();
            }
        }
    }
}