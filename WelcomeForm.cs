using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graduate_Thesis_System
{
    public partial class WelcomeForm : Form
    {
        public int counter = 0;
        public int time = 0;
        public string text;
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            text = lblWelcome.Text;
            time = text.Length;
            lblWelcome.Text = "";
            timer1.Start();
        }
        //timer the moving text
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblWelcome.Text = text.Substring(0, counter);
            ++counter;
            if (counter > time)
            {
                timer1.Stop();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {
            Form adminform = new MainForm();
            adminform.ShowDialog();
        }

        private void lblAuthor_Click(object sender, EventArgs e)
        {
            Form authorform = new ThesisForm();
            authorform.ShowDialog();
        }


    }
}
