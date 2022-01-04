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
    public partial class Submit_2ndForm : Form
    {
        protected internal  int thes_id;
        
        public Submit_2ndForm()
        {
            InitializeComponent();
            lblAboveKeyword.Enabled = false;    
        }


        private void TopicsAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("ADD_THESIS_TOPICS", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@THES_ID", thes_id);
                    cmd.Parameters.AddWithValue("@T_ID", cmbTopics.SelectedValue);

                    if (con.State != ConnectionState.Open)
                        con.Open();

                    cmd.ExecuteScalar();

                    AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                    SuccForm.lblTaskCompleted.Text = " Topics Linked";
                    SuccForm.timer1.Interval = 1000;
                    SuccForm.ShowDialog();

                }
            }
        }




        //check IF keyword exists if not added then get the id
        // then add it to the thesis_key table
        private void checkAndAdd()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("ADD_THESIS_KEY_BOTH_4", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KEYWORD", txtboxKey.Text.Trim());
                    cmd.Parameters.AddWithValue("@THES_ID", thes_id);


                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int modified = (int)cmd.ExecuteScalar();
                    MessageBox.Show("ID :" + modified);

                    if (cmd.ExecuteNonQuery() != 1)
                    {

                    }
                    txtboxKey.Clear();
                }
            }
        }

        //CHECKS IF keyword exists if not added then get the id
        private void checkIfkeyWordExists(){
            //check if keyword exists or not. if not then add it to the keyword table then add it to the thesis_keyword
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("ADD_THESIS_KEY_BOTH_3", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KEYWORD", txtboxKey.Text.Trim());

                 
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    int modified = (int)cmd.ExecuteScalar();
                    MessageBox.Show("ID :"+modified);

                    if (cmd.ExecuteNonQuery() != 1)
                    {

                    }
                    txtboxKey.Clear();
                }
            }

            /*using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("CHECK_KEYWORD_BEFORE_ADD", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KEYWORD", txtboxKey.Text.Trim());
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        //add the key id to the thesis_keyword
                        MessageBox.Show("keyword already exsits");
                    }
                    txtboxKey.Clear();
                }
            }*/
        }

        private void txtboxKey_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtboxKey.Text != String.Empty)
                {
                    checkAndAdd();
                }
                else
                {
                    MessageBox.Show("Keywords can't be empty", "Adding failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }

}
