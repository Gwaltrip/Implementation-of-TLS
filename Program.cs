using System;

namespace TLS
{
    class Program
    {
        static void Main(string[] args)
        {
            TLS tls = new TLS();
            Console.WriteLine("To run Client and Server, you must have two of the same prog running.\nTurn on server mode first.");
            Console.WriteLine("Enter 1 for Client Mode, 2 for Server Mode.");
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
