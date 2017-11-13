﻿using System;
using System.Collections.Generic;


namespace AwanturaLib
{
    public class GameState {
        public Team[] Teams { get; set; }
        public int CurrentTeam { get; set; }
        public Question Question { get; set; }
        public Licitation Licitation { get; set; }
        public int Pool { get; set; }
        public States State { get;
            set; } = States.Idle;
        public Dictionary<string, bool> OneOnOneCategories { get; set; }
        public int Timer { get; set; }
    }
}

