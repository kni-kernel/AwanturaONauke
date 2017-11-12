using System;
using System.Collections.Generic;


namespace AwanturaLib
{
    public class GameState {
        public Team[] Teams { get; set; }
        public Question Question { get; set; }
        public Licitation Licitation { get; set; }
        public int Pool { get; set; }
        public States State { get; set; } = States.Idle;
        public Dictionary<String, bool> Categories { get; set; }
    }
}

