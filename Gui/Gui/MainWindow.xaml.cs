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
            get { return VM.gameState; }
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
            gs = mainService.StartFirstRound(gs);
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
                    this.Dispatcher.Invoke(() =>
                    {
                        VM.gameState = mainService.SetTimer(GS, VM.Timer, true);
                    });

                    webService.UpdateGameState(GS);
                }
        }

        public void ImportCategories()
        {
            /// do stworzenia menu z kategoriami na starcie

            String[] categories = mainService.GetCategoriesNames();
            foreach (var category in categories)
            {
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
           // lock (VM)
            {
                VM.gameState = gameState;

                VM.State = GS.State.ToString();
                VM.Pool = GS.Pool;
                VM.Timer = gameState.Timer;
                VM.TimerEnabled = gameState.TimerEnabled;
                VM.Question = gameState?.Question?.Content;
                VM.Hint1 = gameState?.Question?.Tip1;
                VM.Hint2 = gameState?.Question?.Tip2;
                VM.Hint3 = gameState?.Question?.Tip3;
                VM.Hint4 = gameState?.Question?.Tip4;



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

                if (GS.OneOnOneCategories != null)
                {
                    OnOnOneActions.Items.Clear();
                    if (GS.OneOnOneCategories.Where(c => c.Value == true).Count() == 1)
                    {
                        var item = new MenuItem();
                        item.Header = "mistrzowie wygrali 1 na 1";
                        item.Tag = "DEAN";
                        item.Click += new RoutedEventHandler(this.on1On1End);

                        OnOnOneActions.Items.Add(item);

                        item = new MenuItem();
                        item.Header = "druga drużyna wygrała 1 na 1";
                        item.Click += new RoutedEventHandler(this.on1On1End);

                        OnOnOneActions.Items.Add(item);
                    }
                    else
                        foreach (var category in GS.OneOnOneCategories)
                        {
                            if (category.Value)
                            {
                                var item = new MenuItem();
                                item.Header = category.Key;
                                item.Click += new RoutedEventHandler(this.onDeleteCategoryClick);

                                OnOnOneActions.Items.Add(item);
                            }
                        }
                }
                VM.Timer = VM.gameState.Timer;
            }
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

        private void on1On1End(object sender, RoutedEventArgs args)
        {
            bool isDeanWin = false;
            if (((sender as MenuItem)?.Tag as string) == "DEAN")
                isDeanWin = true;

            GS = mainService.EndOneOnOne(GS, isDeanWin);
        }

        private void onCategoryClick(object sender, RoutedEventArgs args)
        {   
            var menuItem = (sender as MenuItem);
            var categoryName = (string)menuItem.Header;

            VM.ChosenCategory = categoryName;

            GS = mainService.EndLicitationToQuestion(GS, categoryName);
        }

        private void onDeleteCategoryClick(object sender, RoutedEventArgs args)
        {
            var menuItem = (sender as MenuItem);
            var categoryName = (string)menuItem.Header;

            GS = mainService.RemoveCategory(GS, categoryName);
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
            GS = mainService.UseHint(GS);
        }

        private void OfferHint(object sender, RoutedEventArgs args)
        {
            var tb = HintCost;
            int price;
            if (int.TryParse(tb.Text, out price) && price > 0)
            {
                GS = mainService.GoToBuyableHint(GS, price);
            }
        }

        private void NotAcceptHint(object sender, RoutedEventArgs args)
        {
            GS = mainService.DoNotBuyHint(GS);
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
            UpdateALL(mainService.StartSecondRound(GS, 10000));

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
            int price;
            string txt = (HintCost)?.Text;
            if (txt != null)
            {
                if (int.TryParse(txt, out price) && price > 0)
                {
                    GS = mainService.BuyHint(GS);
                }
            }
            /// button WEŹ PODPOWIEDŹ
            /// z tym że musi sprawdzić czy drużyna może użyć za darmo podpowiedzi
            /// albo poprosić o kwote do zapłaty
        }

        private void startTimeButton_Click(object sender, RoutedEventArgs e)
        {
            VM.TimerEnabled = true;
            GS = mainService.SetTimer(GS, VM.Timer, true);
        }

        private void stopTimeButton_Click(object sender, RoutedEventArgs e)
        {
            VM.TimerEnabled = false;
            lock (VM)
            {
                GS = mainService.SetTimer(GS, VM.Timer, false);
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            string str = ResetAmount.Text?.ToString();
            int amount;
            if (int.TryParse(str, out amount))
            {
                GS = mainService.ResetPoints(GS, amount);
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
            GS = GS; //refresh XD
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

        private void KeyUpTeam(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int teamNumber;
                string tag = (sender as TextBox)?.Tag as string;

                if (int.TryParse(tag, out teamNumber))
                {
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
                int teamNumber;
                if (tb != null && int.TryParse(txt, out teamNumber))
                {
                   
                    int bid;

                    if (tb.Text == "V" || tb.Text == "v")
                    {
                        GS = mainService.GoVabank(GS, teamNumber);
                    }
                    else if (int.TryParse(tb.Text as string, out bid))
                    {
                        bid *= 100;
                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                            GS = mainService.BetWithoutRestrictions(GS, teamNumber, bid);
                        else
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

        private void SaldoKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sender is TextBox)
                {
                    var tb = sender as TextBox;
                    int amount;
                    int teamNumber;

                    if (int.TryParse(tb.Tag as string, out teamNumber) && int.TryParse(tb.Text, out amount))
                    {
                        GS = mainService.SetPoints(GS, teamNumber, amount);
                    }
                }
            }
        }
    }
}
