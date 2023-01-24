using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class FavoritesAndHistoryPageModel : IKitchenWizardViewModel
    {
        public FavoritesAndHistoryPageModel()
        {      
            Title = "Favorites and History";
            LoadData();
            FavoritesIsRefreshing = false;
            HistoryIsRefreshing = false;
           
        }

        private void LoadData()
        {
            List<Recipe> faves = FavoritesHistoryDBHelper.LoadFavorites();

            Favorites.Clear();
            foreach (var item in faves)
            {
                Favorites.Add(item);
            }

            List<Recipe> history = FavoritesHistoryDBHelper.LoadHistory();

            History.Clear();
            foreach (var item in history)
            {
                History.Add(item);
            }
        }

        [RelayCommand]
        void RefreshFavorites()
        {
            FavoritesIsRefreshing = true;

            LoadData();

            FavoritesIsRefreshing = false;
        }

        [RelayCommand]
        void RefreshHistory()
        {
            HistoryIsRefreshing = true;

            LoadData();

            HistoryIsRefreshing = false;
        }

        public ObservableCollection<Recipe> Favorites { get; set; } = new();
        public ObservableCollection<Recipe> History { get; set; } = new();

        [ObservableProperty]
        bool favoritesIsRefreshing;

        [ObservableProperty]
        bool historyIsRefreshing;

    }
}
