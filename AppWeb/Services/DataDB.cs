using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace AppWeb.Services
{
    public class DataDB : IDataAccessDB
    {
        public List<User> GetAllUsers() //Obtain Users from DB//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    var output = dbConnection.Query<User>("GetAllUser", CommandType.StoredProcedure).ToList();
                    return output;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }                
            }
        }

        public List<Product> GetAllProducts() //Obtain Products from DB//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    var output = dbConnection.Query<Product>("GetAllProducts", CommandType.StoredProcedure).ToList();
                    return output;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public List<Category> GetAllCategories() //Obtain Categories from DB//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    var output = dbConnection.Query<Category>("GetAllCategories", CommandType.StoredProcedure).ToList();
                    return output;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    
        public void InsertCategory(Category category)  //Insert new Category//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {                    
                    dbConnection.Execute($"InsertCategory '{category.Categoria}','{category.Descripcion}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);                 
                }
            }
        }
    
        public void DeleteCategory(string category) //Delete Category//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"DeleteCategory '{category}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    
        public void EditCategory(Category category) //Update Category//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"UpdateCategory '{category.CategoriaId}', '{category.Categoria}','{category.Descripcion}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    
        public void DeleteUser(int id) //Delete User//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"DeleteUser '{id}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    
        public void EditUser(User user) //Edit User//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"UpdateUser '{user.UserId}', '{user.Nombre}','{user.Apellido}','{user.Clave}','{user.Usuario}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        } 
    
        public void InsertUser(User user) //Insert user//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"InsertUser '{user.Nombre}', '{user.Apellido}','{user.Usuario}','{user.Clave}', '{user.Rol}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public void InsertProduct(Product product) //Insert Product//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"InsertProduct '{product.Nombre}', '{product.Precio}','{product.CategoriaId}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteProduct(int product) //Delete Product//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"DeleteProduct '{product}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        public void UpdateProduct(Product product) //Update Product//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"UpdateProduct '{product.ProductoId}','{product.Nombre}','{product.Precio}','{product.CategoriaId}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
        public void ProcessPurchase(int user, int id) //Purchase Product//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    dbConnection.Execute($"PurchaseItems '{user}', '{id}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        public List<Factura> ItemsPurchased() //Get items purchased//
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    var output = dbConnection.Query<Factura>("Receips", CommandType.StoredProcedure).ToList();
                    return output;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        }


    }
}