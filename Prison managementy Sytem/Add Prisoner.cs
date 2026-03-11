using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Prison_managementy_Sytem.Model;
using Prison_managementy_Sytem.Repo;

namespace Prison_managementy_Sytem
{
    public partial class Add_Prisoner : Form
    {
        PrisonerRepo repo = new PrisonerRepo();
        public Add_Prisoner()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetAllData();
        }

        public List<PrisonerModel> GetAllData()
        {
            return repo.GetAllPrisoners(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }


        //Add Prisoner
        private void button1_Click(object sender, EventArgs e)
        {
            //  Basic Validation
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            try
            {
                // Create the model and parse the data types
                PrisonerModel model = new PrisonerModel
                {
                    FirstName = textBox2.Text,
                    LastName = textBox3.Text,
                    Gender = textBox4.Text,
                    DOB = DateTime.Parse(textBox5.Text),
                    EntryDate = DateTime.Parse(textBox6.Text),
                    ReleaseDate = DateTime.Parse(textBox7.Text),
                    Status = textBox8.Text,
                    CellID = int.Parse(textBox9.Text),

                    // Initialize empty lists to prevent null reference errors in the Repo
                    Crimes = new List<Crime>(),
                    Transfers = new List<Transfer>(),
                    Visits = new List<Visit>()
                };

                // Call the Repository to save to Database
                repo.AddPrisonerWithDetails(model);

                // Success Message and Clear Form
                MessageBox.Show("Prisoner added successfully!");
                ClearFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please ensure Dates (YYYY-MM-DD) and Cell ID (Numbers) are in the correct format.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // Helper method to clear the textboxes after a successful save
        private void ClearFields()
        {
            textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            textBox5.Clear(); textBox6.Clear(); textBox7.Clear();
            textBox8.Clear(); textBox9.Clear();
        }

        
    }

}
