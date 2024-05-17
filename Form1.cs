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
        ControllerClass ControllerClass = new ControllerClass();
        public Form1()
        {
            InitializeComponent();
            ControllerClass.parm_tb_Log(tb_Log);
        }
        private void Form1_Shown( object sender, EventArgs e )
        {
            tb_Settings.Text = ControllerClass.ReadSettingsFile();
            ControllerClass.LogAdd("Form1_Shown");
        }
        void button1_Click( object sender, EventArgs e )
        {
            ControllerClass.process_SettingsFile(tb_Settings.Text);
        }        
        void button_Stop_Click( object sender, EventArgs e )
        {
            ControllerClass.stop_threads();
        }
        private void Form1_FormClosing( object sender, FormClosingEventArgs e )
        {
            ControllerClass.stop_threads();
        }
    }
}
