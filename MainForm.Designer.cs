namespace Graduate_Thesis_System
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPerson = new System.Windows.Forms.Button();
            this.btnLang = new System.Windows.Forms.Button();
            this.btnUni = new System.Windows.Forms.Button();
            this.btnInst = new System.Windows.Forms.Button();
            this.btnThesis = new System.Windows.Forms.Button();
            this.btnKeywords = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPerson
            // 
            this.btnPerson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnPerson.Location = new System.Drawing.Point(300, 154);
            this.btnPerson.Name = "btnPerson";
            this.btnPerson.Size = new System.Drawing.Size(131, 54);
            this.btnPerson.TabIndex = 0;
            this.btnPerson.Text = "Person";
            this.btnPerson.UseVisualStyleBackColor = false;
            this.btnPerson.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLang
            // 
            this.btnLang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnLang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnLang.Location = new System.Drawing.Point(707, 329);
            this.btnLang.Name = "btnLang";
            this.btnLang.Size = new System.Drawing.Size(131, 54);
            this.btnLang.TabIndex = 1;
            this.btnLang.Text = "Language";
            this.btnLang.UseVisualStyleBackColor = false;
            this.btnLang.Click += new System.EventHandler(this.btnLang_Click);
            // 
            // btnUni
            // 
            this.btnUni.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnUni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUni.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnUni.Location = new System.Drawing.Point(437, 203);
            this.btnUni.Name = "btnUni";
            this.btnUni.Size = new System.Drawing.Size(131, 54);
            this.btnUni.TabIndex = 2;
            this.btnUni.Text = "University";
            this.btnUni.UseVisualStyleBackColor = false;
            this.btnUni.Click += new System.EventHandler(this.btnUni_Click);
            // 
            // btnInst
            // 
            this.btnInst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnInst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInst.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnInst.Location = new System.Drawing.Point(570, 269);
            this.btnInst.Name = "btnInst";
            this.btnInst.Size = new System.Drawing.Size(131, 54);
            this.btnInst.TabIndex = 3;
            this.btnInst.Text = "Institution";
            this.btnInst.UseVisualStyleBackColor = false;
            this.btnInst.Click += new System.EventHandler(this.btnInst_Click);
            // 
            // btnThesis
            // 
            this.btnThesis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnThesis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThesis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnThesis.Location = new System.Drawing.Point(163, 96);
            this.btnThesis.Name = "btnThesis";
            this.btnThesis.Size = new System.Drawing.Size(131, 54);
            this.btnThesis.TabIndex = 4;
            this.btnThesis.Text = "Thesis";
            this.btnThesis.UseVisualStyleBackColor = false;
            this.btnThesis.Click += new System.EventHandler(this.btnThesis_Click);
            // 
            // btnKeywords
            // 
            this.btnKeywords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnKeywords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeywords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnKeywords.Location = new System.Drawing.Point(841, 386);
            this.btnKeywords.Name = "btnKeywords";
            this.btnKeywords.Size = new System.Drawing.Size(131, 54);
            this.btnKeywords.TabIndex = 5;
            this.btnKeywords.Text = "Keywords";
            this.btnKeywords.UseVisualStyleBackColor = false;
            this.btnKeywords.Click += new System.EventHandler(this.btnKeywords_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(1164, 525);
            this.Controls.Add(this.btnKeywords);
            this.Controls.Add(this.btnThesis);
            this.Controls.Add(this.btnInst);
            this.Controls.Add(this.btnUni);
            this.Controls.Add(this.btnLang);
            this.Controls.Add(this.btnPerson);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnPerson;
        private Button btnLang;
        private Button btnUni;
        private Button btnInst;
        private Button btnThesis;
        private Button btnKeywords;
    }
}