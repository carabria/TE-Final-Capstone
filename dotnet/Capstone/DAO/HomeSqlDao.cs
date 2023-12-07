using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public Home GetViewById(int id)
        {

            string sql = "SELECT * FROM homeview WHERE view_id = @id";
            Home home = new Home();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id",id);
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
        public List<Home> GetAllViews()
        {
            string sql = "SELECT * FROM homeview";
            List<Home> homes = new List<Home>();
            try {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Home home = MapRowToHome(reader);
                        homes.Add(home);
                    }
                }
                    }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return homes;

        }
        public Home PostNewHomeView(Home data)
        {
            string sql = "INSERT INTO homeview (header, body, image_source, active) " +
            "OUTPUT INSERTED.view_id " +
            "VALUES (@header, @body, @image_source, @active) ";
            Home newHome = new Home();
            int newId = 0;
            data = NullPropertyToEmpty(data);
            try {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@header", data.header);
                    cmd.Parameters.AddWithValue("@body", data.body);
                    cmd.Parameters.AddWithValue("@image_source", data.image);
                    cmd.Parameters.AddWithValue("@active", false);
                    newId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                    newHome = GetViewById(newId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return newHome;

        } 
        public void UpdateHomeView(int newId)
        {
            string sql = "UPDATE homeview " +
             "SET active = 0 " +
             "WHERE active = 1; " +
             "GO"+
             "UPDATE homeview " +
             "SET active = 1 " +
             "WHERE view_id = @id ";
            try { 
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id",newId);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
        }
        private Home NullPropertyToEmpty(Home home)
        {
            if(home.active == null)
            {
                home.active = false;
            }
            if(home.body == null)
            {
                home.body = "empty";
            }
            if(home.header == null)
            {
                home.header = "empty";
            }
            if(home.image == null)
            {
                home.image = "src/img/Empty.jpg";
            }
            return home;
        }
        private Home MapRowToHome(SqlDataReader reader)
        {
            Home home = new Home();
            home.viewId = Convert.ToInt32(reader["view_id"]);
            home.body = Convert.ToString(reader["body"]);
            home.header = Convert.ToString(reader["header"]);
            home.image = Convert.ToString(reader["image_source"]);
            home.active = Convert.ToBoolean(reader["active"]);
            return home;
        }
    }
}
