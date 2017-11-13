using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwanturaLib;
using System.Net;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new WebService(8002);
            var ms = new MainService();

            int interval = 2500;

            var gs = ms.StartGame();
            gs = ms.StartFirtRound(gs);
            update(ws, interval, gs);
            gs = ms.StartLicitation(gs);
            update(ws, interval, gs);
            gs = ms.Bet(gs, 0, 1000);
            update(ws, interval, gs);
            gs = ms.Bet(gs, 1, 2000);
            update(ws, interval, gs);
            gs = ms.Bet(gs, 2, 2200);
            update(ws, interval, gs);
            gs.Pool = 5000;


            gs = ms.EndLicitationToHint(gs);
            update(ws, interval, gs);

            gs = ms.StartLicitation(gs);
            update(ws, interval, gs);
            gs = ms.Bet(gs, 0, 300);
            update(ws, interval, gs);
            gs = ms.Bet(gs, 3, 400);
            update(ws, interval, gs);
            gs = ms.Bet(gs, 2, 500);
            update(ws, interval, gs);

            gs = ms.EndLicitationToBlackBox(gs);
            update(ws, interval, gs);

        }

        private static void update(WebService ws, int interval, GameState gs)
        {
            Thread.Sleep(interval);

            ws.UpdateGameState(gs);
        }
    }
}
