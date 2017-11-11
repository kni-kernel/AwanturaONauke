using System;
using System.Threading;

namespace AwanturaLib
{
    public class GameState
    {
        public Team[] Team { get; set; }
        public Question Question { get; set; }
        public Licitation Licictation { get; set; }
        public int Pull { get; set; }
        public int State { get; set; }
        enum States: int {Idle,Licitation,OneOnOne,Question,Hint};

    }
}

