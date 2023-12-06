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
    public class ProteinSqlDao : IProteinDao
    {
        private readonly string connectionString;

        public ProteinSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public IList<Protein> GetProteins()
        {
            IList<Protein> proteins = new List<Protein>();

            string sql = "SELECT protein_id, sequence_name, protein_sequence, description, format_type, user_id FROM proteins";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Protein protein = MapRowToProtein(reader);
                        proteins.Add(protein);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return proteins;
        }

        public Protein GetProteinById(int id)
        {
            Protein protein = null;

            string sql = "SELECT protein_id, sequence_name, protein_sequence, description, format_type, user_id " +
                "FROM proteins " +
                "WHERE protein_id = @protein_id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@protein_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        protein = MapRowToProtein(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return protein;
        }

        public IList<Protein> GetProteinsBySequenceName(string sequenceName)
        {
            {
                IList<Protein> proteins = new List<Protein>();
                string sql = "SELECT protein_id, sequence_name, protein_sequence, description, format_type, user_id " +
                    "FROM proteins " +
                    "WHERE sequence_name = @sequence_name";

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@sequence_name", sequenceName);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Protein protein = MapRowToProtein(reader);
                            proteins.Add(protein);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new DaoException("SQL exception occurred", ex);
                }

                return proteins;
            }
        }

        public IList<Protein> GetProteinsByUserId(int id)
        {
            IList<Protein> proteins = new List<Protein>();
            string sql = "SELECT protein_id, sequence_name, protein_sequence, description, format_type, user_id " +
                "FROM proteins " +
                "WHERE user_id = @user_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Protein protein = MapRowToProtein(reader);
                        proteins.Add(protein);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return proteins;
        }

        public Protein CreateProtein(string sequenceName, string proteinSequence, string description, int userId)
        {
            Protein newProtein = null;
            int formatType = DetectFormat(proteinSequence);
            string sql = "INSERT INTO proteins (sequence_name, protein_sequence, description, format_type, user_id) " +
                "OUTPUT INSERTED.protein_id " +
                "VALUES (@sequence_name, @protein_sequence, @description, @format_type, @user_id)";
            int newProteinId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@sequence_name", sequenceName);
                    cmd.Parameters.AddWithValue("@protein_sequence", proteinSequence);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@format_type", formatType);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    newProteinId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                newProtein = GetProteinById(newProteinId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newProtein;
        }

        public Protein UpdateProtein(int proteinId, string sequenceName, string proteinSequence, string description, int userId)
        {
            Protein updatedProtein = null;
            int formatType = DetectFormat(proteinSequence);
            string sql = "UPDATE proteins " +
            "SET sequence_name = @sequence_name, protein_sequence = @protein_sequence, description = @description, format_type = @format_type, user_id = @user_id " +
            "WHERE protein_id = @protein_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@sequence_name", sequenceName);
                    cmd.Parameters.AddWithValue("@protein_sequence", proteinSequence);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@format_type", formatType);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@protein_id", proteinId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            updatedProtein = GetProteinById(proteinId);
            return updatedProtein;
        }

        public bool DeleteProteinById(int proteinId)
        {
            Protein protein = GetProteinById(proteinId);
            if (protein == null)
            {
                return false;
            }
            bool result = false;
            string sql = "DELETE FROM proteins " +
            "WHERE protein_id = @protein_id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@protein_id", proteinId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            result = true;
            return result;
        }

        public int DetectFormat(string sequence)
        {
            int result = 0;
            if (Char.IsDigit(sequence, 0))
            {
                result = 3;
            }
            else if (sequence.IndexOf(" ") == 10)
            {
                result = 2;
            }
            else
            {
                result = 1;
            }
            return result;
        }
        private Protein MapRowToProtein(SqlDataReader reader)
        {
            Protein protein = new Protein();
            protein.ProteinId = Convert.ToInt32(reader["protein_id"]);
            protein.SequenceName = Convert.ToString(reader["sequence_name"]);
            protein.ProteinSequence = Convert.ToString(reader["protein_sequence"]);
            protein.Description = Convert.ToString(reader["description"]);
            protein.FormatType = Convert.ToInt32(reader["format_type"]);
            protein.UserId = Convert.ToInt32(reader["user_id"]);
            return protein;
        }
    }
}
