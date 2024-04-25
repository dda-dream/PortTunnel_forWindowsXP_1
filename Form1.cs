using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
 
namespace PortTunnel_forWindowsXP_1
{
    public partial class Form1 : Form
    {
        string[] settingArray;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown( object sender, EventArgs e )
        {
            string s = File.ReadAllText("PortTunnelSettings.txt");
            tb_Settings.Text = s;
        }

        private void button1_Click( object sender, EventArgs e )
        {
            settingArray = tb_Settings.Text.Split('\n');
            listener_1();
            //listener_2();

        }

        void listener_1()
        {
            TcpForwarderSlim tunnel = new TcpForwarderSlim();
            tunnel.local = new IPEndPoint(IPAddress.Any, 4001);
            tunnel.remote = new IPEndPoint(IPAddress.Parse("172.18.12.47"), 3389);

            Thread InstanceCaller = new Thread(new ThreadStart(tunnel.Start));
            InstanceCaller.Start();
        }
        
        
    }
}
