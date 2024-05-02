using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        Thread[] threadsStarted;
        int      threadsStartedCount=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown( object sender, EventArgs e )
        {
            string s = File.ReadAllText("PortTunnelSettings.txt");
            tb_Settings.Text = s;
        }

        void button1_Click( object sender, EventArgs e )
        {
            settingArray = tb_Settings.Text.Split('\n');
            process_SettingsFile();
        }

        void process_SettingsFile()
        {
            string[] settingsArr = tb_Settings.Text.Split('\n');
            threadsStarted = new Thread[10];

            foreach ( string s in settingsArr )
            {
                string listenAddress="", connectAddress="";
                int    listenPort=0, connectPort=0;
                string[] lineArr = s.Split(' ');
                foreach ( string s2 in lineArr )
                {
                    if(s2.StartsWith("listenAddress="))
                        listenAddress = s2.Replace("listenAddress=", "").Trim();
                    if(s2.StartsWith("connectAddress="))
                        connectAddress = s2.Replace("connectAddress=","").Trim();
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
            string s = String.Format("listener_start({0}, {1}, {2}, {3})",listenAddress, connectAddress, listenPort, connectPort);
            LogAdd(s);
            TcpForwarderSlim tunnel = new TcpForwarderSlim();
            tunnel.local = new IPEndPoint(IPAddress.Any, listenPort);
            tunnel.remote = new IPEndPoint(IPAddress.Parse(connectAddress), connectPort);

            Thread InstanceCaller = new Thread(new ThreadStart(tunnel.Start));

            InstanceCaller.Start();
            InstanceCaller.Name = s;

            threadsStarted[threadsStartedCount] = InstanceCaller;
            threadsStartedCount++;

            LogAdd(tunnel.startResult);
        }
        
        void LogAdd(string message)
        { 
            tb_Log.Text = tb_Log.Text +DateTime.Now.ToShortTimeString()+" : "+ message + Environment.NewLine;
            tb_Log.SelectionStart = tb_Log.TextLength;
            tb_Log.ScrollToCaret();
        }

        void button_Stop_Click( object sender, EventArgs e )
        {
            for(int i = 0; i < threadsStarted.Length; i++)
            {
                if(threadsStarted[i] != null)
                {
                    if(threadsStarted[i].ThreadState == System.Threading.ThreadState.Running)
                    { 
                        threadsStarted[i].Abort();
                        LogAdd(threadsStarted[i].Name+" aborting.");
                    }
                }
            }
        }
    }
}
