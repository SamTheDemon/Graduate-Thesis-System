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
    public partial class AddPersonForm : Form
    {
        protected internal int ID; //Hold the Id of the SLECTED PERSON 
        protected internal int ADD_OR_UPDATE;
        public AddPersonForm()
        {
        
            InitializeComponent();

        }
        //chose a picture
        private void pickPicBox_Click(object sender, EventArgs e)
        {
            var dia = new OpenFileDialog();
            var result = dia.ShowDialog();
            if (result == DialogResult.OK)
            {
                pickPicBox.Image = Image.FromFile(dia.FileName);
            }


        }
        //chose a picture
        private void linklblPick_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dia = new OpenFileDialog();
            var result = dia.ShowDialog();
            if (result == DialogResult.OK)
            {
                pickPicBox.Image = Image.FromFile(dia.FileName);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            if (ADD_OR_UPDATE == 0)
            {   //adding 
                if (IsValidToAdd() <= 0)
                {
                    //converting the pic to binray
                    MemoryStream mem = new MemoryStream();
                    pickPicBox.Image.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _picbocCover = mem.ToArray();
                    try
                    {
                        using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                        {
                            using (SqlCommand cmd = new SqlCommand("ADD_PERSON", con)) 
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@FNAME", txtboxFname.Text.Trim());
                                cmd.Parameters.AddWithValue("@LNAME", txtboxLname.Text.Trim());
                                cmd.Parameters.AddWithValue("@EMAIL", txtboxEmail.Text.Trim());
                                cmd.Parameters.AddWithValue("@BDAY", dtpickerBday.Value);
                                cmd.Parameters.AddWithValue("@IMG", _picbocCover);
                                

                                if (con.State != ConnectionState.Open)
                                    con.Open();

                                cmd.ExecuteScalar();
                                AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                                SuccForm.lblTaskCompleted.Text = " Person Added";
                                SuccForm.ShowDialog();

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("While Adding a Person an Error catched \n" + ex.Message + "\n" + ex.StackTrace, "Catched an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        txtboxFname.Focus();
                        ClearTextBoxes(this.Controls);
                    }
                }
            }
            //updating
            else
            {
                try
                {
                    //converting the pic to binray
                    MemoryStream mem = new MemoryStream();
                    pickPicBox.Image.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _picbocCover = mem.ToArray();
                    using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE_PERSON", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", this.ID);
                            cmd.Parameters.AddWithValue("@FNAME", txtboxFname.Text.Trim());
                            cmd.Parameters.AddWithValue("@LNAME", txtboxLname.Text.Trim());
                            cmd.Parameters.AddWithValue("@EMAIL", txtboxEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@BDAY", dtpickerBday.Value);
                            cmd.Parameters.AddWithValue("@IMG", _picbocCover);


                            if (con.State != ConnectionState.Open)
                                con.Open();

                            SqlDataReader dr = cmd.ExecuteReader();
                            AddedSuccefulyDialog SuccForm = new AddedSuccefulyDialog();
                            SuccForm.lblTaskCompleted.Text = " Person Update";
                            SuccForm.ShowDialog();

                        }

                    }

                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("While Updating a Person  an Error catched \n" + ex.Message + "\n" + ex.StackTrace, "Catched an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    txtboxFname.Focus();
                }
            }
        }


        //Is data valid and Username doesn't exists
        private int IsValidToAdd()
        {
            if (txtboxFname.Text == string.Empty)
            {
                MessageBox.Show("First Name Can't be empty");
                return 2;
            }
            if (txtboxLname.Text == string.Empty)
            {
                MessageBox.Show("Last Name can't be empty");
                return 2;
            }
            if ((txtboxEmail.Text == string.Empty))
            {
                MessageBox.Show("Email can't be empty");
                return 2;
            }
                return 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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
