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
            var ws = new WebService(8001);
            var ms = new MainService();

            var gs = ms.StartGame();
            gs = ms.StartFirtRound(gs);
            
            
            

            ws.UpdateGameState(gs);

        }
    }
}
