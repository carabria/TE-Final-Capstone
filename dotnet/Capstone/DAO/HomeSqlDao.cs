using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Identity;

namespace Capstone.DAO
{
    public class HomeSqlDao : IHomeDao
    {
        private readonly string connectionString;

        public HomeSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Home GetView()
        {
            
            string sql = "SELECT * FROM homeview WHERE active = 1";
            Home home = new Home();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        home = MapRowToHome(reader);
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return home;
        }
        private Home MapRowToHome(SqlDataReader reader)
        {
            Home home = new Home();
            home.viewId = Convert.ToInt32(reader["view_id"]);
            home.app = Convert.ToString(reader["app"]);
            home.company = Convert.ToString(reader["company"]);
            home.image = Convert.ToString(reader["image_source"]);
            home.active = Convert.ToBoolean(reader["active"]);
            return home;
        }
    }
}
