using System;

namespace TLS
{
    class Program
    {
        static void Main(string[] args)
        {
            TLS tls = new TLS();
            string str = Console.ReadLine();

            if (str.Equals("1"))
            {
                tls.Client();
            }
            else
            {
                tls.Server();   
            }
        }
    }
}
