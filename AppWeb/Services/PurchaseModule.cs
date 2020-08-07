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
    public class PurchaseModule: IPurchase
    {
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

    }
}