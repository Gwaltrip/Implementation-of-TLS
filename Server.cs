using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TLS
{
    class Server: Messager
    {
        private int port;
        private IPAddress ipa;
        private TcpListener tcp;
        private Socket socket;
        private UTF8Encoding utf;

        public Server(int port)
        {
            this.port = port;
            ipa = IPAddress.Parse("127.0.0.1");
            tcp = new TcpListener(ipa, port);
            utf = new UTF8Encoding();
        }

        public Server()
        {
            this.port = 34567;
            ipa = IPAddress.Parse("127.0.0.1");
            tcp = new TcpListener(ipa, port);
            utf = new UTF8Encoding();
        }

        public void Start()
        {
            tcp.Start();
            socket = tcp.AcceptSocket();
        }

        public string Recieve()
        {
            byte[] utfChar = new byte[256];
            try
            {
                int buffer = socket.Receive(utfChar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return utf.GetString(utfChar);
        }

        public void Send(string message)
        {

        }
    }
}
