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
        public ObservableCollection<RecipeClass> Favorites { get; set; } = new();
        public ObservableCollection<RecipeClass> History { get; set; } = new();

        [ObservableProperty]
        bool favoritesIsRefreshing;

        [ObservableProperty]
        bool historyIsRefreshing;

        public FavoritesAndHistoryPageModel()
        {      
            Title = "Favorites and History";
            FavoritesIsRefreshing = false;
            HistoryIsRefreshing = false;
           
        }

        public void LoadFavorites()
        {
            List<RecipeClass> faves = FavoritesHistoryDBHelper.LoadFavorites();

            Favorites.Clear();
            foreach (var item in faves)
            {
                Favorites.Add(item);
            }
        }

        public void LoadHistory()
        {
            List<RecipeClass> history = FavoritesHistoryDBHelper.LoadHistory();

            History.Clear();
            foreach (var item in history)
            {
                History.Add(item);
            }
        }
        private void LoadData()
        {
            LoadFavorites();
            LoadHistory();
        }

        [RelayCommand]
        void RefreshFavorites()
        {
            FavoritesIsRefreshing = true;

            LoadFavorites();

            FavoritesIsRefreshing = false;
        }

        [RelayCommand]
        void RefreshHistory()
        {
            HistoryIsRefreshing = true;

            LoadHistory();

            HistoryIsRefreshing = false;
        }


    }
}
