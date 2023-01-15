using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class RecipeSearchPageModel : IKitchenWizardViewModel
    {
        private ISearchHelper searchHelper;
        private IFoodListHelper foodListHelper;
        private IHistoryHelper historyHelper;
        private IFavoritesHelper favoritesHelper;
        private IRecipeHelper recipeHelper;
        private IUserPreferences userPrefs;

        public UserPreferences SearchOptions { get; set; }

        public ObservableCollection<Recipe> SearchResults { get; set; } = new();

        [ObservableProperty]
        string searchField;

        public RecipeSearchPageModel(ISearchHelper _searchHelper, IFoodListHelper _foodListHelper, IHistoryHelper _historyHelper,
                                IFavoritesHelper _favoritesHelper, IRecipeHelper _recipeHelper, IUserPreferences _userPrefs)
        {
            searchHelper = _searchHelper;
            foodListHelper = _foodListHelper;
            historyHelper = _historyHelper;
            favoritesHelper = _favoritesHelper;
            recipeHelper = _recipeHelper;
            userPrefs = _userPrefs;

            Title = "Search";

        }

        [RelayCommand]
        async Task KeywordSearch(string keyword)
        {
            SearchResults.Clear();
            //do the search here

            var results = await searchHelper.SearchRecipeByKeyword(keyword, (UserPreferences)userPrefs);

            foreach(var recipe in results)
            {
                SearchResults.Add(recipe);
            }
        }

    }
}
