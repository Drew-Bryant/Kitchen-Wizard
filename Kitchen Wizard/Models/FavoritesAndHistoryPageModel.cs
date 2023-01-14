using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class FavoritesAndHistoryPageModel : ObservableObject
    {
        public string Filename { get; set; }
        public string Text { get; set; } = "Welcome to Favorites";
        public DateTime Date { get; set; }

        void OnPropertyChanged()
        {

        }
}
}
