using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.Services
{
    public interface ICategoryModule
    {
        List<Category> GetAllCategories(); //Get all categories//
        void InsertCategory(Category category); //Insert category//
        void DeleteCategory(string category); //delete category//
        void EditCategory(Category category); //Edit category// 

    }
}
