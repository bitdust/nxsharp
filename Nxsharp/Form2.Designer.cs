namespace gui
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.modeButton1 = new System.Windows.Forms.RadioButton();
            this.modeButton2 = new System.Windows.Forms.RadioButton();
            this.modeButton3 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(212, 251);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "修改版本号&V";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(212, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "手动刷新&IP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(300, 174);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(232, 207);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 12);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "看看新版本";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // modeButton1
            // 
            this.modeButton1.AutoSize = true;
            this.modeButton1.Location = new System.Drawing.Point(27, 212);
            this.modeButton1.Name = "modeButton1";
            this.modeButton1.Size = new System.Drawing.Size(95, 16);
            this.modeButton1.TabIndex = 5;
            this.modeButton1.TabStop = true;
            this.modeButton1.Text = "掉线就掉线了";
            this.modeButton1.UseVisualStyleBackColor = true;
            this.modeButton1.CheckedChanged += new System.EventHandler(this.modeButton1_CheckedChanged);
            // 
            // modeButton2
            // 
            this.modeButton2.AutoSize = true;
            this.modeButton2.Location = new System.Drawing.Point(27, 235);
            this.modeButton2.Name = "modeButton2";
            this.modeButton2.Size = new System.Drawing.Size(71, 16);
            this.modeButton2.TabIndex = 6;
            this.modeButton2.TabStop = true;
            this.modeButton2.Text = "掉线重连";
            this.modeButton2.UseVisualStyleBackColor = true;
            this.modeButton2.CheckedChanged += new System.EventHandler(this.modeButton2_CheckedChanged);
            // 
            // modeButton3
            // 
            this.modeButton3.AutoSize = true;
            this.modeButton3.Location = new System.Drawing.Point(27, 258);
            this.modeButton3.Name = "modeButton3";
            this.modeButton3.Size = new System.Drawing.Size(155, 16);
            this.modeButton3.TabIndex = 7;
            this.modeButton3.TabStop = true;
            this.modeButton3.Text = "止痛药(每10分钟掉线！)";
            this.modeButton3.UseVisualStyleBackColor = true;
            this.modeButton3.CheckedChanged += new System.EventHandler(this.modeButton3_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "工作模式选择：";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 285);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modeButton3);
            this.Controls.Add(this.modeButton2);
            this.Controls.Add(this.modeButton1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "高级";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.RadioButton modeButton1;
        private System.Windows.Forms.RadioButton modeButton2;
        private System.Windows.Forms.RadioButton modeButton3;
        private System.Windows.Forms.Label label1;

    }
}