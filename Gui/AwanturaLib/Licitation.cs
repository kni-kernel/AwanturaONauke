using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class Licitation
    {
        public int[] Bid { get; set; } = new int[4];
        public int WhoWin { get; set; }
        public int Pool { get; set; }

        public Licitation(GameState gameState, int startAmount = 200)
        {
            Pool = gameState.Pool;
            for (int i = 0; i < 4; i++)
            {
                if(gameState.Teams[i].isPlaying == true)
                {
                    this.Bid[i] = startAmount;
                    Pool += startAmount;
                }
            }
        }

        public void bet(GameState gameState, int index, int amount)
        {
           
                this.Bid[index] = Math.Min(gameState.Teams[index].Points, amount);
            

            Pool = gameState.Pool;
            for (int i = 0; i < 4; ++i)
                if (gameState.Teams[i].isPlaying)
                    Pool += Bid[i];
        }
        
    }
}
