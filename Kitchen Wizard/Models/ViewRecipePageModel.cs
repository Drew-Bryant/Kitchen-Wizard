using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
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

        private IFavoritesHelper favoritesHelper;
        private IHistoryHelper historyHelper;

        [ObservableProperty]
        Recipe recipe;

        [RelayCommand]
        async void AddToFavorites()
        { 
            FavoritesHistoryDBHelper.AddFavorite(Recipe);
        }

        [RelayCommand]
        async void AddToHistory()
        {
            FavoritesHistoryDBHelper.AddHistory(Recipe);
        }

    }
}
