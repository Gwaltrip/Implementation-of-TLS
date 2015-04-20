using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TLS
{
    interface Messager
    {
        void Send(string message);
        string Recieve();
    }
}
