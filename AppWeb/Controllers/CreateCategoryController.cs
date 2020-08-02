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
        IDataAccessDB Data = new DataDB();
        Category category = new Category();

        // GET: CreateCategory
        public ActionResult Index()
        {
            try
            {
                string btnclick = Request["createcategory"];
                if (btnclick == "Create")
                {
                    category.Categoria = Request["categorytxt"];
                    category.Descripcion = Request["desctxt"];

                    if (!string.IsNullOrEmpty(category.Categoria) || !string.IsNullOrEmpty(category.Descripcion))
                    {
                        Data.InsertCategory(category);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                return View();
            }
            return View();
        }
    }
}