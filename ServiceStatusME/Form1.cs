using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceStatusME
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Service name for checking
        ServiceController sc = new ServiceController("your service name");

        // manually start checking service status
        private void button1_Click(object sender, EventArgs e)
        {
            sc.Refresh();

            // set service status to a label on the form
            string status = sc.Status.ToString();
            label1.Text = status;

            // start timer - every 1s it will check status of the service
            timer1.Enabled = true;
        }

        // just display notification window (after service is started)
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Show();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            // hide app from the taskbar and show icon in the tray
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        //show app window
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal)
            {
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        // runn app in the tray after startup instead of on the taskbar
        private void Form1_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

            button1_Click(sender, e);
        }

        // just display notification window (after service is stopped)
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 a = new Form3();
            a.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //check service status
            sc.Refresh();
            string status = sc.Status.ToString();
            label1.Text = status;

            // set label to app status
            if (timer1.Enabled == true)
            {
                label2.Text = "Checking is running";
            }
            else
            {
                label2.Text = "Checking stopped";
            }
        }

        //after service status changed, open proper window
        private void label1_TextChanged(object sender, EventArgs e)
        {
            if (label1.Text == "Running")
            {
                button2_Click(sender, e);
            }
            if (label1.Text == "Stopped")
            {
                button3_Click(sender, e);
            }
        }
    }
}
