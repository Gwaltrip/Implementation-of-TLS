﻿using System;
using System.Text;
using System.Threading;

namespace TLS
{
    class TLS
    {
        private StringBuilder sb;
        private Authentication auth;
        private static RSA rsa;
        private static RC4 rc4;
        private static Server s;
        private static Client c;

        /// <summary>
        ///  A static method which handles messages being sent.
        /// </summary>
        public static void OutGoingMessage()
        {
            string message = "";
            while (!message.ToLower().Equals("stop"))
            {
                Console.Write("Sending: ");
                message = Console.ReadLine();
                c.Send(rc4.Message(message));
                Console.Write("Cypher Text: ");
                Console.WriteLine(rc4.Message(message));
                Thread.Sleep(10);
            }
        }

        /// <summary>
        ///  A static method which handles messages being recieved.
        /// </summary>
        public static void InComingMessage()
        {
            string message = "";
            while (!rc4.Message(message).ToLower().Equals("stop"))
            {
                message = s.Recieve().Replace("\0", string.Empty);
                Console.Write("Message: ");
                Console.WriteLine(rc4.Message(message));
                Console.Write("Cypher Text: ");
                Console.WriteLine(message);
                Thread.Sleep(10);
            }
        }

        /// <summary>
        ///  Serialize a value with a key.
        /// </summary>
        public string XMLSend(string key, string value)
        {
            sb = new StringBuilder();
            return sb.Append("<").Append(key).Append(">").Append(value).Append("<\\").Append(key).Append(">").ToString();
        }

        /// <summary>
        ///  Creates an instance of a Client.
        /// </summary>
        public void Client()
        {
            auth = new Authentication();
            rsa = new RSA("00a92cd736f374db51", 0xd4b96f4f, 0xcb97635f);
            rc4 = new RC4();
            rc4.KeyGenerator();
            RSA rsaServer = new RSA();
            c = new Client(34567);

            Console.WriteLine("::::Client Config::::");
            Console.Write("Port: ");
            Console.WriteLine((34568).ToString());
            Console.Write("N: ");
            Console.WriteLine(rsa.N);
            Console.Write("D: ");
            Console.WriteLine(rsa.D);

            c.Connect();

            Console.WriteLine("Sending port: " + 34568);
            c.Send((34568).ToString());
            Thread.Sleep(10);
            c.Send(rsa.N);
            Thread.Sleep(10);
            c.Send(rsa.D);

            s = new Server(34568);
            s.Start();
            rsaServer.N = s.Recieve();
            Thread.Sleep(10);
            rsaServer.D = s.Recieve();
            Thread.Sleep(10);
            rsaServer.E = s.Recieve();
            Thread.Sleep(10);
            Console.WriteLine("::::Server Public Keys::::");
            Console.WriteLine("N: " + rsaServer.N);
            Console.WriteLine("D " + rsaServer.D);
            rc4.Key = rsa.Decrypt(s.Recieve().Replace("\0", string.Empty));
            Console.WriteLine("RC4 Key: \"" + rc4.Key + "\"");
            string sign = s.Recieve().Replace("\0", string.Empty);
            if (!auth.Verify(rc4.Key, sign, rsaServer.E, rsaServer.N))
                throw new Exception("Invalid Signature!");
            Console.WriteLine("Verifcation successful!");
            Thread.Sleep(10);

            try
            {
                Thread outMessage = new Thread(new ThreadStart(OutGoingMessage));
                outMessage.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        ///  Creates an instance of a Server.
        /// </summary>
        public void Server()
        {
            auth = new Authentication();
            rsa = new RSA("00e8a4f6b1c06a554d", 0xf8fbaa77, 0xef336b5b);
            rc4 = new RC4();
            rc4.KeyGenerator();
            RSA rsaClient;
            s = new Server(34567);
            c = new Client();

            Console.WriteLine("::::Server Config::::");
            Console.Write("Port: ");
            Console.WriteLine((34567).ToString());
            Console.Write("N: ");
            Console.WriteLine(rsa.N);
            Console.Write("D: ");
            Console.WriteLine(rsa.D);

            s.Start();

            string port = s.Recieve();

            Console.WriteLine("Receive port: " + port.Replace("\0",string.Empty));
            c.Port = Int32.Parse(port);
            Thread.Sleep(10);
            rsaClient = new RSA(s.Recieve(),s.Recieve());
            Console.WriteLine("::::Client public keys:::::");
            Console.Write("N Client: ");
            Console.WriteLine(rsaClient.N);
            Thread.Sleep(10);
            Console.Write("E Client: ");
            Console.WriteLine(rsaClient.E);

            c.Connect();
            c.Send(rsa.N);
            Thread.Sleep(10);
            c.Send(rsa.D);
            Thread.Sleep(10);
            c.Send(rsa.E);
            Thread.Sleep(10);
            Console.WriteLine("RC4 Key: " + rc4.Key);
            c.Send(rsaClient.Encrypt(rc4.Key));
            Console.WriteLine("Signature: " + auth.Sign(rc4.Key, rsa.D, rsa.N));
            c.Send(auth.Sign(rc4.Key, rsa.D, rsa.N));
            Thread.Sleep(10);

            try
            {
                Thread inMessage = new Thread(new ThreadStart(InComingMessage));
                inMessage.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
