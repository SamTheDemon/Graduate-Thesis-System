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
    public partial class InstituteForm : Form
    {
        protected internal int ID;
        public InstituteForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InstituteForm_Activated(object sender, EventArgs e)
        {
            GetUniData();
            LoadInstituteData();

        }

        //get publisher combo dorp list from database
        public void GetUniData()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_UNI", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dtData = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dtData.Load(dr);

                        cmbBoxUni.DataSource = dtData;
                        cmbBoxUni.DisplayMember = "U_NAME";
                        cmbBoxUni.ValueMember = "U_ID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading Universities data to combo box" + ex.Message, "Error catched");
            }
        }

        //add
        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (IsValid() <= 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("ADD_INSTI", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@INST_NAME", txtboxInstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@UNI_ID", cmbBoxUni.SelectedValue);



                            if (con.State != ConnectionState.Open)
                                con.Open();

                            cmd.ExecuteNonQuery();

                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "Institute Added";
                            SuccForm.ShowDialog();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("While adding a Institute \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearTextBoxes(this.Controls);
                }
            }
        }
        //Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //check if a cell is clicked to load the data
            if (txtboxInstName.Text == string.Empty || cmbBoxUni.SelectedValue.Equals(null))
            { 
                MessageBox.Show("Choose data to Update, \n You Can't leave the Name and University fields empty", "Updating Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE_INST", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@INST_ID", this.ID);
                            cmd.Parameters.AddWithValue("@INST_NAME", txtboxInstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@UNI_ID", cmbBoxUni.SelectedValue);

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
                    MessageBox.Show(" Error catched: While Updating the Institute\n Error Details:  \n" + ex.Message, "Catched an error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (txtboxInstName.Text == string.Empty || cmbBoxUni.SelectedValue.Equals(null))
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
                                using (SqlCommand cmd = new SqlCommand("DELETE_INST", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("INST_ID", ID);

                                    if (con.State != ConnectionState.Open)
                                        con.Open();

                                    cmd.ExecuteReader();

                                }
                            }
                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "Institute Deleted";
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


        //LOAD INST DATA
        private void LoadInstituteData()
        {
            ClearTextBoxes(this.Controls);
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("LOAD_INSTITIUTE_DATA", con))
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
                    using (SqlCommand cmd = new SqlCommand("[CHECK_INST_THES_BEFORE_DELETE]", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@INST_ID", ID);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtBook = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();


                        if (dr.HasRows)
                        {
                            //can't delete
                            MessageBox.Show("You can't delete THIS Institute, A Thesis is written from it! ", "Can't Complete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        //data to add is valid
        private int IsValid()
        {
            if (txtboxInstName.Text == string.Empty)
            {

                MessageBox.Show("Institute NAME and University Can't Be Empty. Check it and Try Again!", "Adding failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView.Rows.Count > 0)
                {
                    ID = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
                    txtboxInstName.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                    cmbBoxUni.Text= dataGridView.CurrentRow.Cells[2].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accourd while putting Istitute data to delete and update" + ex.Message, "Error Catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //search
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM INSTITUTES " +
                    " WHERE [INST_NAME] LIKE '%' + @SEARCHBAR + '%'", con))
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
