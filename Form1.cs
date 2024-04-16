using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PortTunnel_forWindowsXP_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown( object sender, EventArgs e )
        {
            string s = File.ReadAllText("Settings.txt");

            tb_Settings.Text = s;
        }
    }
}
