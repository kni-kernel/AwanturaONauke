using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwanturaLib;
using System.Net;


namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new WebService();
            ws.OpenTcpConnection(IPAddress.Parse("127.0.0.1"), 80);
        }
    }
}
