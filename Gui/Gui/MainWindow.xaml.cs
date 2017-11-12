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
        private static WebService webService = new WebService(8001);
        public ViewModel VM { get; set; } = new ViewModel();
        public object Keys { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            MainGrid.DataContext = VM;
            ImportCategories();
            var gs = mainService.StartGame();
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
                    VM.Timer--;
        }

        public void ImportCategories()
        {
            /// do stworzenia menu z kategoriami na starcie
            VM.Categories.Add("Muzyka", true);
            VM.Categories.Add("Fizyka", true);
            VM.Categories.Add("Seriale", false);

            /// dodanie kategorii z listy z Service
            foreach (var category in VM.Categories)
            {
                var item = new MenuItem();
                item.Header = category.Key;
                item.Name = category.Key;
                item.IsEnabled = category.Value;
                item.Click += new RoutedEventHandler(this.onCategoryClick);

                CategoriesMenu.Items.Add(item);
            }

        }
        public void ImportGameState(GameState gameState)
        {
            VM.gameState = gameState;

            Team[] Teams = new Team[5];
            for (int i = 0; i < 5; i++)
                Teams[i] = new Team();

            Teams[0].Name = "WFIS";
            Teams[1].Name = "WIMIC";
            Teams[2].Name = "WEIP";
            Teams[3].Name = "WMS";
            Teams[4].Name = "Marsjanie";

            foreach (var team in Teams)
            {
                team.Points = 5000;
                team.isPlaying = true;
                team.Hints = 0;
                team.ClassName = "";
            }

            VM.gameState.Teams = Teams;
            VM.Team1 = VM.gameState.Teams[0].Name;
            VM.Team2 = VM.gameState.Teams[1].Name;
            VM.Team3 = VM.gameState.Teams[2].Name;
            VM.Team4 = VM.gameState.Teams[3].Name;
            VM.Team5 = VM.gameState.Teams[4].Name;

            VM.inGame1 = VM.gameState.Teams[0].isPlaying;
            VM.inGame2 = VM.gameState.Teams[1].isPlaying;
            VM.inGame3 = VM.gameState.Teams[2].isPlaying;
            VM.inGame4 = VM.gameState.Teams[3].isPlaying;
            VM.inGame5 = VM.gameState.Teams[4].isPlaying;

            VM.Saldo1 = VM.gameState.Teams[0].Points;
            VM.Saldo2 = VM.gameState.Teams[1].Points;
            VM.Saldo3 = VM.gameState.Teams[2].Points;
            VM.Saldo4 = VM.gameState.Teams[3].Points;
            VM.Saldo5 = VM.gameState.Teams[4].Points;

          //  VM.Timer = VM.gameState.time
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
            //Dictionary<String, List<Question>> questions = new Dictionary<String, List<Question>>();
            //podać nazwe kategorii i pobrać do listy List<Questions>

            //wyslac wybrana kategorie do servera
            //pobrać wylosowane pytanie
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
            string text = (string)tb.Text;
            int bid;
            if (int.TryParse(text, out bid))
            {
                tb.Text = (bid * 100).ToString();
                tb.Focus();
            }
        }
        
        private void SetAnswerToSend(TextBox tb)
        {

        }
        
    }
}
