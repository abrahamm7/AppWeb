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
    public class DataDB : DataAccessDB
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
                    dbConnection.Execute($"InsertCategory '{category.Categoria}'");
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
    }
}