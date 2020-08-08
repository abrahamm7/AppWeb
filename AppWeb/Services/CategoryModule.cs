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
    public class CategoryModule : ICategoryModule
    {
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
                    dbConnection.Execute($"InsertCategory '{category.CategoryName}','{category.DescriptionCategory}'");
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
                    dbConnection.Execute($"UpdateCategory '{category.IDCategory}', '{category.CategoryName}','{category.DescriptionCategory}'");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}