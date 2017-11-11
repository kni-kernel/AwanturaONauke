using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class Licitation
    {
        public int Team1Licitation { get; set; }
        public int Team2Licitation { get; set; }
        public int Team3Licitation { get; set; }
        public int Team4Licitation { get; set; }
        public int Pool { get; set; }
        public int WhoWin { get; set; }

        public Licitation(int Team1Licitation, int Team2Licitation, int Team3Licitation, int Team4Licitation)
        {
            this.Team1Licitation = Team1Licitation;
            this.Team2Licitation = Team2Licitation;
            this.Team3Licitation = Team3Licitation;
            this.Team4Licitation = Team4Licitation;
            this.Pool = Team1Licitation + Team2Licitation + Team3Licitation + Team4Licitation;
        }

        public void EndLicitation(int WhoWin)
        {
            this.WhoWin = WhoWin;
        }
    }
}
