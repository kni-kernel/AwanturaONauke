using System;
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
            for(int i=0; i<TeamCount;i++)
            {
                if (gs.Teams[i].isPlaying == true)
                    updateTeamPoints(gs, i,amount);
            }
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
            if(gamestate.State == States.Licitation && amount > 0 && gamestate.Licitation.Bid.Max() < amount && gamestate.Teams[index].Points > 300)
                gamestate.Licitation.bet(gamestate, index, amount);

            return gamestate;
        }

        public GameState BetWithoutRestrictions(GameState gamestate, int index, int amount)
        {
            if(gamestate.State == States.Licitation && amount > 0 && gamestate.Teams[index].Points > 300)
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
            if(gamestate.State != States.Licitation)
                return gamestate;
            updateAllPoints(gamestate);
            gamestate = AssignBlackBoxToTeam(gamestate, WinnerIndex(gamestate));
            gamestate.State = States.Idle;
            return gamestate;
        }


        public GameState EndLicitationToQuestion(GameState gamestate, String CategoryName)
        {
            if(gamestate.State != States.Licitation)
                return gamestate;
            updateAllPoints(gamestate);
            gamestate.State = States.Question;
            gamestate.CurrentTeam = WinnerIndex(gamestate);
            gamestate = RandomQuestion(gamestate, CategoryName, QuestionsSet.Current);
            gamestate.Timer = 60;
            gamestate.TimerEnabled = true;
            return gamestate;
        }
        public int MaxValue(GameState gs)
        {
            int maxValue= gs.Licitation.Bid.Max();

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
            if(gamestate.State != States.Licitation)
                return gamestate;
            updateAllPoints(gamestate);
            gamestate = AssignHint(gamestate, WinnerIndex(gamestate));
            gamestate.State = States.Idle;
            return gamestate;
        }
        

        //QUESTION
        public GameState RandomQuestion(GameState gamestate, String CategoryName, QuestionsSet qs)
        {
            gamestate.Question = qs.Questions[CategoryName].Where(q => q.Used == false)
                .TakeRandom(Random);
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
            for(int i=0; i< TeamCount; i++)
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


        public GameState StartSecondRound(GameState gamestate,  int mastersPoints)
        {
            //index of best team in first round
            int maxindex = 0;

            for (int i = 1; i < TeamCount-1; i++)
            {
                if (gamestate.Teams[maxindex].Points < gamestate.Teams[i].Points)
                    maxindex = i;
                else if (gamestate.Teams[maxindex].Points == gamestate.Teams[i].Points)
                {
                    return gamestate;
                }
            }

            for (int i = 0; i < TeamCount-1; i++)
            {
                gamestate.Teams[i].isPlaying = false;
            }
            
            gamestate.Teams[maxindex].isPlaying = true;
            
            //deans
            gamestate.Teams[4].Points = mastersPoints;
            gamestate.Teams[4].isPlaying = true;

            return gamestate;
        }


        //1 ON 1
        public GameState ToOneOnOne(GameState gamestate)
        {
            gamestate.State = States.OneOnOne;
            gamestate = OneOnOneCategories(gamestate, QuestionsSet.Current);
            return gamestate;
        }


        public GameState RemoveCategory(GameState gamestate, string categoryName)
        {
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
                if(gamestate.OneOnOneCategories.ContainsKey(category) == false)
                    {
                        gamestate.OneOnOneCategories.Add(category, true);
                   }
               } while(gamestate.OneOnOneCategories.Keys.Count < 9);
            
            return gamestate;
        }

        public String[] GetCategoriesNames() {

            StorageService ss = new StorageService();
            QuestionsSet qs = ss.DeserializeFromXMLFile<QuestionsSet>(@"..\..\pytania.xml");
            return qs.Questions.Keys.ToArray();
        }
       
    }   
}

