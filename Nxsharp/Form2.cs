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
                checkBox1.Checked = true;
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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Cfg.mode = 1;
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
    }
}
