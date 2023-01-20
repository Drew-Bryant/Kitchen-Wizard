using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Views.Embedded_Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Android.Provider.ContactsContract.CommonDataKinds;

namespace Kitchen_Wizard.Models
{
    [QueryProperty(nameof(Recipe), "Recipe")]
    public partial class ViewRecipePageModel : IKitchenWizardViewModel
    {

        [ObservableProperty]
        Recipe recipe;

        [RelayCommand]
        void AddToFavorites()
        { 
            FavoritesHistoryDBHelper.AddFavorite(Recipe);
            Recipe.IsFavorite = true;

        }

        [RelayCommand]
        void RemoveFromFavorites()
        {
            FavoritesHistoryDBHelper.RemoveFavorite(Recipe);
            Recipe.IsFavorite = false;
        }

        [RelayCommand]
        void AddToHistory()
        {
            FavoritesHistoryDBHelper.AddHistory(Recipe);
            Recipe.IsHistory = true;

        }

        [RelayCommand]
        void RemoveFromHistory()
        {
            FavoritesHistoryDBHelper.RemoveHistory(Recipe);
            Recipe.IsHistory = false;
        }


    }
}
