using AwanturaLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gui
{
    public class ViewModel : INotifyPropertyChanged
    {
        // public Category Categories { get; set; } = new Category();
        public GameState gameState { get; set; } = new GameState();

        // kategoria pytań z Service
        public Dictionary<string, bool> Categories { get; set; } = new Dictionary<string, bool>();

        public Team[] Teams { get; set; } = new Team[4];

        private string chosenCategory; public string ChosenCategory
        {
            get { return chosenCategory; }
            set { chosenCategory = value; OnPropertyChanged(); }
        }

        // pytania pobierane z Service
        private string question; public string Question
        {
            get { return question; }
            set { question = value; OnPropertyChanged(); }
        }

        // podpowiedzi pobrane z pytaniem z Service
        private string hint1; public string Hint1
        {
            get { return hint1; }
            set { hint1 = value; OnPropertyChanged(); }
        }
        private string hint2; public string Hint2
        {
            get { return hint2; }
            set { hint2 = value; OnPropertyChanged(); }
        }
        private string hint3; public string Hint3
        {
            get { return hint3; }
            set { hint3 = value; OnPropertyChanged(); }
        }
        private string hint4; public string Hint4
        {
            get { return hint4; }
            set { hint4 = value; OnPropertyChanged(); }
        }

        //obrazek do pobrania ???
        private string urlImage; public string UrlImage
        {
            get { return urlImage; }
            set { urlImage = value; OnPropertyChanged(); }
        }


        //nazwy zespolow
        private string team1; public string Team1
        {
            get { return team1; }
            set { team1 = value; OnPropertyChanged(); }
        }
        private string team2; public string Team2
        {
            get { return team2; }
            set { team2 = value; OnPropertyChanged(); }
        }
        private string team3; public string Team3
        {
            get { return team3; }
            set { team3 = value; OnPropertyChanged(); }
        }
        private string team4; public string Team4
        {
            get { return team4; }
            set { team4 = value; OnPropertyChanged(); }
        }
        private string team5; public string Team5
        {
            get { return team5; }
            set { team5 = value; OnPropertyChanged(); }
        }

        // druzyny w grze CHECKBOX
        private bool ingame1; public bool inGame1
        {
            get { return ingame1; }
            set { ingame1 = value; OnPropertyChanged(); }
        }
        private bool ingame2; public bool inGame2
        {
            get { return ingame2; }
            set { ingame2 = value; OnPropertyChanged(); }
        }
        private bool ingame3; public bool inGame3
        {
            get { return ingame3; }
            set { ingame3 = value; OnPropertyChanged(); }
        }
        private bool ingame4; public bool inGame4
        {
            get { return ingame4; }
            set { ingame4 = value; OnPropertyChanged(); }
        }
        private bool ingame5; public bool inGame5
        {
            get { return ingame5; }
            set { ingame5 = value; OnPropertyChanged(); }
        }

        // stan kont druzyn
        private int saldo1; public int Saldo1
        {
            get { return saldo1; }
            set { saldo1 = value; OnPropertyChanged(); }
        }
        private int saldo2; public int Saldo2
        {
            get { return saldo2; }
            set { saldo2 = value; OnPropertyChanged(); }
        }
        private int saldo3; public int Saldo3
        {
            get { return saldo3; }
            set { saldo3 = value; OnPropertyChanged(); }
        }
        private int saldo4; public int Saldo4
        {
            get { return saldo4; }
            set { saldo4 = value; OnPropertyChanged(); }
        }
        private int saldo5; public int Saldo5
        {
            get { return saldo5; }
            set { saldo5 = value; OnPropertyChanged(); }
        }

        // oferty w licytacji
        private int bid1; public int Bid1
        {
            get { return bid1; }
            set { bid1 = value; OnPropertyChanged(); }
        }
        private int bid2; public int Bid2
        {
            get { return bid2; }
            set { bid2 = value; OnPropertyChanged(); }
        }
        private int bid3; public int Bid3
        {
            get { return bid3; }
            set { bid3 = value; OnPropertyChanged(); }
        }
        private int bid4; public int Bid4
        {
            get { return bid4; }
            set { bid4 = value; OnPropertyChanged(); }
        }
        private int bid5; public int Bid5
        {
            get { return bid5; }
            set { bid5 = value; OnPropertyChanged(); }
        }

        //pula do wygrania
        private int sumbids; public int SumBids
        {
            get { return sumbids; }
            set { sumbids = value; OnPropertyChanged(); }
        }

        //podana odpowiedz przez zespol
        private string answer; public string Answer
        {
            get { return answer; }
            set { answer = value; OnPropertyChanged(); }
        }

        // zakup podpowiedzi jeśli drużyna nie posiada darmowej
        private int hintpayment; public int HintPayment
        {
            get { return hintpayment; }
            set { hintpayment = value; OnPropertyChanged(); }
        }

        // czas gry
        // zwracany typ ? 
        private string timer; public string Timer
        {
            get { return timer; }
            set { timer = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
