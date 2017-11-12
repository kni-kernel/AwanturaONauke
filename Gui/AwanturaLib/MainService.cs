﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public class MainService
    {
        public static Random Random { get; set; } = new Random();
        public void updateTeamPoints(GameState gamestate, int Index, int amount)
        {
            gamestate.Teams[Index].Points += amount;
        }


        void updateAllPoints(GameState gamestate)
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
            gamestate.State = States.Win;
            return gamestate;
        }


        public GameState WrongGuessFirstRound(GameState gamestate)
        {
            gamestate.State = States.Idle;
            return gamestate;
        }
        public GameState WrongGuessSecondRound(GameState gamestate)
        {
            gamestate.Pool = 0;
            gamestate.State = States.Idle;
            return gamestate;   
        }

        public GameState AssignBlackBoxToTeam(GameState gamestate, int Index, BlackBox blackbox)
        {
            gamestate.Teams[Index].BlackBox = blackbox;
            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState AssignHint(GameState gamestate, int Index, BlackBox blackbox)
        {
            gamestate.Teams[Index].Hints += 1;
            gamestate.State = States.Idle;
            return gamestate;
        }

 
        public GameState UseHint(GameState gamestate, int Index)
        {
            if (gamestate.Teams[Index].Hints > 0)
            {
                gamestate.Teams[Index].Hints -= 1;
                gamestate.State = States.Hint;
            }
                return gamestate;
        }


        public GameState EndLicitationToBlackBox (GameState gamestate)
        {
            updateAllPoints(gamestate);
            gamestate.State = States.BlackBox;
            return gamestate;
        }
        public GameState EndLicitationToQuestion(GameState gamestate)
        {
            updateAllPoints(gamestate);
            gamestate.State = States.Question;
            return gamestate;
        }
        public GameState EndLicitationToHint(GameState gamestate)
        {
            updateAllPoints(gamestate);
            gamestate.State = States.GetHint;
            return gamestate;
        }

        public GameState RemoveCategory(GameState gamestate, Category category)
        {
            gamestate.Categories.Remove(category.Name);
            return gamestate;
        }


        public GameState StartGame(GameState gamestate)
        {

            gamestate.Teams[0].Name = "Niebiescy";
            gamestate.Teams[0].Name = "Zieloni";
            gamestate.Teams[0].Name = "Żółci";
            gamestate.Teams[0].Name = "Czarni";

            gamestate.Teams[0].ClassName = "blue";
            gamestate.Teams[0].ClassName = "green";
            gamestate.Teams[0].ClassName = "yellow";
            gamestate.Teams[0].ClassName = "black";

            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState StartFirtRound(GameState gamestate)
        { 
            //without deans
            foreach (var team in gamestate.Teams)
            {
                if (team.ClassName == "black")
                {
                    //deans are not playin
                    team.isPlaying = false;
                    continue;
                }

                team.Points = 5000;
                team.Hints = 0;
                team.isPlaying = true;              
            }
                return gamestate;
        }


        public GameState StartSecondRound(GameState gamestate, int index, int mastersPoints, int noobsPoints)
        {
            //deans
            gamestate.Teams[4].Points = mastersPoints;
            gamestate.Teams[4].isPlaying = true;
            //second team
            gamestate.Teams[index].Points = noobsPoints;
            gamestate.Teams[index].isPlaying = true;
            return gamestate;
        }

        public GameState RandomQuestion(GameState gamestate, List<Question> questions)
        {
            gamestate.Question = questions.Where(q => q.Used == false)
                .TakeRandom(Random);
            gamestate.Question.Used = true;
            return gamestate;
        }
    }   
}

