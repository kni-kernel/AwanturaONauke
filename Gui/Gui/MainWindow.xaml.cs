using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public ViewModel VM { get; set; } = new ViewModel();
        public object Keys { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            VM.Question = "U brzegu jakiego kontynentu leży największy rów?";
            VM.UrlImage = "";
            VM.Hint1 = "do wyrobu instrumentów";
            VM.Hint2 = "do konserwacji fortepianu";
            VM.Hint3 = "do nacierania włosia smyczków";
            VM.Hint4 = "do przechowywania instrumentów";

            VM.Team1 = "Niebiescy";
            VM.Team2 = "Czerwoni";
            VM.Team3 = "Zieloni";
            VM.Team4 = "Mistrzowie";

            VM.Saldo1 = 5000;
            VM.Saldo2 = 5000;
            VM.Saldo3 = 5000;
            VM.Saldo4 = 5000;

            VM.Bid1 = 0;
            VM.Bid2 = 0;
            VM.Bid3 = 0;
            VM.Bid4 = 0;

            VM.SumBids = 0;
            VM.Answer = "";
            VM.HintPayment = 0;
            VM.Timer = 0;

            MainGrid.DataContext = VM;

            /// do stworzenia menu z kategoriami na starcie
            VM.Categories.Add(new CategoryViewModel()
            {
                ID = "COS",
                Name = "Kategoria 1"
            });

            VM.Categories.Add(new CategoryViewModel()
            {
                ID = "COasdasdsaS",
                Name = "Kategoria 2"
            });

            /// dodanie kategorii z listy z Service
            foreach(var category in VM.Categories)
            {
                var item = new MenuItem();
                item.Header = category.Name;
                item.Name = category.ID;

                CategoriesMenu.Items.Add(item);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// na key click reaguje tylko TA funkcja
        /// dane zmieniaja sie tylko po enter click 
        /*
        private void TextBox_KeyDown(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                
            }
        }
        */

        /// button do wyświetlania wszystkich kategorii jako Menu
        /// user wybiera z menu kategorie i przesyła do Server
        private void categoriesMenu_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void category_Click(object sender, RoutedEventArgs e)
        {
            /// sprawdzić czy forma gry 1:1
            /// jesli tak to wyślij nazwe kategorii i disable wybrany button
            /// jeśli nie to tylko wyślij nazwę kategorii
        }

        // button do wyswietlania obrazka podpiętego pod pytanie
        private void imgButton_Click(object sender, RoutedEventArgs e)
        {
            /// zastosować ścieżkę do wyświetlenia gdzies obok zdj
            /// jesli istnieje
            /// obrazek wywoływany dopiero po kliknięciu
            /// VM.UrlImage
        }

        // kup podpowiedź
        private void buyHintButton_Click(object sender, RoutedEventArgs e)
        {
            /// nie wiem czy potrzebne
            /// czy nie uzyć odpowiedzi jako skrótu że H/P to podpowiedz 
            /// i zatwierdzić ENTER
        }

        // wykorzystaj podpowiedź
        private void useHintButton_Click(object sender, RoutedEventArgs e)
        {
            /// button WEŹ PODPOWIEDŹ
            /// z tym że musi sprawdzić czy drużyna może użyć za darmo podpowiedzi
            /// albo poprosić o kwote do zapłaty
        }

        // kup czarne pudełko
        private void buyBlackBoxButton_Click(object sender, RoutedEventArgs e)
        {
            /// zakup czarnego pudełka 
            /// użyć skrótu np. BB/B/CP czy button?
        }
        private void startTimeButton_Click(object sender, RoutedEventArgs e)
        {
            /// licznik czasu odpowiedzi : 60s
            
        }

        private void stopTimeButton_Click(object sender, RoutedEventArgs e)
        {
            /// zatrzymaj czas albo czas skończy się po 60s
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            /// tutaj nie wiem - powrót do IDLE
        }
    }
}
