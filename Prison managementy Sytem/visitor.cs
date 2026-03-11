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
    public partial class visitor : Form
    {
        VisitorsRepo repo = new VisitorsRepo();


        public visitor()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetAllData();
        }

        public List<VisitorsModel> GetAllData() => repo.GetAllVisitors();
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Add Visitor
        private void button1_Click(object sender, EventArgs e)
        {
            // Validating the boxes
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Please provide the Visitor's Name, CNIC, and Phone number.",
                                "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                //  Map UI to VisitorsModel
                VisitorsModel newVisitor = new VisitorsModel
                {
                    VisitorName = textBox2.Text, 
                    CNIC = textBox3.Text,        
                    Phone = textBox4.Text        
                };

                // Map UI to Visit Model 
                Visit newVisit = new Visit
                {
                    //  parse the IDs because the Model expects integers
                    PrisonerID = int.Parse(textBox7.Text),
                    ApprovedByStaffID = int.Parse(textBox8.Text)
                };

                // Save the Visitor
                repo.AddVisitor(newVisitor);

               


                MessageBox.Show("Visitor and Visit details processed!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please ensure IDs (Prisoner, Staff) are numbers only.",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
    
}
