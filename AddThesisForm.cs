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
    public partial class AddThesisForm : Form
    {
        Submit_2ndForm nform = new Submit_2ndForm();
        protected internal int thesis_num;
        public AddThesisForm()
        {
            InitializeComponent();
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if(cmbTopics.SelectedItem != null)
            {
                if (CheckSupervisorAndCo() == true)
                {
                    addThesis();

                }
                else
                {
                    MessageBox.Show("Please Select a Topic to continute," +
                        "Note: You can add more Topics and Keywords next page");
                }
            }

            
        }
        
        //add
        private void addThesis()
        {
            
            if (IsValidToAdd() <= 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("ADD_THESIS", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TITLE", txtboxTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@ABSTRACT", txtboxAbstract.Text.Trim());
                            cmd.Parameters.AddWithValue("@AUTHOR_ID", cmbAuthor.SelectedValue);
                            cmd.Parameters.AddWithValue("@TYPE", cmbType.Text.Trim());
                            cmd.Parameters.AddWithValue("@UNI_ID", cmbUniversity.SelectedValue);
                            cmd.Parameters.AddWithValue("@INST_ID", cmbInstitute.SelectedValue);
                            cmd.Parameters.AddWithValue("@PAGES", mtxtboxPage.Text.Trim());
                            cmd.Parameters.AddWithValue("@LANG_ID", cmbLanguages.SelectedValue);
                            cmd.Parameters.AddWithValue("@SUBMITDATE", dateTimePickerBDAY.Value);
                            cmd.Parameters.AddWithValue("@SUPERVISOR_ID", cmbSupervisor.SelectedValue);
                            //if co super visor exists
                            if (cmbCoSuper.SelectedItem != null)
                            {

                                cmd.Parameters.AddWithValue("@COSUPERVISOR_ID", cmbCoSuper.SelectedValue);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@COSUPERVISOR_ID", DBNull.Value);
                            }

                            if (con.State != ConnectionState.Open)
                                con.Open();

                            thesis_num = Convert.ToInt32(cmd.ExecuteScalar());

                            using (SqlConnection con1 = new SqlConnection(AppConnection.GetConnectionString()))
                            {
                                using (SqlCommand cmd1 = new SqlCommand("ADD_THESIS_TOPICS", con1))
                                {
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue("@THES_ID", thesis_num);
                                    cmd1.Parameters.AddWithValue("@T_ID", cmbTopics.SelectedValue);

                                    if (con1.State != ConnectionState.Open)
                                        con1.Open();

                                    cmd1.ExecuteScalar();

                                    DialogResult rslt = MessageBox.Show(" Do you want to link this thesis to more Topics?", "Additional Topics", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (rslt == DialogResult.Yes)
                                    {
                                        nform.thes_id = thesis_num;
                                        nform.ShowDialog();
                                    }

                                    AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                                    SuccForm.lblTaskCompleted.Text = " THESIS Added";
                                    SuccForm.ShowDialog();
                                   
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("While Adding a Thesis an Error catched \n" + ex.Message + "\n" + ex.StackTrace, "Catched an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    ClearTextBoxes(this.Controls);
                }
            }
        }
        private int IsValidToAdd()
        {
            if (txtboxTitle.Text == string.Empty || txtboxAbstract.Text == string.Empty || mtxtboxPage.Text == string.Empty
                || cmbLanguages.SelectedIndex.Equals(string.Empty) || cmbSupervisor.SelectedIndex.Equals(string.Empty) || cmbTopics.SelectedIndex.Equals(string.Empty)) 
            {
                MessageBox.Show("title and abstract and number of pages and langauges and supervisroa and author can't be empty.");
                return 2;
            }
            
            return 0;
        }

        private bool CheckSupervisorAndCo()
        {
            if( cmbCoSuper.SelectedValue == cmbSupervisor.SelectedValue)
            {
                MessageBox.Show("Supervisor and Co-supervisor can't be the same Person");
                return false;
            }
            else if(cmbAuthor.SelectedValue == cmbSupervisor.SelectedValue || cmbCoSuper.SelectedValue == cmbAuthor.SelectedValue)
            {

                MessageBox.Show("Author and Co-supervisor and Supervisor can't be the same Person");
                return false;
            }
            else
            {
            return true;
            }

        }

        //LOAD Author DATA
        private void loadAuthorData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_PERSON_DATA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dt = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dt.Load(dr);

                        cmbAuthor.DataSource = dt;
                        cmbAuthor.DisplayMember = "FULLNAME";
                        cmbAuthor.ValueMember = "P_ID";
                        cmbAuthor.SelectedIndex = -1;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading Supervisor data to combo box" + ex.Message, "Error catched");
            }
        }
        // LOAD CosuperData
        private void loadCoSuporData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_PERSON_DATA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dt = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();
                        dt.Load(dr);

                        cmbCoSuper.DataSource = dt;
                        cmbCoSuper.DisplayMember = "FULLNAME";
                        cmbCoSuper.ValueMember = "P_ID";
                        cmbCoSuper.SelectedIndex = -1;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading Supervisor data to combo box" + ex.Message, "Error catched");
            }
        }
        //LOAD Super DATA 
        private void loadSupervisorData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_PERSON_DATA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dt = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dt.Load(dr);

                        cmbSupervisor.DataSource = dt;
                        cmbSupervisor.DisplayMember = "FULLNAME";
                        cmbSupervisor.ValueMember = "P_ID";
                        cmbSupervisor.SelectedIndex = -1;
                        


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading Supervisor data to combo box" + ex.Message, "Error catched");
            }
        }

        //LOAD LANGUAGES DATA
        private void loadLangaugeData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_LANG_DATA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dt = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dt.Load(dr);

                        cmbLanguages.DataSource = dt;
                        cmbLanguages.DisplayMember = "LANGAUGES";
                        cmbLanguages.ValueMember = "L_ID";
                        cmbLanguages.SelectedIndex = -1;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading Supervisor data to combo box" + ex.Message, "Error catched");
            }
        }
        //LOAD TOPICS DATA
        private void loadTopicData()
        {
        try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_TOPIC_DATA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dt = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dt.Load(dr);

                        cmbTopics.DataSource = dt;
                        cmbTopics.DisplayMember = "TOPICS";
                        cmbTopics.ValueMember = "T_ID";
                        cmbTopics.SelectedIndex = -1;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading Supervisor data to combo box" + ex.Message, "Error catched");
            }
        }
        //LOAD University DATA
        private void loadUniversitysData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_UNIVERSITY_DATA", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (con.State != ConnectionState.Open)
                            con.Open();

                        DataTable dt = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dt.Load(dr);

                        cmbUniversity.DataSource = dt;
                        cmbUniversity.DisplayMember = "UNIVERSITY";
                        cmbUniversity.ValueMember = "U_ID";
                        cmbUniversity.SelectedValueChanged += cmbUniversity_SelectedValueChanged;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading UNIVERSITY data to combo box" + ex.Message, "Error catched");
            }
        }

        //load institutes based on university
        private void cmbUniversity_SelectedValueChanged(object? sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("COMBO_INSTITUTE_DATA", con))
                    {
                        /*MessageBox.Show("Uni ID is " + cmbUniversity.SelectedValue);*/
                        /*int uni_id = ((DataRowView)cmbUniversity.SelectedValue).Row.Field<int>("U_ID");*/
                        /*MessageBox.Show("Uni ID is " + uni_id);*/
                        /*MessageBox.Show(" Uni ID is" + cmbUniversity.SelectedIndex.ToString());*/
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UNI_ID", cmbUniversity.SelectedValue);
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dt = new DataTable();
                        SqlDataReader dr = cmd.ExecuteReader();

                        dt.Load(dr);

                        cmbInstitute.DisplayMember = "INSTITUTION";
                        cmbInstitute.ValueMember = "INST_ID";
                        cmbInstitute.DataSource = dt;
                        /*cmbInstitute.SelectedIndex = -1;*/
                        cmbInstitute.SelectedValueChanged += cmbUniversity_SelectedValueChanged;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error accured while loading INSTITUTE data to combo box" + ex.Message, "Error catched");
            }
        }

        //auto Clear boxes
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

        private void AddThesisForm_Load(object sender, EventArgs e)
        {
            loadAuthorData();
            loadCoSuporData();
            loadLangaugeData();
            loadSupervisorData();
            loadTopicData();

        }

        private void AddThesisForm_Activated(object sender, EventArgs e)
        {
            loadUniversitysData();
        }
    }

}
