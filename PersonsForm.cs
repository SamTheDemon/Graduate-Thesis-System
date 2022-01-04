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
    public partial class PersonsForm : Form
    {


        AddPersonForm person_update = new AddPersonForm();
        public PersonsForm()
        {
            InitializeComponent();
        }
        private void PersonsForm_Activated(object sender, EventArgs e)
        {
            LoadPersonData();
        }
        
        //CLOSE
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (person_update.txtboxFname.Text != string.Empty)
            {
                person_update.ADD_OR_UPDATE = 1; // 0 to add 1 to update
                person_update.btnAdd.Text = "Update";
                person_update.lblperson.Text = "Update Person";
                person_update.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecet a cell of the member you want to update \n ", " Select Member data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            person_update.ADD_OR_UPDATE = 0; // 0 to add 1 to update
            person_update.ShowDialog();
        }


        //Delete 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (person_update.txtboxFname.Text == string.Empty)
            {
                MessageBox.Show("Selecet a row or cell of the Person you wants to delete \n ", " Deleting failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (checkPersonBeforeDelete().Equals(true))
                {

                    DialogResult rslt = MessageBox.Show("Record Will be Deleted Paremntly Are you sure you want to delete?", "Deleting confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rslt == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                            {
                                using (SqlCommand cmd = new SqlCommand("DELETE_PERSON", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@ID", person_update.ID);

                                    if (con.State != ConnectionState.Open)
                                        con.Open();

                                    cmd.ExecuteReader();

                                }
                            }
                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = "Person Deleted";
                            SuccForm.ShowDialog();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("While Deleting a Person an Error catched \n" + ex.Message + "\n" + ex.StackTrace, "Catched an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            ClearTextBoxes(this.Controls);
                        }
                    }
                }



            }
        }

        private void dataGridViewPerson_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            byte[] img = (byte[])dataGridViewPerson.CurrentRow.Cells[6].Value;
            MemoryStream mems = new MemoryStream(img);

            if (dataGridViewPerson.Rows.Count > 0)
            {
                person_update.ID = Convert.ToInt32(dataGridViewPerson.CurrentRow.Cells[0].Value);
                person_update.txtboxFname.Text = dataGridViewPerson.CurrentRow.Cells[2].Value.ToString();
                person_update.txtboxLname.Text = dataGridViewPerson.CurrentRow.Cells[3].Value.ToString();
                person_update.txtboxEmail.Text = dataGridViewPerson.CurrentRow.Cells[4].Value.ToString();
                person_update.dtpickerBday.Value = Convert.ToDateTime(dataGridViewPerson.CurrentRow.Cells[5].Value);
                person_update.pickPicBox.Image = Image.FromStream(mems);
            }
        }

        private void LoadPersonData()
        {
            try
            {

                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("LOAD_PERSON_DATA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtPerson = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dtPerson.Load(dr);

                        dataGridViewPerson.DataSource = dtPerson;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading Members data \n " + ex.Message, "Error catched", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //Check if person has connection before deletion
        public bool checkPersonBeforeDelete()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("CHECK_PERSON_BEFORE_DELETE", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", person_update.ID);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtBook = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            //can't delete
                            MessageBox.Show("You can't delete this Person its had been Envolved with a thesis  before! ", "Can't Complete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return false;
                        }
                        else
                        {
                            //cann delete
                            return true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while checking if a person can be deleted \n " + ex.Message, "Error catched", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        //search
        private void txtboxLangSearch_TextChanged(object sender, EventArgs e)
        {
            
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM PERSON " +
                        " WHERE [P_FNAME] LIKE '%' + @SEARCHBAR + '%' OR [P_LNAME] LIKE '%' + @SEARCHBAR + '%' ", con))
                    {
                        cmd.Parameters.AddWithValue("@SEARCHBAR", txtboxLangSearch.Text);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtThesis = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dtThesis.Load(dr);
                        dataGridViewPerson.DataSource = dtThesis;

                    }
                }
            
        }
    }
}
