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
        List<User> GetAllUsers(); //Get all users//
        List<Product> GetAllProducts(); //Get all products//
        List<Category> GetAllCategories(); //Get all categories//
        void InsertCategory(Category category); //Insert category//
        void DeleteCategory(string category); //delete category//
        void EditCategory(Category category); //Edit category//
        void DeleteUser(int id); //Delete user//
        void EditUser(User user); //Edit user//
        void InsertUser(User user); //Insert user//
        void InsertProduct(Product product); //Insert product//
        void DeleteProduct(int id); //Delete Product//
        void UpdateProduct(Product product); //Update product//
        void ProcessPurchase(int user, int id); //Purchase item//
        List<Receip> ItemsPurchased(); //Get all items purchased//
    }
}
