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
        
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string pepper;
        public string Pepper
        {
            get { return pepper; }
            set { pepper = value; OnPropertyChanged(); }
        }

        // kategoria pytań z Service
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        // pytania pobierane z Service
        private string question;
        public string Question
        {
            get { return question; }
            set { question = value; }
        }

        // podpowiedzi pobrane z pytaniem z Service
        private string hint1;
        public string Hint1
        {
            get { return hint1; }
            set { hint1 = value; }
        }

        private string hint2;
        public string Hint2
        {
            get { return hint2; }
            set { hint2 = value; }
        }

        private string hint3;
        public string Hint3
        {
            get { return hint3; }
            set { hint3 = value; }
        }

        private string hint4;
        public string Hint4
        {
            get { return hint4; }
            set { hint4 = value; }
        }


        //obrazek do pobrania ???
        private string urlImage;
        public string UrlImage
        {
            get { return urlImage; }
            set { urlImage = value; }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
