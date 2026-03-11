using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using Prison_managementy_Sytem.Model;

namespace Prison_managementy_Sytem.Repo
{
    internal class StaffRepo
    {
        private readonly string connectionString = Util.connection();

        public StaffRepo()
        {
            connectionString = Util.connection();
        }

        // Add new staff
        public void AddStaff(StaffModel staff)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO Staff (FullName, Role, Phone, Shift)
                                     VALUES (@FullName, @Role, @Phone, @Shift)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = (object)staff.FullName ?? DBNull.Value;
                        cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = (object)staff.Role ?? DBNull.Value;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)staff.Phone ?? DBNull.Value;
                        cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 20).Value = (object)staff.Shift ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in AddStaff:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in AddStaff:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update existing staff
        public void UpdateStaff(StaffModel staff)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Staff
                                     SET FullName=@FullName, Role=@Role, Phone=@Phone, Shift=@Shift
                                     WHERE StaffID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = (object)staff.FullName ?? DBNull.Value;
                        cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value = (object)staff.Role ?? DBNull.Value;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)staff.Phone ?? DBNull.Value;
                        cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 20).Value = (object)staff.Shift ?? DBNull.Value;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = staff.StaffID;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in UpdateStaff:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in UpdateStaff:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete staff by ID
        public void DeleteStaff(int staffID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM Staff WHERE StaffID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = staffID;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in DeleteStaff:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in DeleteStaff:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get all staff
        public List<StaffModel> GetAllStaff()
        {
            List<StaffModel> staffList = new List<StaffModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Staff";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            staffList.Add(new StaffModel
                            {
                                StaffID = Convert.ToInt32(dr["StaffID"]),
                                FullName = dr["FullName"] != DBNull.Value ? dr["FullName"].ToString() : null,
                                Role = dr["Role"] != DBNull.Value ? dr["Role"].ToString() : null,
                                Phone = dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : null,
                                Shift = dr["Shift"] != DBNull.Value ? dr["Shift"].ToString() : null
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in GetAllStaff:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in GetAllStaff:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return staffList;
        }
    }
}
