using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class MainService
    {

        public GameState Bet(GameState gamestate, int index, int amount)
        {
            gamestate.Licictation.bet(gamestate, index, amount);
            return gamestate;
        }

        public void EndLicitation(GameState gameState)
        {
            int maxValue = this.Bid.Max();
            int maxIndex = anArray.ToList().IndexOf(maxValue);

        }
    }
}
