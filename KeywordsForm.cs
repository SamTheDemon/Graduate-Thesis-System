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
    public partial class KeywordsForm : Form
    {
        protected internal int ID;
        public KeywordsForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtboxKeyword.Text != String.Empty)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("ADD_KEY", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@K_WORD", txtboxKeyword.Text.Trim());



                            if (con.State != ConnectionState.Open)
                                con.Open();

                            cmd.ExecuteNonQuery();

                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "keyWord Added";
                            SuccForm.timer1.Interval = 1500;
                            SuccForm.ShowDialog();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("While adding a Keyword \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearTextBoxes(this.Controls);
                }
            }
            else
            {
                MessageBox.Show("Keywords can't be empty","Adding failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID <= 0)
            {
                MessageBox.Show(" Choose data to Update, \n You Can't leave the Name and University fields empty", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                try
                {
                    if (CheckInstBeforeDelete().Equals(true))
                    {
                        //Can delete
                        DialogResult rslt = MessageBox.Show(" This Institute Will be Deleted Paremntly! \n Are you sure you want to Delete it?", "Deleting confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt == DialogResult.Yes)
                        {

                            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                            {
                                using (SqlCommand cmd = new SqlCommand("DELETE_KEYWORD", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("K_ID", ID);

                                    if (con.State != ConnectionState.Open)
                                        con.Open();

                                    cmd.ExecuteReader();

                                }
                            }
                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "Keyword Deleted";
                            SuccForm.timer1.Interval = 1500;
                            SuccForm.ShowDialog();
                            ClearTextBoxes(this.Controls);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Error Catched: While Deleting a Institute \n Error Message  \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void LoadKeywordData()
        {
            ClearTextBoxes(this.Controls);
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("LOAD_KEYWORDS_DATA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    DataTable dt = new DataTable();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dt.Load(dr);

                    dataGridView.DataSource = dt;
                }
            }
        }
        //check language before delete
        public bool CheckInstBeforeDelete()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("CHECK_KEYWORD_BEFORE_DELETE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@K_ID", ID);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtBook = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            //can't delete
                            MessageBox.Show("You can't delete THIS Keywords, A Thesis is written from it! ", "Can't Complete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return false;
                        }
                        else
                        {

                            //can delete
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while checking if Institute can be deleted \n " + ex.Message, "Error catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

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

        private void KeywordsForm_Activated(object sender, EventArgs e)
        {
            LoadKeywordData();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count > 0)
                {
                    ID = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accourd while choosing Keyword data to delete \n" + ex.Message, "Error Catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
