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
    public class UserSqlDao : IUserDao
    {
        private readonly string connectionString;

        public UserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<ReturnUser> GetUsers()
        {
            IList<ReturnUser> users = new List<ReturnUser>();

            string sql = "SELECT user_id, username, user_role FROM users";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReturnUser user = MapRowToReturnUser(reader);
                        users.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return users;
        }

        public User GetUserById(int userId)
        {
            User user = null;

            string sql = "SELECT user_id, username, password_hash, salt, user_role, one_time_password_hash, one_time_password_salt FROM users WHERE user_id = @user_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }


        public User GetFullUserByUsername(string username)
        {
            User user = null;

            string sql = "SELECT user_id, username, user_role, password_hash, salt, one_time_password_hash, one_time_password_salt FROM users WHERE username = @username";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }


        public ReturnUser GetUserByUsername(string username)
        {
            ReturnUser user = null;

            string sql = "SELECT user_id, username, user_role FROM users WHERE username = @username";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToReturnUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }

        public User CreateUser(string username, string password, string role)
        {
            User newUser = new User();
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            string sql = "INSERT INTO users (username, password_hash, salt, user_role) " +
                         "OUTPUT INSERTED.user_id " +
                         "VALUES (@username, @password_hash, @salt, @user_role)";
            int newUserId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", role);
                    newUserId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                newUser = GetUserById(newUserId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return newUser;
        }

        public void ChangePassword(string username, string password)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            string sql = "UPDATE users " +
             "SET password_hash = @password_hash, salt = @salt, one_time_password_hash = NULL, one_time_password_salt = NULL " +
             "WHERE username = @username";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);

                    cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
        }

        public string GenerateOneTimePassword(string username)
        {
            Console.WriteLine(username);
            PasswordGenerator generator = new PasswordGenerator();
            IPasswordHasher passwordHasher = new PasswordHasher();

            string oneTimePassword = generator.oneTimeGenerator();
            PasswordHash hash = passwordHasher.ComputeHash(oneTimePassword);

            string sql = "UPDATE users " +
             "SET one_time_password_hash = @password_hash, one_time_password_salt = @salt " +
             "WHERE username = @username";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return oneTimePassword;
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            User user = new User();
            user.UserId = Convert.ToInt32(reader["user_id"]);
            user.Username = Convert.ToString(reader["username"]);
            user.Role = Convert.ToString(reader["user_role"]);
            user.PasswordHash = Convert.ToString(reader["password_hash"]);
            user.Salt = Convert.ToString(reader["salt"]);
            user.OneTimePasswordSalt = Convert.ToString(reader["one_time_password_salt"]);
            user.OneTimePasswordHash = Convert.ToString(reader["one_time_password_hash"]);
            return user;
        }

        private ReturnUser MapRowToReturnUser(SqlDataReader reader)
        {
            ReturnUser user = new ReturnUser();
            user.UserId = Convert.ToInt32(reader["user_id"]);
            user.Username = Convert.ToString(reader["username"]);
            user.Role = Convert.ToString(reader["user_role"]);
            return user;
        }
    }
}
