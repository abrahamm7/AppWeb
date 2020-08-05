using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.Services
{
    public interface IDataAccessDB
    {
        List<User> GetAllUsers();
        List<Product> GetAllProducts();
        List<Category> GetAllCategories();
        void InsertCategory(Category category);
        void DeleteCategory(string category);
        void EditCategory(Category category);
        void DeleteUser(int id);
        void EditUser(User user);
        void InsertUser(User user);
        void InsertProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(Product product);
        void ProcessPurchase(int user, int id);
        List<Factura> ItemsPurchased();
    }
}
