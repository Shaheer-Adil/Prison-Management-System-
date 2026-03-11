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
    public partial class Inmates : Form
    {
        PrisonerRepo Prepo = new PrisonerRepo();
        StaffRepo repo = new StaffRepo();

        public Inmates()
        {
            InitializeComponent();
        }

        private void Inmates_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Getting the data of all the inmates to show it on the grid
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch the data from the Repo
                List<PrisonerModel> list = Prepo.GetAllPrisoners();

                //  Bind to the Grid
                dataGridView1.DataSource = null;

                if (list != null && list.Count > 0)
                {
                    dataGridView1.DataSource = list;

                    //  Hide the 'List' columns that don't display well in a grid
                    if (dataGridView1.Columns["Crimes"] != null) dataGridView1.Columns["Crimes"].Visible = false;
                    if (dataGridView1.Columns["Transfers"] != null) dataGridView1.Columns["Transfers"].Visible = false;
                    if (dataGridView1.Columns["Visits"] != null) dataGridView1.Columns["Visits"].Visible = false;

                    // Make columns fit the content
                    dataGridView1.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("No prisoner records found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //getting the data of all staff memebers
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetching the data from the Staff Repository
                List<StaffModel> staffList = repo.GetAllStaff();

                //  Bind to the Grid
                dataGridView1.DataSource = null; // Clear old data

                if (staffList != null && staffList.Count > 0)
                {
                    dataGridView1.DataSource = staffList;

                    //  Formatting
                    dataGridView1.AutoResizeColumns();
                }
                else
                {
                    MessageBox.Show("No staff records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading staff data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
