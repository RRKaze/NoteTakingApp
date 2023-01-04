using System;
using System.ComponentModel;
using System.Data;

namespace NoteTakingApp
{
    public partial class Form1 : Form
    {

        DataTable table;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("Title", typeof(String));
            table.Columns.Add("Message", typeof(String));

            dataGridView1.DataSource = table;
            dataGridView1.Columns["Message"].Visible = false;

            bttDelete.Enabled = false;
        }


        /// <summary>
        /// This New button will create a clean form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttNew_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtMessage.Clear();
        }

        /// <summary>
        /// This Save button will save the Title and Message to the Table
        /// and then create a new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                table.Rows.Add(txtTitle.Text, txtMessage.Text);
                txtTitle.Clear();
                txtMessage.Clear();
                bttDelete.Enabled = true;
            }
            else
            {
                MessageBox.Show("No Title! Please add a title!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This Read button will read the latest entry of notes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttRead_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            if (index > -1)
            {
                txtTitle.Text = table.Rows[index].ItemArray[0].ToString();
                txtMessage.Text = table.Rows[index].ItemArray[1].ToString();
            }
        }

        /// <summary>
        /// This Delete button will remove the latest entry of notes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bttDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            if (index > -1)
            {
                table.Rows[index].Delete();
                if (table.Rows.Count == 0)
                {
                    bttDelete.Enabled = false;
                }
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (txtTitle.Text == "")
            {
                errNoTitle.SetError(txtTitle, "No Title! Please add a title!");
                e.Cancel = true;
            }
            else
            {
                this.errNoTitle.SetError(this.txtTitle, "");
            }
        }
    }
}