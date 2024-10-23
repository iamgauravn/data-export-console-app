using DataExporter.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace DataExporter.Implementation
{
    public class DbService : IdbService
    {
        public List<string> GetTableNames(string _conectionString)
        {
            using (SqlConnection connection = new SqlConnection(_conectionString))
            {
                connection.Open();
                return connection.Query<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'").ToList();
            }
        }

        public bool IsValidConnectionString(string _connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                }
                return true; 
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
