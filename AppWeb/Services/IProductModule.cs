using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.Services
{
    public interface IProductModule
    {
        List<Product> GetAllProducts(); //Get all products//
        void InsertProduct(Product product); //Insert product//
        void DeleteProduct(int id); //Delete Product//
        void UpdateProduct(Product product); //Update product//
        List<Product> FilterProductByCategories(string category); //Filter by category//
    }
}
