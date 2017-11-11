using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class Licitation
    {
        public int []Bid { get; set; }
        public int Pool { get; set; }
        public int WhoWin { get; set; }

        public Licitation(int[]Bid, int Pool)
        {
            this.Bid = new int[4];
            this.Bid[0] = Bid[0];
            this.Bid[1] = Bid[1];
            this.Bid[2] = Bid[2];
            this.Bid[3] = Bid[3];
            this.Pool = Pool + Bid[0] + Bid[1] + Bid[2] + Bid[3];
        }

        public void EndLicitation(int WhoWin)
        {
            this.WhoWin = WhoWin;
        }
    }
}
