using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Prison_managementy_Sytem.Model;


namespace Prison_managementy_Sytem.Repo
{
    internal class VisitorsRepo
    {
        private readonly string connectionString = Util.connection();

        // Add a new visitor
        public void AddVisitor(VisitorsModel visitor)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO Visitors (VisitorName, CNIC, Phone)
                                     VALUES (@Name, @CNIC, @Phone)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = (object)visitor.VisitorName ?? DBNull.Value;
                        cmd.Parameters.Add("@CNIC", SqlDbType.NVarChar, 20).Value = (object)visitor.CNIC ?? DBNull.Value;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)visitor.Phone ?? DBNull.Value;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in AddVisitor:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in AddVisitor:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Update existing visitor
        public void UpdateVisitor(VisitorsModel visitor)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Visitors
                                     SET VisitorName=@Name, CNIC=@CNIC, Phone=@Phone
                                     WHERE VisitorID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = (object)visitor.VisitorName ?? DBNull.Value;
                        cmd.Parameters.Add("@CNIC", SqlDbType.NVarChar, 20).Value = (object)visitor.CNIC ?? DBNull.Value;
                        cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = (object)visitor.Phone ?? DBNull.Value;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = visitor.VisitorID;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in UpdateVisitor:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in UpdateVisitor:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete a visitor by ID
        public void DeleteVisitor(int visitorID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM Visitors WHERE VisitorID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = visitorID;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in DeleteVisitor:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in DeleteVisitor:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get all visitors
        public List<VisitorsModel> GetAllVisitors()
        {
            List<VisitorsModel> visitors = new List<VisitorsModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Visitors";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            visitors.Add(new VisitorsModel
                            {
                                VisitorID = Convert.ToInt32(dr["VisitorID"]),
                                VisitorName = dr["VisitorName"] != DBNull.Value ? dr["VisitorName"].ToString() : null,
                                CNIC = dr["CNIC"] != DBNull.Value ? dr["CNIC"].ToString() : null,
                                Phone = dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : null
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error in GetAllVisitors:\n{ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in GetAllVisitors:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return visitors;
        }
    }
}
