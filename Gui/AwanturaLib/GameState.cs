﻿using System;
namespace AwanturaLib
{
    public class GameState
    {
        public Team[] Teams { get; set; }
        public Question Question { get; set; }
        public Licitation Licictation { get; set; }
        public int Pool { get; set; }
        public States State { get; set; } = States.Idle;

    }
}

