using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
