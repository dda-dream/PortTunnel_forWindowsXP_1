using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PortTunnel_forWindowsXP_1
{
    class ControllerClass
    {
        TcpForwarderSlim[]  threadsStarted = new TcpForwarderSlim[10];
        int                 threadsStartedCount=0;
        TextBox             tb_Log;
        public void parm_tb_Log( TextBox _tb_Log )
        {
            tb_Log = _tb_Log;
        }
        public void LogAdd(string message)
        { 
            tb_Log.Text = tb_Log.Text +DateTime.Now.ToShortTimeString()+" : "+ message + Environment.NewLine;
            tb_Log.SelectionStart = tb_Log.TextLength;
            tb_Log.ScrollToCaret();
        }
        public string ReadSettingsFile()
        { 
            LogAdd("Read settings file.");
            string              s = "";
            s = File.ReadAllText("PortTunnelSettings.txt");
            return s;
        }
        public void process_SettingsFile(string settingsStr)
        {
            string[] settingsArr = settingsStr.Split('\n');

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
            TcpForwarderSlim tunnel = new TcpForwarderSlim();
            tunnel.local = new IPEndPoint(IPAddress.Any, listenPort);
            tunnel.remote = new IPEndPoint(IPAddress.Parse(connectAddress), connectPort);
            tunnel.name = String.Format("thread parameters: ({0}, {1}, {2}, {3})",listenAddress, connectAddress, listenPort, connectPort);
            LogAdd("START: "+tunnel.name);
            tunnel.StartListening();
            LogAdd("startResult: "+tunnel.startResult);

            Thread InstanceCaller = new Thread(new ThreadStart(tunnel.Start));

            InstanceCaller.Start();
            InstanceCaller.Name = tunnel.name;

            threadsStarted[threadsStartedCount] = tunnel;
            threadsStartedCount++;
        }
        public void stop_threads()
        {
            for(int i = 0; i < threadsStarted.Length; i++)
            {
                if(threadsStarted[i] != null)
                {
                    var thread = threadsStarted[i];
                    LogAdd("STOP: "+thread.name);
                    thread.Stop();
                    threadsStarted[i] = null;
                }
            }
        }


    }
}
