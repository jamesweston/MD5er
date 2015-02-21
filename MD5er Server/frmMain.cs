using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

namespace MD5er_Server
{
    public partial class frmMain : Form
    {
        public TcpServer server;
        private UpdateLog log = new UpdateLog();
        private bool serverStarted = false;
        private int connectedClientsVal = 0;
        private Int64 checklimit = 2147483647;
        private Int64 checkrange = 0;
        private Int64 checkbottom = 0;
        private Int64 checktop = 0;
        private ArrayList MD5Range;

        Computer myComputer = new Computer();

        public frmMain()
        {
            InitializeComponent();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                foreach (var x in properties.UnicastAddresses)
                {
                    if (x.Address.AddressFamily == AddressFamily.InterNetwork)
                        if (IPAddress.IsLoopback(x.Address) == false)
                        {
                            lblIpaddress.Text = "IP: " + x.Address.ToString();
                            Console.WriteLine(" IPAddress ........ : {0:x}", x.Address.ToString());
                        }
                }
            } 
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            server = new TcpServer();
            serverStarted = true;
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            foreach (TcpClient i in server.ConnectedClients)
            {
                System.Diagnostics.Debug.WriteLine(i.Client.Connected);
                NetworkStream clientStream = i.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes("Hello Client!");
                clientStream.Write(buffer, 0, buffer.Length);
                ASCIIEncoding encoder2 = new ASCIIEncoding();
                lock (Program.Data)
                {
                    Program.Data.Add("SEND " + encoder2.GetString(buffer, 0, buffer.Length));
                }
                log.Update();
                clientStream.Flush();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                log.Stop(server);
               
            } 
            Properties.Settings.Default.Save();
        }

        public byte[] getByteArrayWithObject(object o)
        {
            /*

                1) Create a new MemoryStream class with the CanWrite property set to true
                (should be by default, using the default constructor).

                2) Create a new instance of the BinaryFormatter class.

                3) Pass the MemoryStream instance and your object to be serialized to the
                Serialize method of the BinaryFormatter class.

                4) Call the ToArray method on the MemoryStream class to get a byte array
                with the serialized data.

            */


            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }

        public object getObjectWithByteArray(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;

            return bf1.Deserialize(ms);
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            if (serverStarted == false)
            {
               server = new TcpServer();
              
            }
            connectedClientsVal = server.ConnectedClients.Count;
            checkrange = (this.checklimit / this.connectedClientsVal);
            checktop = checkrange;
            checkbottom = 0;
            for (int i = 0; i < server.ConnectedClients.Count; i++)
            {
                TcpClient temp = (TcpClient)server.ConnectedClients[i];
                MD5Range = new ArrayList() { checkbottom, checktop, tbHash.Text };
                checkbottom = checktop + 1;
                checktop = checktop + checkrange;
                NetworkStream clientStream = temp.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes("STRT");
                clientStream.Write(buffer, 0, buffer.Length);
                lock (Program.Data)
                {
                    Program.Data.Add("SEND " + encoder.GetString(buffer, 0, buffer.Length));
                }
                log.Update();
                byte[] buffer2 = getByteArrayWithObject(MD5Range);
                clientStream.Write(buffer2, 0, buffer2.Length);
                clientStream.Flush();
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            log.Stop(server);
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (serverStarted == false)
            {
                server = new TcpServer();

            }
            connectedClientsVal = server.ConnectedClients.Count;
            checkrange = (this.checklimit / this.connectedClientsVal);
            checktop = checkrange;
            checkbottom = Program.StoreRecover;
            for (int i = 0; i < server.ConnectedClients.Count; i++)
            {
                TcpClient temp = (TcpClient)server.ConnectedClients[i];
                MD5Range = new ArrayList() { checkbottom, checktop, tbHash.Text };
                checkbottom = checktop + 1;
                checktop = checktop + checkrange;
                NetworkStream clientStream = temp.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes("STRT");
                clientStream.Write(buffer, 0, buffer.Length);
                lock (Program.Data)
                {
                    Program.Data.Add("SEND " + encoder.GetString(buffer, 0, buffer.Length));
                }
                log.Update();
                byte[] buffer2 = getByteArrayWithObject(MD5Range);
                clientStream.Write(buffer2, 0, buffer2.Length);
                clientStream.Flush();
            }
        }

        private void btnCRestore_Click(object sender, EventArgs e)
        {
            Program.StoreRecover = 0;
        }

        
    }
    public class UpdateLog
    {
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public void Update()
        {
            object temp = null;

            lock (Program.Data)
            {
            System.Collections.IEnumerator myEnumerator = Program.Data.GetEnumerator();
            while ( myEnumerator.MoveNext() ){
              ThreadSafeSet(Convert.ToString(myEnumerator.Current));
             temp = myEnumerator.Current;
            }
            Program.Data.Remove(temp);
            }
        }

        private void SetLog(string txt)
        {
            //Program.MainForm.rtbLog.AppendText(txt);
            //Program.MainForm.rtbLog.AppendText(Environment.NewLine);
            //Program.MainForm.rtbLog.Text += txt + "\r\n";
            //Program.MainForm.rtbLog.Focus();
            //Program.MainForm.rtbLog.SelectionStart = Program.MainForm.rtbLog.Text.Length - 1;
            //Program.MainForm.rtbLog.ScrollToCaret();
            Program.MainForm.rtbLog.SuspendLayout();
            Program.MainForm.rtbLog.AppendText(txt + "\r\n");
            Program.MainForm.rtbLog.ScrollToBottom();
            Program.MainForm.rtbLog.ResumeLayout();
        }

        private delegate void ThreadSetter(string txt);

        public void ThreadSafeSet(string txt)
        {
            Program.MainForm.rtbLog.BeginInvoke(new ThreadSetter(SetLog), txt);
        }

        public void Stop(TcpServer s)
        {
            foreach (TcpClient i in s.ConnectedClients)
            {
                NetworkStream clientStream = i.GetStream();
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes("STOP");
                clientStream.Write(buffer, 0, buffer.Length);
                lock (Program.Data)
                {
                    Program.Data.Add("SEND " + encoder.GetString(buffer, 0, buffer.Length));
                }
                this.Update();
                clientStream.Flush();
            }
        }

    }
}
