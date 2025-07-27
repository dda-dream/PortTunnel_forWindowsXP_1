using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
 
namespace PortTunnel_forWindowsXP_1
{
    public partial class Form1 : Form
    {
        public static Form1 _Form1;
        ControllerClass ControllerClass = new ControllerClass();
        public Form1()
        {
            InitializeComponent();
            _Form1 = this;
        }
        private void Form1_Shown( object sender, EventArgs e )
        {
            tb_Settings.Text = ControllerClass.ReadSettingsFile();
            LogAdd("Form1_Shown");
        }
        void button1_Click( object sender, EventArgs e )
        {
            ControllerClass.process_SettingsFile(tb_Settings.Text);
        }        
        void button_Stop_Click( object sender, EventArgs e )
        {
            ControllerClass.stop_threads();
        }
        public void LogAdd(string message)
        { 
            if (InvokeRequired)
            {
                if(Form1._Form1 != null)
                    this.Invoke(new Action<string>(LogAdd), new object[] {message});
                return;
            }

            tb_Log.Text = tb_Log.Text +" "+ DateTime.Now.ToShortDateString() + " "+DateTime.Now.ToLongTimeString()+" : "+ message + Environment.NewLine;
            tb_Log.SelectionStart = tb_Log.TextLength;
            tb_Log.ScrollToCaret();
        }

        private void Form1_FormClosing( object sender, FormClosingEventArgs e )
        {
            ControllerClass.stop_threads();
        }

        private void Form1_Load( object sender, EventArgs e )
        {

        }
    }
}
