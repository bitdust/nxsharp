using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gui
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            textBox1.Text = Cfg.versionHEX;
            textBox2.Text = Cfg.VersionParser(Cfg.versionHEX);
            textBox3.Text = Cfg.H3C_key;
            textBox4.Text = Cfg.H3C_key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length % 2 != 0 || textBox1.Text.Length > 32 || textBox1.Text.Length < 2)
            {
                MessageBox.Show("输入版本号信息不正确！"); 
                textBox1.Text = "";
            }
            else if (textBox3.Text.Length > 20)
            {
                MessageBox.Show("输入密钥过长！");
                textBox1.Text = "";
            }
            else
            {            
                Cfg.versionHEX = textBox1.Text;
                Cfg.H3C_key = textBox3.Text;
                Cfg.Commit();
                textBox2.Text = Cfg.VersionParser(textBox1.Text);
                textBox4.Text = Cfg.H3C_key;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "https://github.com/bitdust/H3C_toolkit");
        }
    }
}
