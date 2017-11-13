using AwanturaLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<int> HintCount { get; set; } = new ObservableCollection<int>()
        {
            1,2,3,4,5
        };

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

        public ObservableCollection<string> TeamNames { get; set; } = new ObservableCollection<string>()
        {
            "","","","",""
        };

        public ObservableCollection<bool> ArePlaying { get; set; } = new ObservableCollection<bool>()
        {
            true, true, true, true, true
        };

        public ObservableCollection<int> Points { get; set; } = new ObservableCollection<int>()
            {
            0,0,0,0,0
        };

        private string state;

        public string State
        {
            get { return state; }
            set { state = value; OnPropertyChanged(); }
        }



        public ObservableCollection<int> Bids { get; set; } = new ObservableCollection<int>()
            {
            0,0,0,0,0
        };


        //pula do wygrania

        private int pool;

        public int Pool
        {
            get { return pool; }
            set { pool = value; OnPropertyChanged(); }
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
        private int timer; public int Timer
        {
            get { return timer; }
            set { timer = value; OnPropertyChanged(); }
        }

        public bool TimerEnabled { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
