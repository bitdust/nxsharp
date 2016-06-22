using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace gui
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if (Cfg.mode == 1)
            {
                modeButton2.Checked = true;
            }
            else if(Cfg.mode == 2)
            {
                modeButton3.Checked = true;
            }
            else if (Cfg.mode == 0)
            {
                modeButton1.Checked = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (NetworkInterfaceAvaliable.adapters_dict.ContainsKey(Cfg.device))
            {
                NetworkInterfaceAvaliable.RefreshDHCP(NetworkInterfaceAvaliable.adapters_dict[Cfg.device]);
            }
            else
            {
                MessageBox.Show("你没有选择网卡！");
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "https://github.com/bitdust/nxsharp/releases");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();      
        }

        private void modeButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(modeButton1.Checked)
            {
                Cfg.mode = 0;
                Cfg.Commit();
            }
        }

        private void modeButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (modeButton2.Checked)
            {
                Cfg.mode = 1;
                Cfg.Commit();
            }

        }

        private void modeButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (modeButton3.Checked)
            {
                Cfg.mode = 2;
                Cfg.Commit();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
