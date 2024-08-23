using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace AssetStatusInfo
{
    internal static class DatabaseQueries
    {
        public static readonly String defaultLocation = "%";
        //queries
        private static string stockItemRoot = AllSettings.Default.stockQueryRoot;
        private static string stockItem = $"SELECT {stockItemRoot}";
        private static string schema = $"SELECT TOP(1) {stockItemRoot}";
        private static string nonStock = AllSettings.Default.nonStockQuery;
        private static string phones = AllSettings.Default.phoneQuery;
        private static string locationQuery = AllSettings.Default.locationQuery;
        private static string connectionString = AllSettings.Default.connectionString;

        private static string[] locationHistoryConfig = AllSettings.Default.GetsStockItems.Split("|");

        public static List<String> PrimaryKeys(String tableName)
        {
            string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
            DataSet columns = GetDatabaseQuery(query, null);
            List<String> columnList = new List<String>();
            foreach (DataRow row in columns.Tables[0].Rows)
            {
                foreach (DataColumn column in columns.Tables[0].Columns)
                {
                    columnList.Add(row[column].ToString());
                }
            }
            return columnList;
        }
        public static DataSet GetDatabaseQuery(String query, DataSet? ds, bool schemaOnly = false)
        {
            SqlConnection connectionName = connection();
            SqlDataAdapter adapter = new SqlDataAdapter(new SqlCommand(query, connectionName));
            if (ds == null)
            {
                ds = new System.Data.DataSet();
            }
            if (schemaOnly)
            {
                adapter.FillSchema(ds, SchemaType.Source);
            }
            else
            {
                adapter.Fill(ds);
            }
            connectionName.Close();
            return ds;
        }
        public static SqlConnection connection()
        {
            SqlConnection connectionName = new SqlConnection(connectionString);
            //MessageBox.Show("Connection Open!");
            connectionName.Open();
            return connectionName;
        }
        public static void Upsert(string query)
        {
            SqlConnection connectionName = connection();
            SqlCommand command = new SqlCommand(query, connectionName);
            try
            {
                command.ExecuteNonQuery();
            }
            catch(Microsoft.Data.SqlClient.SqlException ex)
            {
                //primary key violations are fine
            }
            connectionName.Close();
        }

        public static List<String> GetLocations()
        {
            System.Data.DataSet ds = GetDatabaseQuery(locationQuery, new System.Data.DataSet());
            List<String> returnList = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                returnList.Add(row.ItemArray[0].ToString());
            }
            returnList.Add("All Sites");
            return returnList;
        }
        public static System.Data.DataTable GetHistory(string location, bool allItems = false)
        {
            string nonTaggedQuery = DatabaseQueries.nonStock;
            string taggedQuery = DatabaseQueries.stockItem;
            string phones = DatabaseQueries.phones;
            string amountField = AllSettings.Default.AmountFieldName + " > 0 AND ";
            if (allItems)
            {
                nonTaggedQuery = DatabaseQueries.nonStock.Replace(amountField, "");
                taggedQuery = DatabaseQueries.stockItem.Replace(amountField, "");
                phones = DatabaseQueries.phones.Replace(amountField, "");
            }
            //should change this to a list I loop through or a dict or something
            System.Data.DataSet ds = new System.Data.DataSet();
            string locationSql;
            if (AllSettings.Default.CompoundLocationList.Contains(location))
            {
                locationSql = $"{location}'";
                foreach (string loc in AllSettings.Default.CompoundLocationList.Split("|"))
                {
                    locationSql += $" OR location = '{loc}'";
                }
            }
            else
            {
                locationSql = location + "'";
            }
            ds = GetDatabaseQuery((nonTaggedQuery + locationSql), ds);
            if (locationHistoryConfig.Contains(location))
            {
                ds = GetDatabaseQuery((taggedQuery + locationSql), ds);
            }
            ds = GetDatabaseQuery((phones + locationSql), ds);
            return ds.Tables[0];
        }
        public static System.Data.DataTable GetSchema(String location)
        {
            DataSet ds = GetDatabaseQuery((DatabaseQueries.schema + location + "'"), null, true);
            return ds.Tables[0];
        }
    }
}

