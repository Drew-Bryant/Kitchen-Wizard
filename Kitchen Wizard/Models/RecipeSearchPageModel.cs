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
        private IRecipeHelper recipeHelper;
        private IUserPreferences userPrefs;


        public enum CuisineType
        {
            Any,
            Mexican,
            Italian,
            Asian,
            Indian,
            American
        }

        public enum DietaryRestrictions
        {
            None,
            Gluten,
            Keto,
            Vegan,
            Vegetarian
        }

        [ObservableProperty]
        public UserPreferences searchOptions;

        [ObservableProperty]
        public EnumOptions enumOptions;

        public ObservableCollection<Recipe> SearchResults { get; set; } = new();

        [ObservableProperty]
        string searchField;

        public RecipeSearchPageModel(ISearchHelper _searchHelper, IFoodListHelper _foodListHelper, IRecipeHelper _recipeHelper, IUserPreferences _userPrefs)
        {
            searchHelper = _searchHelper;
            foodListHelper = _foodListHelper;
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
