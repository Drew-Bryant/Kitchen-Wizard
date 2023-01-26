using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Views.Embedded_Views;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Android.Provider.ContactsContract.CommonDataKinds;

namespace Kitchen_Wizard.Models
{
    [QueryProperty("Recipe", "Recipe")]
    public partial class ViewRecipePageModel : IKitchenWizardViewModel
    {

        public static string NotFavoriteString { get; set; } = "Add\nFavorite";
        public static string FavoriteString { get; set; } = "Remove\nFavorite";

        public static string NotHistoryString { get; set; } = "Make This\nRecipe";
        public static string ShareString { get; set; } = "Share\nRecipe";


        [ObservableProperty]
        RecipeClass recipe;

        public ObservableCollection<string> Steps { get; set; } = new();

        //[ObservableProperty]
        //string shareButtonText = ShareString;
        [ObservableProperty]
        string shareButtonText = ShareString;
        [ObservableProperty]
        string favoriteButtonText = NotFavoriteString;

        [ObservableProperty]
        string historyButtonText = NotHistoryString;

        public void InitProperties()
        {
            FavoriteButtonText = NotFavoriteString;
            HistoryButtonText = NotHistoryString;

            if(Recipe.IsFavorite)
            {
                FavoriteButtonText = FavoriteString;
            }

            if(Recipe.IsHistory)
            {
                Recipe.HistoryDate = FavoritesHistoryDBHelper.GetHistoryDate(Recipe);
                HistoryButtonText = $"Made on\n{Recipe.HistoryDate.ToString("MM/dd/yyyy")}";
            }

            Steps.Clear();

            foreach(var step in Recipe.Steps)
            {
                Steps.Add(step);
            }


        }
        [RelayCommand]
        void AddToFavorites()
        {
            FavoritesHistoryDBHelper.AddFavorite(Recipe);
            Recipe.IsFavorite = true;
            FavoriteButtonText = FavoriteString;
        }

        [RelayCommand]
        void RemoveFromFavorites()
        {
            FavoritesHistoryDBHelper.RemoveFavorite(Recipe);
            Recipe.IsFavorite = false;
            FavoriteButtonText = NotFavoriteString;
        }

        [RelayCommand]
        void ToggleFavorite()
        {
            if (Recipe.IsFavorite)
            {
                FavoritesHistoryDBHelper.RemoveFavorite(Recipe);
                Recipe.IsFavorite = false;
                FavoriteButtonText = NotFavoriteString;
            }
            else
            {
                FavoritesHistoryDBHelper.AddFavorite(Recipe);
                Recipe.IsFavorite = true;
                FavoriteButtonText = FavoriteString;
            }
        }

        [RelayCommand]
        void ToggleHistory()
        {
            if(Recipe.IsHistory)
            {
                FavoritesHistoryDBHelper.RemoveHistory(Recipe);
                Recipe.IsHistory = false;
                HistoryButtonText = NotHistoryString;
            }
            else
            {
                Recipe.HistoryDate = DateTime.Now;
                FavoritesHistoryDBHelper.AddHistory(Recipe);
                Recipe.IsHistory = true;
                HistoryButtonText = $"Made on\n {Recipe.HistoryDate.ToString("MM/dd/yyyy")}";
            }
        }

        [RelayCommand]
        void AddToHistory()
        {
            Recipe.HistoryDate = FavoritesHistoryDBHelper.AddHistory(Recipe);
            Recipe.IsHistory = true;

        }

        [RelayCommand]
        void RemoveFromHistory()
        {
            FavoritesHistoryDBHelper.RemoveHistory(Recipe);
            Recipe.IsHistory = false;
        }

        [RelayCommand]
        async void ShareRecipe(RecipeClass recipe)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Uri = recipe.Source,
                Title = "Choose where to share this recipe"
            });
        }

    }
}
