using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace PortTunnel_forWindowsXP_1
{
    public class TcpForwarderSlim
    {
        private readonly Socket _mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public IPEndPoint local;
        public IPEndPoint remote;
        public string name = "NotInitialized";
        public string OperationExecutionResult="OK";
        public static long bytesCount;
        public long GetbytesCount()
        { 
            return bytesCount; 
        }
        public void StartListening()
        { 
            Form1._Form1.LogAdd("StartListening() called.");
            try {
                _mainSocket.Bind(local);
                _mainSocket.Listen(10);
            } catch (Exception ex)
            {
                //if error Сделана попытка доступа к сокету методом, запрещенным правами доступа
                //net stop hns
                //net start hns
                OperationExecutionResult = ex.Message.ToString();
                MessageBox.Show(OperationExecutionResult);
            }        
        }
        public void StartTunnelConnection()
        {
            try {
                while (true)
                {
                    var source = _mainSocket.Accept();
                    Form1._Form1.LogAdd(String.Format("Connection accepted {0:} -> {1:} ",source.RemoteEndPoint,source.LocalEndPoint));
                    var destination = new TcpForwarderSlim();
                    var state = new State(source, destination._mainSocket);
                    destination.Connect(remote, source);
                    source.BeginReceive(state.Buffer, 0, state.Buffer.Length, 0, OnDataReceive, state);
                    
                }
            } catch (Exception ex)
            {
                OperationExecutionResult = ex.ToString();
                Form1._Form1.LogAdd(OperationExecutionResult);
                //MessageBox.Show(OperationExecutionResult);
            }
        }
 
        private void Connect(EndPoint remoteEndpoint, Socket destination)
        {
            try
            {
                var state = new State(_mainSocket, destination);
                _mainSocket.Connect(remoteEndpoint);
                _mainSocket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, OnDataReceive, state );

                Form1._Form1.LogAdd(String.Format("Tunnel established: {0:} -> {1:} ",destination.LocalEndPoint, remoteEndpoint.ToString()));
            } catch (Exception ex)
            {
                OperationExecutionResult = ex.ToString();
                Form1._Form1.LogAdd(OperationExecutionResult);
                //MessageBox.Show(OperationExecutionResult);
            }
        }
 
        private static void OnDataReceive(IAsyncResult result)
        {
            var state = (State)result.AsyncState;
            try
            {
                var bytesRead = state.SourceSocket.EndReceive(result);
                bytesCount += bytesRead;
                if (bytesRead > 0)
                {
                    state.DestinationSocket.Send(state.Buffer, bytesRead, SocketFlags.None);
                    state.SourceSocket.BeginReceive(state.Buffer, 0, state.Buffer.Length, 0, OnDataReceive, state);
                } else { 
                    Form1._Form1.LogAdd("try: bytesCount: "+bytesCount.ToString());
                }
            }
            catch
            {
                string srcInfo = state.SourceSocket.Connected ? state.SourceSocket.LocalEndPoint +" -> "+ state.SourceSocket.RemoteEndPoint : "- -> -";
                string dstInfo = state.DestinationSocket.Connected ? state.DestinationSocket.LocalEndPoint +" -> "+ state.DestinationSocket.RemoteEndPoint : "- -> -";
                Form1._Form1.LogAdd(String.Format("catch. {0:}      {1:}  ", srcInfo, dstInfo ));

                if(state.SourceSocket.Connected == false || state.DestinationSocket.Connected == false)
                { 
                    Form1._Form1.LogAdd(String.Format("Disconnect detected. source {0:} destination {1:}",state.SourceSocket.Connected, state.DestinationSocket.Connected));
                }
                Form1._Form1.LogAdd("catch: bytesCount: "+bytesCount.ToString());
                state.DestinationSocket.Close();
                state.SourceSocket.Close();
            }
        }

        public void Stop()
        {
            try
            {
                _mainSocket.Close();
            }
            catch(Exception ex)
            {
                OperationExecutionResult = ex.ToString();
                Form1._Form1.LogAdd(OperationExecutionResult);
            }
        }
        private class State
        {
            public Socket SourceSocket { get; private set; }
            public Socket DestinationSocket { get; private set; }
            public byte[] Buffer { get; private set; }
 
            public State(Socket source, Socket destination)
            {
                SourceSocket = source;
                DestinationSocket = destination;
                Buffer = new byte[8192];
            }
        }
    }}
