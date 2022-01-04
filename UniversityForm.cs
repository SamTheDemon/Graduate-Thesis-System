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
    public partial class UniversityForm : Form
    {
        protected internal int ID;
        public UniversityForm()
        {
            InitializeComponent();
        }

        /* -------------Buttons------------------ */
        // Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid() <= 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("ADD_UNI", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UNI_NAME", txtboxUniName.Text.Trim());
                            cmd.Parameters.AddWithValue("@LOCATION", txtboxUniLocation.Text.Trim());



                            if (con.State != ConnectionState.Open)
                                con.Open();

                            cmd.ExecuteNonQuery();

                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "University Added";
                            SuccForm.ShowDialog();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("While adding a University \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearTextBoxes(this.Controls);
                }
            }
        }
        
        //Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (txtboxUniName.Text == string.Empty || txtboxUniLocation.Text == string.Empty)
            {
                MessageBox.Show(" Choose data to Update, \n You Can't leave the Name and Location fields empty", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                try
                {
                    if (CheckUniBeforeDelete().Equals(true))
                    {
                        //Can delete
                        DialogResult rslt = MessageBox.Show(" This University Will be Deleted Paremntly! \n Are you sure you want to Delete it?", "Deleting confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt == DialogResult.Yes)
                        {

                            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                            {
                                using (SqlCommand cmd = new SqlCommand("DELETE_UNI", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@UNI_ID", ID);

                                    if (con.State != ConnectionState.Open)
                                        con.Open();

                                    cmd.ExecuteReader();

                                }
                            }
                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "University Deleted";
                            SuccForm.ShowDialog();
                            ClearTextBoxes(this.Controls);


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Error Catched: While Deleting a University \n Error Message  \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
        
        //Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //check if a cell is clicked to load the data
            if (txtboxUniName.Text == string.Empty || txtboxUniLocation.Text == string.Empty)
            {
                MessageBox.Show("Choose data to Update, \n You Can't leave the Name and Location fields empty", "Updating Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE_UNI", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UNI_ID", this.ID);
                            cmd.Parameters.AddWithValue("@UNI_NAME", txtboxUniName.Text.Trim());
                            cmd.Parameters.AddWithValue("@lOCATION", txtboxUniLocation.Text.Trim());

                            if (con.State != ConnectionState.Open)
                                con.Open();

                            SqlDataReader dr = cmd.ExecuteReader();

                        }

                    }
                    AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                    SuccForm.lblTaskCompleted.Text = "Updated Successfully";
                    SuccForm.ShowDialog();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(" Error catched: While Updating the University\n Error Details:  \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearTextBoxes(this.Controls);
                }
            }
        }

        private void UniversityForm_Activated(object sender, EventArgs e)
        {
            LoadLanguageData();
        }
        //LOAD LANG DATA
        private void LoadLanguageData()
        {
            ClearTextBoxes(this.Controls);
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("LOAD_UNIVERSITY_DATA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    DataTable dtLang = new DataTable();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dtLang.Load(dr);

                    dataGridView.DataSource = dtLang;
                }
            }
        }

        //check language before delete
        public bool CheckUniBeforeDelete()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("CHECK_UNI_THES_BEFORE_DELETE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UNI_ID", ID);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtBook = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            //can't delete
                            MessageBox.Show("You can't delete THIS University, A Thesis is written from it! ", "Can't Complete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                MessageBox.Show("An error accured while checking if University can be deleted \n " + ex.Message, "Error catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        //data to add is valid
        private int IsValid()
        {
            if (txtboxUniName.Text == string.Empty && txtboxUniLocation.Text == string.Empty)
            {

                MessageBox.Show("UNIVERSITY NAME and LOCATION Can't Be Empty. Check it and Try Again!", "Adding failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                return 1;
            }
            return 0;
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
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count > 0)
                {
                   ID = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
                    txtboxUniName.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                    txtboxUniLocation.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accourd while loading Universites data" + ex.Message, "Error Catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //search
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM UNIVERSITY " +
                    " WHERE [U_NAME] LIKE '%' + @SEARCHBAR + '%'", con))
                {
                    cmd.Parameters.AddWithValue("@SEARCHBAR", txtboxSearch.Text);
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    DataTable dtThesis = new DataTable();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dtThesis.Load(dr);
                    dataGridView.DataSource = dtThesis;

                }
            }
        }
    }
}
