using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class MainService
    {


        int WinnerOfLicitation(GameState gameState)
        {
            int maxValue = gameState.Licitation.Bid.Max();
            return gameState.Licitation.Bid.ToList().IndexOf(maxValue);

        }

        public void updateTeamPoints(GameState gamestate, int Index, int amount)
        {
            gamestate.Teams[Index].Points += amount;

        }
        void UpdateAllPoints(GameState gamestate)
        {
            for (int i = 0; i < 4; i++)
            {
                updateTeamPoints(gamestate, i, -gamestate.Licitation.Bid[i]);
            }
        }

        public GameState Bet(GameState gamestate, int index, int amount)
        {
            gamestate.Licitation.bet(gamestate, index, amount);
            return gamestate;
        }
        public GameState RightGuess(GameState gamestate, int Index)
        {
            updateTeamPoints(gamestate, Index, gamestate.Pool);//winner
            gamestate.Pool = 0;
            return gamestate;
        }
        public GameState WrongGuess(GameState gamestate)
        {
            gamestate.Licitation = new Licitation(gamestate);
            return gamestate;
        }

        public GameState AssignBlackBox(GameState gamestate, int Index, BlackBox blackbox)
        {
            gamestate.Teams[Index].BlackBox = blackbox;
            return gamestate;
        }
        public GameState AssignHint(GameState gamestate, int Index, BlackBox blackbox)
        {
            gamestate.Teams[Index].Hints += 1;
            return gamestate;
        }
        public GameState UseHint(GameState gamestate, int Index)
        {
            gamestate.Teams[Index].Hints -= 1;
            return gamestate;
        }

        public GameState EndLicitation(GameState gamestate)
        {
            UpdateAllPoints(gamestate);
            return gamestate;
        }


        public GameState RemoveCategory(GameState gamestate, Category category)
        {
            gamestate.Categories.Remove(category.Name);
            return gamestate;
        }
        public GameState StartFirtRound(GameState gamestate)
        {
            gamestate.Teams[0].Name = "Niebiescy";
            gamestate.Teams[0].Name = "Zieloni";
            gamestate.Teams[0].Name = "Żółci";
            gamestate.Teams[0].Name = "Czarni";

            gamestate.Teams[0].ClassName = "blue";
            gamestate.Teams[0].ClassName = "green";
            gamestate.Teams[0].ClassName = "yellow";
            gamestate.Teams[0].ClassName = "black";

            //without deans
            foreach (var team in gamestate.Teams)
            {
                if (team.ClassName == "black")
                    continue;

                team.Points = 5000;
                team.Hints = 0;
                team.isPlaying = true;

                
            }
            
            return gamestate;
        }
    }   
}

