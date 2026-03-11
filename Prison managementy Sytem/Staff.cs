using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prison_managementy_Sytem.Model;
using Prison_managementy_Sytem.Repo;

namespace Prison_managementy_Sytem
{
    public partial class Staff : Form
    {
        StaffRepo repo = new StaffRepo();
        public Staff()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetAllData();
        }

        public List<StaffModel> GetAllData()
        {
            return repo.GetAllStaff();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }
        // Adding staff data to the table
        private void button1_Click(object sender, EventArgs e)
        {
            // Validating fields 
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please fill in all staff details (Name, Role, Phone, and Shift).",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create the StaffModel object using the data from the UI
                StaffModel newStaff = new StaffModel
                {
                    FullName = textBox2.Text,
                    Role = textBox3.Text,
                    Phone = textBox4.Text,
                    Shift = textBox5.Text
                };

                // Call the Repo to save to the database
                repo.AddStaff(newStaff);

                //  Success feedback and clear the form
                MessageBox.Show("Staff member added successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to clear textboxes after saving
        private void ClearFields()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            // Focus back to the first textbox for the next entry
            textBox2.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }

}
    
