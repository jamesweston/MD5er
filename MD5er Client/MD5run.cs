using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Sockets;
using Microsoft.VisualBasic.Devices;
using System.Threading;

namespace MD5er_Client
{
    class MD5run
    {
private string original;
    private  long bottom;
    private long top;
    private NetworkStream clientStream;
    private Computer myComputer = new Computer();
    private volatile bool _shouldStop;

    public MD5run(string original, long bottom, long top, NetworkStream clientStream)
        {
            this.original = original;
            this.bottom = bottom;
            this.top = top;
            this.clientStream = clientStream;
        }

    public void RequestStop()
    {
        _shouldStop = true;
    }


        public void checkHash()
        {
            while (!_shouldStop)
            {
                long count = bottom;
                double perc;
                //string original = textBox1.Text;
                string tocheck = "";
                for (count = bottom; count < top; count++)
                {
                    tocheck = count.ToString();
                    //perc = (Convert.ToDouble(count) / Convert.ToDouble(top)) * 100.0;
                    //Console.WriteLine(perc.ToString("0.000000"));
                    if (original.CompareTo(generateHash(tocheck)) == 0)
                    {
                        Console.WriteLine("Found clear text is " + tocheck);
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        byte[] buffer = encoder.GetBytes("DONE" + tocheck);
                        clientStream.Write(buffer, 0, buffer.Length);
                        clientStream.Flush();
                        count = top;
                        Thread.CurrentThread.Abort();
                        //break;
                    }
                    if (count % 60000 == 0)
                    {
                        //Console.WriteLine("Upto " + tocheck + " checked.");
                        try
                        {
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        byte[] buffer = encoder.GetBytes("PRGS" + tocheck);
                        clientStream.Write(buffer, 0, buffer.Length);
                        clientStream.Flush();
                        }
                        catch (System.IO.IOException ex)
                        {
                        Console.WriteLine("Server Offline");
                        }   
                    }
                    if (_shouldStop)
                        break;
                } //end of the FOR Loop
            }
            Console.WriteLine("Term");
        }

        private string generateHash(string input)
        {
            //the method used here to generate the MD5 hash is a standard method provided by Microsoft
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        }
    }

