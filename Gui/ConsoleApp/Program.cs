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
            gs = ms.StartLicitation(gs);

            gs = ms.Bet(gs, 0, 1000);
            gs = ms.Bet(gs, 1, 2000);
            gs = ms.Bet(gs, 2, 2200);
            gs.Pool = 5000;


            gs = ms.EndLicitationToHint(gs);


            gs = ms.StartLicitation(gs);

            gs = ms.Bet(gs, 0, 300);
            gs = ms.Bet(gs, 3, 400);
            gs = ms.Bet(gs, 2, 500);

            //gs = ms.CreateBlackBox(gs, "whatever");

            //gs = ms.EndLicitationToBlackBox(gs, gs.BlackBox);

            ws.UpdateGameState(gs);

        }
    }
}
