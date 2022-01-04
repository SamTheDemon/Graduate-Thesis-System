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
    public partial class LangaugesForm : Form
    {
        public int ID =0;
        public string? oldtitle =null;
        public string? oldsymb = null;
        public LangaugesForm()
        {
            InitializeComponent();
        }
        private void LangaugesForm_Activated(object sender, EventArgs e)
        {
            LoadLanguageData();
        }

        //Langauges doesn't exists and data is valid
        private int IsValid()
        {
            if (txtboxTitleLang.Text != string.Empty && txtboxSymbolLang.Text != string.Empty)
            {

                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM LANGAUGES WHERE (L_TITLE = @TITLE)", con))
                    {
                        check_User_Name.Parameters.AddWithValue("@TITLE", txtboxTitleLang.Text);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        int UserExist = (int)check_User_Name.ExecuteScalar(); // if already exists returns 1 

                        if (UserExist > 0)
                        {
                            MessageBox.Show("Langauge Already Exists. ", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
                            return 1;
                        }
                        else
                        {
                            return UserExist;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Langauge Title and Symbol Can't Be Empty. Check it and Try Again!", "Adding failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
                return 1;
            }
        }

        //LOAD LANG DATA
        private void LoadLanguageData()
        {
            ClearTextBoxes(this.Controls);
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("LOAD_LANGAUGES_DATA", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    DataTable dtLang = new DataTable();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dtLang.Load(dr);

                    dataGridViewLang.DataSource = dtLang;
                }
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
        //Load data to text boxes when clicked
        private void dataGridViewLang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewLang.Rows.Count > 0)
                {
                    ID = Convert.ToInt32(dataGridViewLang.CurrentRow.Cells[0].Value);
                    txtboxTitleLang.Text = dataGridViewLang.CurrentRow.Cells[1].Value.ToString();
                    txtboxSymbolLang.Text = dataGridViewLang.CurrentRow.Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accourd while loading Langauges data" + ex.Message, "Error Catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //check language before delete
        public bool checkAuthorBeforeDelete()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("CHECK_LANG_BEFORE_DELETE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@LANG_ID", ID);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtBook = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            //can't delete
                            MessageBox.Show("You can't delete THIS Langauge, A Thesis is written with with it! ", "Can't Complete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                MessageBox.Show("An error accured while checking if Language can be deleted \n " + ex.Message, "Error catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        /* -------------Buttons------------------ */
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid() <= 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("ADD_LANG", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TITLE", txtboxTitleLang.Text);
                            cmd.Parameters.AddWithValue("@SYMBOL", txtboxSymbolLang.Text);



                            if (con.State != ConnectionState.Open)
                                con.Open();

                            cmd.ExecuteNonQuery();

                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = " Langauge Added";
                            SuccForm.ShowDialog();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("While adding a Langauge \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearTextBoxes(this.Controls);
                    txtboxTitleLang.Focus();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (txtboxTitleLang.Text == string.Empty || txtboxSymbolLang.Text == string.Empty)
            {
                MessageBox.Show(" Choose data to Update, \n You Can't leave the Title and Symbool fields empty", "Deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                try
                {
                    if (checkAuthorBeforeDelete().Equals(true))
                    {
                        //Can delete
                        DialogResult rslt = MessageBox.Show(" This Langauge Will be Deleted Paremntly! \n Are you sure you want to Delete it?", "Deleting confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rslt == DialogResult.Yes)
                        {

                            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                            {
                                using (SqlCommand cmd = new SqlCommand("DELETE_LANGAUGE", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@LANG_ID", ID);

                                    if (con.State != ConnectionState.Open)
                                        con.Open();

                                    cmd.ExecuteReader();

                                }
                            }
                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "Langauge Deleted";
                            SuccForm.ShowDialog();
                            ClearTextBoxes(this.Controls);


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("While Deleting a Langauge Error catched:  \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //check if a cell is clicked to load the data
            if (txtboxTitleLang.Text == string.Empty || txtboxSymbolLang.Text == string.Empty)
            {
                MessageBox.Show("Choose data to Update, \n You Can't leave the Title and Symbol fields empty", "Updating Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE_LANGAUGE", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ID", this.ID);
                            cmd.Parameters.AddWithValue("@TITLE", txtboxTitleLang.Text);
                            cmd.Parameters.AddWithValue("@SYMBOL", txtboxSymbolLang.Text);

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
                    MessageBox.Show("While Updating the Langaue an Error catched:  \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearTextBoxes(this.Controls);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void txtboxLangSearch_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM LANGAUGES " +
                    " WHERE [L_TITLE] LIKE '%' + @SEARCHBAR + '%' ", con))
                {
                    cmd.Parameters.AddWithValue("@SEARCHBAR", txtboxLangSearch.Text);
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    DataTable dtThesis = new DataTable();

                    SqlDataReader dr = cmd.ExecuteReader();

                    dtThesis.Load(dr);
                    dataGridViewLang.DataSource = dtThesis;

                }
            }
        }
    }
}
