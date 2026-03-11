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
using Prison_managementy_Sytem.Model;
using Prison_managementy_Sytem.Repo;

namespace Prison_managementy_Sytem
{
    public partial class transfer : Form
    {
        PrisonerRepo repo = new PrisonerRepo();

        public transfer()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetAllData();

        }
        public List<Transfer> GetAllData()
        {
            return repo.GetAllTransfers();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  Validation
            if (string.IsNullOrWhiteSpace(textBox2.Text) || // PRISONER_ID
                string.IsNullOrWhiteSpace(textBox3.Text) || // FROM_PRISON
                string.IsNullOrWhiteSpace(textBox4.Text) || // TO_PRISON
                string.IsNullOrWhiteSpace(textBox5.Text) || // TRANSFER_DATE
                string.IsNullOrWhiteSpace(textBox6.Text))   // APPROVED_BY
            {
                MessageBox.Show("Please fill all transfer details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //  Create Transfer Model from UI
                Transfer newTransfer = new Transfer
                {
                    PrisonerID = int.Parse(textBox2.Text),
                    FromPrison = textBox3.Text,
                    ToPrison = textBox4.Text,
                    TransferDate = DateTime.Parse(textBox5.Text),
                    ApprovedBy = textBox6.Text
                };

                //  Calling  Repository to save
                repo.AddTransfer(newTransfer);

                MessageBox.Show("Transfer record prepared successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("Check your formats: Prisoner ID must be a number and Date must be YYYY-MM-DD.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();
            textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void transfer_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
    
