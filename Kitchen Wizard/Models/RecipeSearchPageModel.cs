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
    public partial class RecipeSearchPageModel : ObservableObject
    {
        private ISearchHelper searchHelper;
        private IFoodListHelper foodListHelper;
        private IHistoryHelper historyHelper;
        private IFavoritesHelper favoritesHelper;
        private IRecipeHelper recipeHelper;
        private IUserPreferences userPrefs;

        public UserPreferences SearchOptions { get; set; }
        public ObservableCollection<Recipe> searchResults { get; set; } = new ObservableCollection<Recipe>();

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
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            //TODO:
            //ObservableCollection<Recipe> results = (IEnumerable<Recipe>)searchHelper.SearchRecipeByKeyword(SearchField.Text, (UserPreferences)userPrefs);
            //SearchResults = results;
            //with the results, build a collection view and add all of the recipes to it

            //CollectionView collectionView = new CollectionView
            //{
            //    IsGrouped = true
            //};
            //collectionView.SetBinding(ItemsView.ItemsSourceProperty, "Animals");

            //start with just a static search class that always returns the same thing

            //then work on displaying the recipe view. 
            //bare bones at first, just show the recipe name, no ingredients slideout or anything
            //just add the favorites functionality and add it and get it to show up in local storage
            //then go to favorites page and display the list.
        }

        [RelayCommand]
        void KeywordSearch(string keyword)
        {
            //do the search here
            SearchResults = searchHelper.SearchRecipeByKeyword(keyword, (UserPreferences)userPrefs);
        }

    }
}
