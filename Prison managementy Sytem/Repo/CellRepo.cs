using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Prison_managementy_Sytem.Model;


namespace Prison_managementy_Sytem.Repo
{
    internal class CellRepo
    {
        private readonly string connectionString = Util.connection();

        // Add a new cell
        public void AddCell(CellModel cell)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO Cells (Block, CellNumber, Capacity, CurrentOccupancy)
                                     VALUES (@Block, @CellNumber, @Capacity, @CurrentOccupancy)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@Block", SqlDbType.NVarChar, 50).Value = (object)cell.Block ?? DBNull.Value;
                        cmd.Parameters.Add("@CellNumber", SqlDbType.NVarChar, 50).Value = (object)cell.CellNumber ?? DBNull.Value;
                        cmd.Parameters.Add("@Capacity", SqlDbType.Int).Value = cell.Capacity;
                        cmd.Parameters.Add("@CurrentOccupancy", SqlDbType.Int).Value = cell.CurrentOccupancy;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in AddCell:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in AddCell:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update an existing cell
        public void UpdateCell(CellModel cell)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Cells
                                     SET Block=@Block, CellNumber=@CellNumber, Capacity=@Capacity, CurrentOccupancy=@CurrentOccupancy
                                     WHERE CellID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@Block", SqlDbType.NVarChar, 50).Value = (object)cell.Block ?? DBNull.Value;
                        cmd.Parameters.Add("@CellNumber", SqlDbType.NVarChar, 50).Value = (object)cell.CellNumber ?? DBNull.Value;
                        cmd.Parameters.Add("@Capacity", SqlDbType.Int).Value = cell.Capacity;
                        cmd.Parameters.Add("@CurrentOccupancy", SqlDbType.Int).Value = cell.CurrentOccupancy;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = cell.CellID;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in UpdateCell:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in UpdateCell:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete a cell by ID
        public void DeleteCell(int cellID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM Cells WHERE CellID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = cellID;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in DeleteCell:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in DeleteCell:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get all cells
        public List<CellModel> GetAllCells()
        {
            List<CellModel> cells = new List<CellModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Cells";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cells.Add(new CellModel
                            {
                                CellID = Convert.ToInt32(dr["CellID"]),
                                Block = dr["Block"] != DBNull.Value ? dr["Block"].ToString() : null,
                                CellNumber = dr["CellNumber"] != DBNull.Value ? dr["CellNumber"].ToString() : null,
                                Capacity = dr["Capacity"] != DBNull.Value ? Convert.ToInt32(dr["Capacity"]) : 0,
                                CurrentOccupancy = dr["CurrentOccupancy"] != DBNull.Value ? Convert.ToInt32(dr["CurrentOccupancy"]) : 0
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in GetAllCells:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in GetAllCells:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cells;
        }
    }
}
