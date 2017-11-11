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

        public int WinnerOfLicitation(GameState gameState)
        {
            int maxValue = gameState.Licictation.Bid.Max();
            return gameState.Licictation.Bid.ToList().IndexOf(maxValue);


        }
        public void updateTeamPoints(GameState gamestate, int Index, int amount)
        {
            gamestate.Teams[Index].Points += amount;
        }
        

        public GameState RightGuess(GameState gamestate, int Index)
        { 
          updateTeamPoints(gamestate, Index, gamestate.Pool);
          return gamestate;
        }
        public GameState WrongGuess(GameState gamestate)
        {
            return gamestate;
        }
    }
}
