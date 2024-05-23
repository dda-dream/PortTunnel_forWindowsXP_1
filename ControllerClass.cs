using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PortTunnel_forWindowsXP_1
{
    public class ControllerClass
    {

        TcpForwarderSlim[]  threadsStarted = new TcpForwarderSlim[10];
        int                 threadsStartedCount=0;
        public string ReadSettingsFile()
        { 
            Form1._Form1.LogAdd("Read settings file.");
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
            tunnel.local  = new IPEndPoint(IPAddress.Parse(listenAddress),  listenPort);
            tunnel.remote = new IPEndPoint(IPAddress.Parse(connectAddress), connectPort);
            tunnel.name = String.Format("thread parameters: ({0}, {1}, {2}, {3})",listenAddress, connectAddress, listenPort, connectPort);
            Form1._Form1.LogAdd("START: "+tunnel.name);
            tunnel.StartListening();
            Form1._Form1.LogAdd("startResult: "+tunnel.OperationExecutionResult);

            Thread InstanceCaller = new Thread(new ThreadStart(tunnel.StartTunnelConnection));

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
                    Form1._Form1.LogAdd("STOP: "+thread.name);
                    long bc = thread.GetbytesCount();
                    Form1._Form1.LogAdd("Bytes: "+bc.ToString());
                    thread.Stop();
                    threadsStarted[i] = null;
                }
            }
        }

        public int TEST_METHOD_1(int a, int b)
        {
            return a + b;
        }

    }
}
