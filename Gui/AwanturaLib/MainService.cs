using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AwanturaLib
{
    public class MainService
    {
        private const int DEAN_INDEX = 4; 
        public static Random Random { get; set; } = new Random();

        public void updateTeamPoints(GameState gamestate, int Index, int amount)
        {
            gamestate.Teams[Index].Points += amount;
        }

        private const int TeamCount = 5;

        public void setTeamName(GameState gamestate, String n, int Index)
        {
            gamestate.Teams[Index].Name = n;
        }

        public void setTeamClassName(GameState gamestate, String n, int Index)
        {
            gamestate.Teams[Index].ClassName = n;
        }


        void updateAllPoints(GameState gamestate)
        {
            for (int i = 0; i < TeamCount; i++)
            {
                updateTeamPoints(gamestate, i, -gamestate.Licitation.Bid[i]);
            }
        }
        public GameState SetPlayingPoints(GameState gs, int amount)
        {
            for (int i = 0; i < TeamCount; i++)
            {
                if (gs.Teams[i].isPlaying == true)
                    updateTeamPoints(gs, i, amount);
            }
            return gs;
        }

        public GameState ResetPoints(GameState gs, int amount)
        {
            for (int i = 0; i < TeamCount; ++i)
                gs = SetPoints(gs, i, amount);
            return gs;
        }
        public GameState SetPoints(GameState gs, int index, int amount)
        {
            gs.Teams[index].Points = amount;
            return gs;
        }

        //LICITATION
        public GameState StartLicitation(GameState gamestate)
        {
            gamestate.Licitation = new Licitation(gamestate);
            gamestate.State = States.Licitation;
            return gamestate;
        }

        public GameState Bet(GameState gamestate, int index, int amount)
        {
            if (gamestate.State == States.Licitation && amount > 0 && gamestate.Licitation.Bid.Max() < amount && gamestate.Teams[index].Points > 300)
                gamestate.Licitation.bet(gamestate, index, amount);

            return gamestate;
        }

        public GameState GoVabank(GameState gs, int index)
        {
            return Bet(gs, index, gs.Teams[index].Points);
        }

        public GameState BetWithoutRestrictions(GameState gamestate, int index, int amount)
        {
            if (gamestate.State == States.Licitation && amount > 0 && gamestate.Teams[index].Points > 300 && amount <= gamestate.Teams[index].Points)
                gamestate.Licitation.bet(gamestate, index, amount);

            return gamestate;
        }

        public GameState SetTimer(GameState gs, int Time, bool enabled)
        {
            if (gs.State == States.Hint || gs.State == States.Question)
            {
                gs.Timer = Time;
                gs.TimerEnabled = enabled;
            }
            return gs;
        }

        public GameState EndLicitationToBlackBox(GameState gamestate)
        {
            if (gamestate.State != States.Licitation)
                return gamestate;
            updateAllPoints(gamestate);
            gamestate.TimerEnabled = false;
            gamestate = AssignBlackBoxToTeam(gamestate, WinnerIndex(gamestate));
            gamestate.State = States.Idle;
            return gamestate;
        }

        public GameState EndOneOnOne(GameState gamestate, bool masterWon)
        {
            if (gamestate.State != States.OneOnOne)
                return gamestate;



            var qs = QuestionsSet.Current;
            var categoryName = gamestate.OneOnOneCategories.Where(c => c.Value).First().Key;

            if (qs.Questions[categoryName].Where(q => q.Used == false).Count() == 0)
            {
                MessageBox.Show("Nie ma już pytań w tej kategorii");
                return gamestate;
            }

            updateAllPoints(gamestate);
            gamestate.State = States.Question;
            if (masterWon)
                gamestate.CurrentTeam = DEAN_INDEX;
            else

            {
                for (int i = 0; i < DEAN_INDEX; ++i)
                    if (gamestate.Teams[i].isPlaying)
                        gamestate.CurrentTeam = i;
            }

            gamestate.TimerEnabled = false;
            gamestate = RandomQuestion(gamestate, categoryName, qs);
            gamestate.Question.Used = true;
            gamestate.Timer = 60;
            gamestate.TimerEnabled = true;
            return gamestate;
        }

        public GameState EndLicitationToQuestion(GameState gamestate, String CategoryName)
        {
            if (gamestate.State != States.Licitation)
                return gamestate;
            var qs = QuestionsSet.Current;

            if (qs.Questions[CategoryName].Where(q => q.Used == false).Count() == 0)
            {
                MessageBox.Show("Nie ma już pytań w tej kategorii");
                return gamestate;
            }

            updateAllPoints(gamestate);
            gamestate.State = States.Question;
            gamestate.CurrentTeam = WinnerIndex(gamestate);
            gamestate.TimerEnabled = false;
            gamestate = RandomQuestion(gamestate, CategoryName, qs);
            gamestate.Question.Used = true;
            gamestate.Timer = 60;
            gamestate.TimerEnabled = true;
            return gamestate;
        }
        public int MaxValue(GameState gs)
        {
            int maxValue = gs.Licitation.Bid.Max();

            return maxValue;
        }
        public int WinnerIndex(GameState gs)
        {
            int index = 0;
            for (int i = 0; i < TeamCount; i++)
            {
                if (gs.Licitation.Bid[i] == MaxValue(gs))
                {
                    index = i;
                }
            }
            return index;
        }
        public GameState EndLicitationToHint(GameState gamestate)
        {
            if (gamestate.State != States.Licitation)
                return gamestate;
            updateAllPoints(gamestate);
            gamestate.TimerEnabled = false;
            gamestate = AssignHint(gamestate, WinnerIndex(gamestate));
            gamestate.State = States.Idle;
            return gamestate;
        }


        //QUESTION
        public GameState RandomQuestion(GameState gamestate, String CategoryName, QuestionsSet qs)
        {

            gamestate.Question = qs.Questions[CategoryName].Where(q => q.Used == false)
                .TakeRandom(Random);
            gamestate.QuestionCount += 1;
            return gamestate;
        }


        public GameState RightGuess(GameState gamestate)
        {
            updateTeamPoints(gamestate, WinnerIndex(gamestate), gamestate.Licitation.Pool);//winner
            gamestate.Pool = 0;
            gamestate.State = States.Win;
            return gamestate;
        }


        public GameState ToIdle(GameState gamestate)
        {
            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState WrongGuessFirstRound(GameState gamestate)
        {
            gamestate.Pool = gamestate.Licitation.Pool;
            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState WrongGuessSecondRound(GameState gamestate)
        {
            gamestate.Pool = 0;
            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState UseHint(GameState gamestate)
        {
            int Index = WinnerIndex(gamestate);

            if (gamestate.Teams[Index].Hints > 0)
            {
                gamestate.Teams[Index].Hints -= 1;
                gamestate.State = States.Hint;
            }
            return gamestate;
        }

        //GETTING BLACKBOX OR HINT
        public GameState AssignBlackBoxToTeam(GameState gamestate, int Index)
        {
            gamestate.Teams[Index].BlackBox = true;
            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState AssignHint(GameState gamestate, int Index)
        {
            gamestate.Teams[Index].Hints += 1;
            gamestate.State = States.Idle;
            return gamestate;
        }


        //BEGGINNIG
        public GameState StartGame()
        {
            GameState gamestate = new GameState();
            gamestate.Teams = new Team[TeamCount];
            for (int i = 0; i < TeamCount; i++)
            {
                gamestate.Teams[i] = new Team();
            }

            gamestate.Teams[0].Name = "Niebiescy";
            gamestate.Teams[1].Name = "Zieloni";
            gamestate.Teams[2].Name = "Żółci";
            gamestate.Teams[3].Name = "Czerwoni";
            gamestate.Teams[4].Name = "Czarni";

            gamestate.Teams[0].ClassName = "blue";
            gamestate.Teams[1].ClassName = "green";
            gamestate.Teams[2].ClassName = "yellow";
            gamestate.Teams[3].ClassName = "red";
            gamestate.Teams[4].ClassName = "black";

            gamestate.Pool = 0;
            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState StartFirstRound(GameState gamestate)
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
            //setting question counter to 0
            gamestate.QuestionCount = 0;
            return gamestate;
        }

        public GameState BuyHint(GameState gameState, int price)
        {
            if (gameState.State != States.Question)
                return gameState;
            var team = gameState.Teams[gameState.CurrentTeam];
            if (team.Points < price)
                return gameState;

            team.Points -= price;
            gameState.State = States.Hint;
            return gameState;
        }

        public GameState StartSecondRound(GameState gamestate, int mastersPoints)
        {
            int maxValue = gamestate.Teams.Max(elem => elem.Points);
            int countMaxValue = gamestate.Teams.Where(elem => elem.Points == maxValue).Count();

            Array.ForEach(gamestate.Teams, elem => elem.isPlaying = false);
            if (countMaxValue > 1)
                return gamestate;
            int maxValueIndex = gamestate.Teams.ToList().IndexOf(gamestate.Teams.FirstOrDefault(elem => elem.Points == maxValue));
            gamestate.Teams[maxValueIndex].isPlaying = true;
            
            //deans
            gamestate.Teams[4].Points = mastersPoints;
            gamestate.Teams[4].isPlaying = true;

            //set question counter to 0
            gamestate.QuestionCount = 0;
            return gamestate;
        }


        //1 ON 1
        public GameState ToOneOnOne(GameState gamestate)
        {
            gamestate.State = States.OneOnOne;
            gamestate = OneOnOneCategories(gamestate, QuestionsSet.Current);
            gamestate.Licitation = new Licitation(gamestate, 500);
            return gamestate;
        }


        public GameState RemoveCategory(GameState gamestate, string categoryName)
        {
            if (gamestate.OneOnOneCategories.Count(x => x.Value == true) == 1) //jeśli jest jedna aktywna kategoria to jej nie usuwamy ;)
                return gamestate;

            gamestate.OneOnOneCategories[categoryName] = false;
            return gamestate;
        }


        public GameState OneOnOneCategories(GameState gamestate, QuestionsSet questionset)
        {
            gamestate.OneOnOneCategories = new Dictionary<string, bool>();
            var rand = new Random();
            do
            {
                var category = QuestionsSet.Current.Questions.Keys.ToArray().TakeRandom(rand);
                if (gamestate.OneOnOneCategories.ContainsKey(category) == false)
                {
                    gamestate.OneOnOneCategories.Add(category, true);
                }
            } while (gamestate.OneOnOneCategories.Keys.Count < 9);

            return gamestate;
        }

        public String[] GetCategoriesNames()
        {

            StorageService ss = new StorageService();
            QuestionsSet qs = ss.DeserializeFromXMLFile<QuestionsSet>(@"..\..\pytania.xml");
            return qs.Questions.Keys.ToArray();
        }

    }
}

