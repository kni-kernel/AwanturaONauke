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
            VM.Name = "Kunegunda";
            VM.Pepper = "5000";
            MainGrid.DataContext = VM;

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

            // dodanie kategorii z listy z Service
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Baltazar";
            VM.Pepper = "Mana Mana";
        }

        // na key click reaguje tylko TA funkcja
        // dane zmieniaja sie tylko po enter click
        private void TextBox_KeyDown(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                VM.Name = "Bach";
                VM.Pepper = "Mozart";
            }
        }

        // button do wyświetlania wszystkich kategorii jako Menu
        // user wybiera z menu kategorie i przesyła do Server
        private void categoriesMenu_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void category_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Menu";
            VM.Pepper = "Pepper";
        }

        // button do wyswietlania obrazka podpiętego pod pytanie
        private void imgButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Korelacja";
            VM.Pepper = "Yoda";
        }

        private void buyHintButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Korelacja";
            VM.Pepper = "Yoda";
        }

        private void useHintButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Korelacja";
            VM.Pepper = "kola";
        }

        private void buyBlackBoxButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Tymek";
            VM.Pepper = "Ulek";
        }
        private void startTimeButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Korelacja";
            VM.Pepper = "kola";
        }

        private void stopTimeButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Tymek";
            VM.Pepper = "Ulek";
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            VM.Name = "Tymek";
            VM.Pepper = "Ulek";
        }
    }
}
