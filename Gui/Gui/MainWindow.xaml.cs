using AwanturaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xamlC
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static MainService mainService = new MainService();
        private static WebService webService;
        public GameState GS
        {
            get => VM.gameState;
            set { UpdateALL(value); }
        }
        public ViewModel VM { get; set; } = new ViewModel();
        public object Keys { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            MainGrid.DataContext = VM;
            ImportCategories();
            webService = new WebService(8002);
            var gs = mainService.StartGame();
            gs = mainService.StartFirtRound(gs);
            ImportGameState(gs);
            webService.UpdateGameState(gs);


            VM.TimerEnabled = true;
            VM.Timer = 60;

            System.Timers.Timer aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }


        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (VM.TimerEnabled)
                lock (VM)
                {
                    VM.Timer--;
                    GS.Timer = VM.Timer;
                    webService.UpdateGameState(GS);
                }
        }

        public void ImportCategories()
        {
            /// do stworzenia menu z kategoriami na starcie

            String[] categories = mainService.GetCategoriesNames();
            foreach(var category in categories) {
                VM.Categories.Add(category, true);
            }

            /// dodanie kategorii z listy z Service
            foreach (var category in VM.Categories)
            {
                var item = new MenuItem();
                item.Header = category.Key;
                item.IsEnabled = category.Value;
                item.Click += new RoutedEventHandler(this.onCategoryClick);

                CategoriesMenu.Items.Add(item);
            }

        }
        public void ImportGameState(GameState gameState)
        {
            VM.gameState = gameState;

            VM.State = GS.State.ToString();
            VM.Pool = GS.Pool;

            for (int i = 0; i < 5; ++i)
            {
                VM.TeamNames[i] = GS.Teams[i].Name;
                VM.Points[i] = GS.Teams[i].Points;
                VM.ArePlaying[i] = GS.Teams[i].isPlaying;
                VM.HintCount[i] = GS.Teams[i].Hints;
                if (GS.Licitation != null)
                {
                    VM.Bids[i] = GS.Licitation.Bid[i];
                }
            }
            VM.Timer = VM.gameState.Timer;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        /*
        private void TextBox_KeyDown(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                
            }
        }
        */

        private void categoriesMenu_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
            VM.Answer = (sender as Button).ContextMenu.Name;
        }

        private void onCategoryClick(object sender, RoutedEventArgs args)
        {
            var menuItem = (sender as MenuItem);
            var categoryName = (string)menuItem.Header;

            VM.ChosenCategory = categoryName;

            GS = mainService.EndLicitationToQuestion(GS, categoryName);
        }

        private void RoundWin(object sender, RoutedEventArgs args)
        {
            GS = mainService.RightGuess(GS);
        }

        private void RoundWinStop(object sender, RoutedEventArgs args)
        {
            GS.State = States.Idle;
            UpdateALL(GS);
        }

        private void RoundLose(object sender, RoutedEventArgs args)
        {
            GS = mainService.WrongGuessFirstRound(GS);
        }

        private void ShowHintIfHave(object sender, RoutedEventArgs args)
        {
           // GS = mainService.UseHint(GS);
        }

        private void StartLicitation(object sender, RoutedEventArgs args)
        {
            VM.gameState = mainService.StartLicitation(VM.gameState);
            ImportGameState(GS);
            webService.UpdateGameState(VM.gameState);
        }

        private void EndLicitationHint(object sender, RoutedEventArgs args)
        {
            GS = mainService.EndLicitationToHint(GS);
        }

        private void EndLicitationBlackBox(object sender, RoutedEventArgs args)
        {
            GS = mainService.EndLicitationToBlackBox(GS);
        }

        private void StartOneOnOne(object sender, RoutedEventArgs args)
        {
            GS = mainService.ToOneOnOne(VM.gameState);
        }

        private void StartMasters(object sender, RoutedEventArgs args)
        {
            UpdateALL(mainService.StartSecondRound(GS, 0, 5000, 5000));
            
        }

        private void UpdateALL(GameState GS)
        {
            ImportGameState(GS);
            webService.UpdateGameState(GS);
        }

        // button do wyswietlania obrazka podpiętego pod pytanie
        private void imgButton_Click(object sender, RoutedEventArgs e)
        {
            /// zastosować ścieżkę do wyświetlenia gdzies obok zdj
            /// jesli istnieje
            /// obrazek wywoływany dopiero po kliknięciu
            /// VM.UrlImage
        }

        // wykorzystaj podpowiedź
        private void useHintButton_Click(object sender, RoutedEventArgs e)
        {

            /// button WEŹ PODPOWIEDŹ
            /// z tym że musi sprawdzić czy drużyna może użyć za darmo podpowiedzi
            /// albo poprosić o kwote do zapłaty
        }

        private void startTimeButton_Click(object sender, RoutedEventArgs e)
        {
            VM.TimerEnabled = true;
        }

        private void stopTimeButton_Click(object sender, RoutedEventArgs e)
        {
            VM.TimerEnabled = false;
            lock(VM)
                VM.Timer = -1;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            string str = ResetAmount.Text?.ToString();
            int amount;
            if (int.TryParse(str, out amount))
            {

            }
        }

        private void OnWindowKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
                SetLicitationForUse(Bid1);
            if (e.Key == Key.F2)
                SetLicitationForUse(Bid2);
            if (e.Key == Key.F3)
                SetLicitationForUse(Bid3);
            if (e.Key == Key.F4)
                SetLicitationForUse(Bid4);
            if (e.Key == Key.F5)
                SetLicitationForUse(Bid5);
            if (e.Key == Key.F12)
                SetAnswerToSend(Answer);
        }

        private void SetLicitationForUse(TextBox tb)
        {
            tb.Text = "";
            tb.Focus();
            string text = (string)tb.Text;
        }
        
        private void SetAnswerToSend(TextBox tb)
        {

        }


        private void KeyUpPool(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int amount;
                string txt = (sender as TextBox)?.Text;

                if (int.TryParse(txt, out amount))
                {
                    VM.gameState.Pool = amount;
                    ImportGameState(VM.gameState);
                    webService.UpdateGameState(VM.gameState);
                }
            }
        }

        private void KeyUpTeam(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                int teamNumber;
                string tag = (sender as TextBox)?.Tag as string;

                if (int.TryParse(tag, out teamNumber)) {
                    mainService.setTeamName(VM.gameState, (sender as TextBox)?.Text, teamNumber);
                    //VM.gameState.Teams[teamNumber].Name = (sender as TextBox)?.Text;
                    ImportGameState(VM.gameState);
                    webService.UpdateGameState(VM.gameState);
                }
            }
        }
        private void BidKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var tb = sender as TextBox;
                string txt = tb.Tag as string;

                if (tb != null)
                {
                    int teamNumber;
                    int bid;
                    
                    if (int.TryParse(txt, out teamNumber) && int.TryParse(tb.Text as string, out bid))
                    {
                        bid *= 100;
                        GS = mainService.Bet(GS, teamNumber, bid);
                        ImportGameState(GS);
                        webService.UpdateGameState(GS);
                    }
                }
            }
        }

        private void playingChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;

            if (checkBox != null)
            {
                int? tag = getTeamNumberFromTag(checkBox.Tag);
                if (tag.HasValue)
                {
                    GS.Teams[tag.Value].isPlaying = checkBox.IsChecked == true;
                    UpdateALL(GS);
                }
            }
        }

        private int? getTeamNumberFromTag(object tag)
        {
            string strTag = tag as string;
            if (strTag != null)
            {
                int number;
                if (int.TryParse(strTag, out number))
                    return number;
            }
            return null;
        }
    }
}
