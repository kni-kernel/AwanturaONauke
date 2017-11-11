using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class Licitation
    {
        public int Team1Bid { get; set; }
        public int Team2Bid { get; set; }
        public int Team3Bid { get; set; }
        public int Team4Bid { get; set; }
        public int Pool { get; set; }
        public int WhoWin { get; set; }

        public Licitation(int Team1Bid, int Team2Bid, int Team3Bid, int Team4Bid)
        {
            this.Team1Bid = Team1Bid;
            this.Team2Bid = Team2Bid;
            this.Team3Bid = Team3Bid;
            this.Team4Bid = Team4Bid;
            this.Pool = Team1Bid + Team2Bid + Team3Bid + Team4Bid;
        }

        public void EndLicitation(int WhoWin)
        {
            this.WhoWin = WhoWin;
        }
    }
}
