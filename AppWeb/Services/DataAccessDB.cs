using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.Services
{
    public interface DataAccessDB
    {
        List<User> GetAllUsers();
        List<Product> GetAllProducts();
        List<Category> GetAllCategories();
        void InsertCategory(Category category);
        void DeleteCategory(string category);
    }
}
