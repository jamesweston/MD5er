using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.Devices;


namespace MD5er_Server
{
  public class TcpServer
  {
    private TcpListener tcpListener;
    private Thread listenThread;
    private ArrayList connectedClients = new ArrayList();
    private UpdateLog log = new UpdateLog();
    Computer myComputer = new Computer();

    public ArrayList ConnectedClients
    {
        get { return connectedClients; }
        set { connectedClients = value; }
    }

    public TcpServer()
    {
      this.tcpListener = new TcpListener(IPAddress.Any, 4503);
      this.listenThread = new Thread(new ThreadStart(ListenForClients));
      this.listenThread.IsBackground = true;
      this.listenThread.Start();
      lock (Program.Data)
      {
          Program.Data.Add("Server Online");
      }
      log.Update();
    }

    private void ListenForClients()
    {
        this.tcpListener.Start();

        while (true)
        {
            //blocks until a client has connected to the server
            TcpClient client = this.tcpListener.AcceptTcpClient();
            ConnectedClients.Add(client);
            //create a thread to handle communication
            //with connected client
            Thread clientThread = new Thread(new ParameterizedThreadStart(ClientHandshake));
            clientThread.IsBackground = true;
            clientThread.Start(client);
        }
    }

    private void ClientHandshake(object client)
    {
        TcpClient tcpClient = (TcpClient)client;
        NetworkStream clientStream = tcpClient.GetStream();
        string clienthost = "";
        byte[] message = new byte[4096];
        int bytesRead;

        while (true)
        {
            bytesRead = 0;

            try
            {
                //blocks until a client sends a message
                bytesRead = clientStream.Read(message, 0, 4096);
            }
            catch
            {
                //a socket error has occured
                lock (Program.Data)
                {
                    Program.Data.Add("CTEB");
                }
                log.Update();
                break;
            }

            if (bytesRead == 0)
            {
                //the client has disconnected from the server
                lock (Program.Data)
                {
                    Program.Data.Add("CTER");
                    log.Update();
                }
                break;
            }

            //message has successfully been received
            ASCIIEncoding encoder = new ASCIIEncoding();

            if (encoder.GetString(message, 0, bytesRead).StartsWith("PRGS") == true)
            {
                lock (Program.Data)
                {
                    string temp = encoder.GetString(message, 0, bytesRead).Remove(0, 4);
                    Program.StoreRecover = System.Convert.ToInt64(temp);
                    Program.Data.Add(clienthost + " Upto " + temp );
                }
                log.Update();
                clientStream.Flush();
            }
            else if (encoder.GetString(message, 0, bytesRead).StartsWith("DONE") == true)
            {
                lock (Program.Data)
                {
                    Program.Data.Add(clienthost + " Found clear text is " + encoder.GetString(message, 0, bytesRead).Remove(0, 4));
                }
                log.Update();
                clientStream.Flush();
                log.Stop(this);
            }
            else if (encoder.GetString(message, 0, bytesRead).StartsWith("CRDY") == true)
            {
                clienthost = encoder.GetString(message, 0, bytesRead).Remove(0, 4);
                lock (Program.Data)
                {
                    Program.Data.Add("Client " + encoder.GetString(message, 0, bytesRead).Remove(0, 4) + " is ready");
                }
                log.Update();
                clientStream.Flush();
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
                //ASCIIEncoding encoder2 = new ASCIIEncoding();
                //byte[] buffer = encoder2.GetBytes("SRDY" + myComputer.Name);
                //Program.Data.Add("SEND " + encoder2.GetString(buffer, 0, buffer.Length));
                //log.Update();
                //clientStream.Write(buffer, 0, buffer.Length);
                //clientStream.Flush();
            }
        }
        ConnectedClients.Remove(client);
        tcpClient.Close();
    }
  }
}
