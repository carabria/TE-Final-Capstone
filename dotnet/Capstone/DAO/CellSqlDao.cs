using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using Capstone.Models;

namespace Capstone.DAO
{
    
    public class CellSqlDao : ICellDao
    {
        private readonly string connectionString;
       
        public CellSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        
        public List<Cell> getCells()
        {
            List<Cell> cells = new List<Cell>();
            string sql = "SELECT cell_id, x_cord, y_cord, letter_x, letter_y, color, acid FROM cells;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Cell cell = cell_mapper(reader);
                        cells.Add(cell);
                    }
                }
            } catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return cells;
        }

        public List<Cell> getPossibleCells(string letters)
        {
            List<Cell> cells = new List<Cell>();
            //Note(anita): Grab the Y letter and find all the possible X's
            string letterY = letters.Substring(0, 1);
            string sql = "SELECT cell_id, x_cord, y_cord, letter_x, letter_y, color, acid FROM cells WHERE letter_y = @letterY;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@letterY", letterY);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cell cell = cell_mapper(reader);
                        cells.Add(cell);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return cells;
        }

        public List<Cell> getIdealizedCells(string letters)
        {
            List<Cell> cells = new List<Cell>();
            
            
            
            return cells;
        }

        private List<Cell> reduce_cells(List<Cell> cells)
        {
            List<Cell> reduced_cells = new List<Cell>();
            
            return reduced_cells;
            
        }

        

        public List<Cell> getCellByLetters(string letters)
        {
            List<Cell> cells = new List<Cell>();
            
            //Note(anita): This looks a bit weird but the look up is done by letter, not by cord
            //             and we look up Y first due to how the database is set up
            string letterY = letters.Substring(0, 1);
            string letterX = letters.Substring(1, 1);
            
            string sql = "SELECT cell_id, x_cord, y_cord, letter_x, letter_y, color, acid FROM cells WHERE letter_y = @letterY AND letter_x = @letterX;";
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@letterY", letterY);
                    cmd.Parameters.AddWithValue("@letterX", letterX);
                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Cell cell = cell_mapper(reader);
                        cells.Add(cell);
                    }
                }
            } catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }


            return cells;
        }

        private Cell cell_mapper(SqlDataReader reader)
        {
            Cell cell = new Cell
            {
                CellId = Convert.ToInt32(reader["cell_id"]),
                CordX = Convert.ToInt32(reader["x_cord"]),
                CordY = Convert.ToInt32(reader["y_cord"]),
                LetterX = Convert.ToString(reader["letter_x"]),
                LetterY = Convert.ToString(reader["letter_y"]),
                Color = Convert.ToString(reader["color"]),
                Acid = Convert.ToString(reader["acid"])
            };
            return cell;
        }
    }
}