using AppWeb.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AppWeb.Services
{
    public class ProductModule: IProductModule
    {
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

        public List<Product> FilterProductByCategories(string category)
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
            {
                try
                {
                    var output = dbConnection.Query<Product>("FilterByProductCategory", CommandType.StoredProcedure).ToList();
                    return output;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return null;
                }
            }
        } //Filter product//
    }
}