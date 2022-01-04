
namespace Graduate_Thesis_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form  frm1 = new PersonsForm();
            frm1.ShowDialog();  
        }

        private void btnLang_Click(object sender, EventArgs e)
        {
            LangaugesForm frm2 = new LangaugesForm();
            frm2.ShowDialog();
        }

        private void btnUni_Click(object sender, EventArgs e)
        {
            Form frm = new UniversityForm();
            frm.ShowDialog();
        }

        private void btnInst_Click(object sender, EventArgs e)
        {
            Form frm = new InstituteForm();
            frm.ShowDialog();
        }

        private void btnThesis_Click(object sender, EventArgs e)
        {
            Form frm = new ThesisForm();
            frm.ShowDialog();
        }

        private void btnKeywords_Click(object sender, EventArgs e)
        {
            Form frm = new KeywordsForm();
            frm.ShowDialog();

        }
    }
}