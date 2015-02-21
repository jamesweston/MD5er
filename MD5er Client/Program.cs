using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.Devices;
namespace MD5er_Client
{
    public class Program
    {
        private static int port = 0;
        private static IPAddress ipaddress;
        private static Thread md5Thread;
        private static MD5run i1;
        private static ArrayList MD5Range;

        public static IPAddress Ipaddress
        {
            get { return Program.ipaddress; }
            set 
            {
                if (value == null)
                {
                    Program.ipaddress = IPAddress.Parse("10.0.0.1");
                }
                else
                {
                    Program.ipaddress = value; 
                }
                
            }
        }
        public static int Port
        {
            get { return port; }
            set 
            {
                if (value == 0)
                {
                    port = 4503;
                }
                else
                {
                    port = value;
                }
            }
        }

        public byte[] getByteArrayWithObject(Object o)
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
            bf1.Serialize(ms,o);
            return ms.ToArray();
        }

        public static T getObjectWithByteArray<T>(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;

            return (T) bf1.Deserialize(ms);
        }

        public static IPEndPoint Connect(string ip, string port)
        {
        if (String.IsNullOrEmpty(port) == true)
            { Port = 0; } else {Port = Convert.ToInt32(port);}
            if (String.IsNullOrEmpty(ip) == true)
            { Ipaddress = null; } else { Ipaddress = IPAddress.Parse(ip); }
            IPEndPoint serverEndPoint = new IPEndPoint(Ipaddress, Port);
            return serverEndPoint;
        }

        static void Main(string[] args)
        {
            start:
            Console.Title = "MD5er Client";
            Computer myComputer = new Computer();
            Console.WriteLine("Welcome to MD5er Client");
            Console.WriteLine("-----------------------");
            Console.Write("Please enter ip address: (Default: 10.0.0.1)");
            string tempip = Console.ReadLine();
            Console.Write("Please enter port: (Default: 4503)");
            string temp = Console.ReadLine();
            bool structobj = false;
            Console.WriteLine("-----------------------");
            TcpClient client = new TcpClient();
            try
            {
               client.Connect(Connect(tempip, temp));
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Server not found");
                goto start;
            }
            NetworkStream clientStream = client.GetStream();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes("CRDY" + myComputer.Name);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            byte[] message = new byte[9000];
            int bytesRead;

            while (true)
            {
               bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 9000);
               }
                catch
                {
                  // a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    //break;
                }

                //message has successfully been received
                ASCIIEncoding encoder2 = new ASCIIEncoding();
                if (encoder2.GetString(message, 0, bytesRead) == "STRT")
                {
                    structobj = true;
                    clientStream.Flush();
                }
                else if (structobj == true)
                {
                    MD5Range = new ArrayList(getObjectWithByteArray<ArrayList>(message));
                    i1 = new MD5run(Convert.ToString(MD5Range[2]), Convert.ToInt64(MD5Range[0]), Convert.ToInt64(MD5Range[1]), clientStream);
                    md5Thread = new Thread(new ThreadStart(i1.checkHash));
                    //md5Thread.IsBackground = true;
                    md5Thread.Start();
                    Console.WriteLine("Working...");
                    structobj = false;
                }
                else if (encoder2.GetString(message, 0, bytesRead) == "STOP")
                {
                    try
                    {
                        i1.RequestStop();
                        md5Thread.Join();
                        i1 = null;
                        md5Thread = null;
                        MD5Range.Clear();
                        MD5Range = null;
                        clientStream.Flush();
                    }
                    catch (NullReferenceException es)
                    {
                        Console.WriteLine("STOP error");
                    }
                }
                else
                {
                    //Console.WriteLine(encoder2.GetString(message, 0, bytesRead));
                }
            }
            Console.WriteLine("Server Offline");
            Console.Write("Closing in: ");
            //Console.CursorTop--; 
            for (int i = 5; i > 0; i--)
            {
                Console.Write(i  + "\b");
                System.Threading.Thread.Sleep(1000);
            }
            //Console.ReadLine();
        }
        
    }
}
