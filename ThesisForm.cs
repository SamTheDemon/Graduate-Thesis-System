using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graduate_Thesis_System
{
    public partial class ThesisForm : Form
    {
        AddThesisForm update_thesis = new AddThesisForm();
        private int id=0;
        public ThesisForm()
        {
            InitializeComponent();
        }


        private void ThesisForm_Activated(object sender, EventArgs e)
        {
            LoadThesiseData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form form = new AddThesisForm();
            form.ShowDialog();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update_thesis.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (id ==0)
            {
                MessageBox.Show("Selecet a row or cell of the THESIS you wants to delete \n ", " Deleting failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                    DialogResult rslt = MessageBox.Show("Record Will be Deleted Paremntly Are you sure you want to delete?", "Deleting confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rslt == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                            {
                                using (SqlCommand cmd = new SqlCommand("DELETE_THESIS", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@THES_ID", id);

                                    if (con.State != ConnectionState.Open)
                                        con.Open();

                                    cmd.ExecuteReader();

                                }
                            }
                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "Thesis Deleted";
                            SuccForm.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("While Deleting a Thesis an Error catched \n" + ex.Message + "\n" + ex.StackTrace, "Catched an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            ClearTextBoxes(this.Controls);
                        }
                    }
            }
        }
        //LOAD Thesis DATA
        private void LoadThesiseData()
        {
            ClearTextBoxes(this.Controls);
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("[LOAD_ALL_THESIS]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    DataTable dtThesis = new DataTable();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dtThesis.Load(dr);

                    dataGridView.DataSource = dtThesis;
                }
            }
        }
        //Search
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
           
        }

        //clear controls
        public void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form searchForm = new ThesisSearchForm();
            searchForm.ShowDialog();

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (dataGridView.Rows.Count > 0)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);


            }
        }
    }
}
