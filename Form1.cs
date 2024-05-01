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

        void process_SettingsFile()
        {

            string[] settingsArr = tb_Settings.Text.Split('\n');
            foreach ( string s in settingsArr )
            {
                string listenAddress="", connectAddress="";
                int    listenPort=0, connectPort=0;
                string[] lineArr = s.Split(' ');
                foreach ( string s2 in lineArr )
                {
                    if(s2.StartsWith("listenAddress="))
                        listenAddress = s2.Replace("listenAddress=", "");
                    if(s2.StartsWith("connectAddress="))
                        connectAddress = s2.Replace("connectAddress=","");
                    if(s2.StartsWith("listenPort="))
                        listenPort =  int.Parse(s2.Replace("listenPort=", ""));
                    if(s2.StartsWith("connectPort="))
                        connectPort = int.Parse(s2.Replace("connectPort=", ""));
                }
            
                if(listenAddress != "" && connectAddress != "" && listenPort > 0 && connectPort > 0)                    
                    listener_start(listenAddress, connectAddress, listenPort, connectPort);
            }

        }
        void listener_start(string listenAddress, string connectAddress, int listenPort, int connectPort)
        {
            TcpForwarderSlim tunnel = new TcpForwarderSlim();
            tunnel.local = new IPEndPoint(IPAddress.Any, listenPort);
            tunnel.remote = new IPEndPoint(IPAddress.Parse(connectAddress), connectPort);

            Thread InstanceCaller = new Thread(new ThreadStart(tunnel.Start));
            InstanceCaller.Start();
        }
        
        
    }
}
