using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using global::System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace gui
{
    public partial class Form1:Form
    {
        StringBuilder outSb = new StringBuilder();
        public Form1()
        {
            InitializeComponent();
            this.textBox1.Text = Cfg.username;
            this.textBox2.Text = Cfg.password;
            this.comboBox1.Text = Cfg.device;
            this.checkBox1.Checked = Cfg.store;
            this.checkBox2.Checked = Cfg.auto;

            foreach (string adapter in NetworkInterfaceAvaliable.adapters_dict.Keys)
            {
                this.comboBox1.Items.Add(adapter);
            }
            if (NetworkInterfaceAvaliable.adapters_dict.ContainsKey(Cfg.device))
            {
                this.comboBox1.Text = Cfg.device;
            }
            else
            {
                this.comboBox1.Text = this.comboBox1.Items[0].ToString();
            }
            if (Cfg.auto)
            {
                button1_Click(button1, new EventArgs());
            }
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(OnPowerModeChanged);
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();            
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Cfg.username = textBox1.Text;
            Cfg.password = textBox2.Text;
            Cfg.device = comboBox1.Text;
            Cfg.store = checkBox1.Checked;
            Cfg.auto = checkBox2.Checked;
            if(checkBox1.Checked)
            {
                Cfg.Commit();
            }
            else
            {
                Cfg.username = null;
                Cfg.password = null;
                Cfg.store = false;
                Cfg.auto = false;
                Cfg.Commit();
            }
            RefComm.StartAuthThread(Cfg.username, Cfg.password, "\\Device\\NPF_" + NetworkInterfaceAvaliable.adapters_dict[Cfg.device], Cfg.VersionParser(Cfg.versionHEX), Cfg.H3C_key, Cfg.mode);
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = false;
            panel1.Enabled = false;
            textBox3.Text = "";
            timer1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                checkBox2.Enabled = true;
            }
            else
            {
                checkBox2.Enabled = false;
                checkBox2.Checked = false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            RefComm.StopAuthThread();
            panel1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            textBox3.AppendText("\r\n已登出");
            timer1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cfg.device = comboBox1.Text;
        }
        private void refreshText()
        {
            string tmp = RefComm.ReadLog();



            if (tmp.Length != 0)
            {
                if (textBox3.Text.Length > 1024)
                {
                    textBox3.Text = "";
                    textBox3.ScrollToCaret();
                }
                textBox3.ScrollToCaret();//自动滚屏                
                textBox3.AppendText(tmp);
                textBox3.ScrollToCaret();
            }
            //自动更新IP
            if (tmp.Contains("Success"))
            {
                NetworkInterfaceAvaliable.RefreshDHCP(NetworkInterfaceAvaliable.adapters_dict[comboBox1.Text]);
                textBox3.AppendText("IP Refreshed.\r\n");
                textBox3.ScrollToCaret();//自动滚屏
            }

        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000, "", "NXSharp正在运行！", ToolTipIcon.Warning);
            }
        }



        private void Form1_Activated(object sender, EventArgs e)
        {
            textBox3.ScrollToCaret();//自动滚屏到最下
        }
        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (button1.Enabled == false && button2.Enabled == true) //判断是否点击登录
            {
                switch (e.Mode)
                {
                    //系统挂起
                    case PowerModes.Suspend:
                        RefComm.StopAuthThread();
                        textBox3.AppendText("\r\n已登出");
                        break;
                    //系统恢复
                    case PowerModes.Resume:
                        RefComm.StartAuthThread(Cfg.username, Cfg.password, "\\Device\\NPF_" + NetworkInterfaceAvaliable.adapters_dict[Cfg.device], Cfg.VersionParser(Cfg.versionHEX), Cfg.H3C_key, Cfg.mode);
                        break;
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshText();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }
    }
}
