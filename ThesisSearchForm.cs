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
    public partial class ThesisSearchForm : Form
    {
        public ThesisSearchForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmboxSearchField.SelectedItem.Equals("ALL"))
            {
                 using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                            {
                                using (SqlCommand cmd = new SqlCommand("SEARCH_THESIS_DATA", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
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
            else
            {
                FieldSearch();

            }

        }
        //fields search
        private void FieldSearch()
        {
            if (cmboxSearchField.Text=="Title")
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS WHERE [TH_TITLE] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.Text=="Number")
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS WHERE [TH_NUM] LIKE '%' + @SEARCHBAR + '%' ", con))
                    {
                        cmd.Parameters.AddWithValue("@SEARCHBAR", Convert.ToInt32(txtboxSearch.Text));
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        DataTable dtThesis = new DataTable();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dtThesis.Load(dr);
                        dataGridView.DataSource = dtThesis;

                    }
                }
            }
            else if (cmboxSearchField.SelectedItem.Equals("Abstract"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS WHERE [TH_ABSTRACT] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Year"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS WHERE [TH_YEAR] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Type"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS WHERE [TH_TYPE] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Pages"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS WHERE [TH_NUMOFPAGES] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("SubmitDate"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS WHERE [TH_SUBMITDATE] LIKE '%' + @SEARCHBAR + '/%'", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Author Fname"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN PERSON a ON t.TH_AUTHOR = a.P_ID" +
                        " WHERE [a.P_FNAME] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Author Lname"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                         "INNER JOIN PERSON a ON t.TH_AUTHOR = a.P_ID" +
                         " WHERE [a.P_LNAME] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Supervisor Fname"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN PERSON s ON t.TH_SUPERVISOR = s.P_ID" +
                        " WHERE [s.P_FNAME] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Supervisor Lname"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN PERSON s ON t.TH_SUPERVISOR = s.P_ID" +
                        " WHERE [s.P_LNAME] LIKE '%' + @SEARCHBAR + '%'", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Co-supervisor Fname"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN PERSON c ON t.TH_COSUPERVISOR = c.P_ID" +
                        " WHERE [c.P_FNAME] LIKE '%' + @SEARCHBAR + '%'", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Co-supervisor Lname"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN PERSON c ON t.TH_COSUPERVISOR = c.P_ID" +
                        " WHERE [c.P_LNAME] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Language"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN LANGAUGES l ON t.TH_LANGAGUE = l.L_ID" +
                        " WHERE [l.L_TITLE] LIKE '%' + @SEARCHBAR + '%'", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("University"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN UNIVERSITY u ON t.TH_UNIVERSITY = u.U_ID" +
                        " WHERE [u.U_NAME] LIKE '%' + @SEARCHBAR + '%' ", con))
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
            else if (cmboxSearchField.SelectedItem.Equals("Institution"))
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM THESIS t" +
                        "INNER JOIN INSTITUTES i ON t.TH_INSTITUTE = i.INST_ID" +
                        " WHERE [i.INST_NAME] LIKE '%' + @SEARCHBAR + '%'", con))
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
}
 