using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TLS
{
    class Client: Messager
    {
        private int port;
        public int Port { set { port = value; } }
        private TcpClient tcp;
        private UTF8Encoding utf;

        public Client(int port)
        {
            this.port = port;
            tcp = new TcpClient();
            utf = new UTF8Encoding();
        }

        public Client()
        {
            tcp = new TcpClient();
            utf = new UTF8Encoding();
        }

        public void Connect()
        {
            tcp.Connect("127.0.0.1", port);
        }

        public string Recieve()
        {
            return null;
        }

        public void Send(string message)
        {
            try
            {
                byte[] mess;
                mess = utf.GetBytes(message);
                tcp.GetStream().Write(mess, 0, mess.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
