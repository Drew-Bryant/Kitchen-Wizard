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

        //for managing options checkboxes
        [ObservableProperty]
        bool anyChecked = false;
        [ObservableProperty]
        bool mexicanChecked = false;
        [ObservableProperty]
        bool italianChecked = false;
        [ObservableProperty]
        bool asianChecked = false;
        [ObservableProperty]
        bool indianChecked = false;
        [ObservableProperty]
        bool americanChecked = false;
        [ObservableProperty]
        bool noneChecked = false;
        [ObservableProperty]
        bool glutenChecked = false;
        [ObservableProperty]
        bool ketoChecked = false;
        [ObservableProperty]
        bool veganChecked = false;
        [ObservableProperty]
        bool vegetarianChecked = false;

        
        public List<string> CuisineStrings { get; set; } = new List<string>() 
        { CuisineType.Any.ToString(), CuisineType.Mexican.ToString(), CuisineType.Italian.ToString(), CuisineType.Asian.ToString(), CuisineType.Indian.ToString(), CuisineType.American.ToString() };
        public List<string> DietaryStrings { get; set; } = new List<string>()
        { DietaryRestrictions.None.ToString(), DietaryRestrictions.Gluten_Free.ToString(), DietaryRestrictions.Keto.ToString(), DietaryRestrictions.Vegan.ToString(), DietaryRestrictions.Vegetarian.ToString()};

        [ObservableProperty]
        public UserPreferences searchOptions = new();

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

            foreach(var option in SearchOptions.Cuisine)
            {
                CuisineType value;
                Enum.TryParse<CuisineType>(option, out value);
                switch (value)
                {
                    case CuisineType.Any:
                        AnyChecked = true;
                        break;
                    case CuisineType.Mexican:
                        MexicanChecked = true;
                        break;
                    case CuisineType.Italian:
                        ItalianChecked = true;
                        break;
                    case CuisineType.Asian:
                        AsianChecked = true;
                        break;
                    case CuisineType.Indian:
                        IndianChecked = true;
                        break;
                    case CuisineType.American:
                        AmericanChecked = true;
                        break;
                }

            }

            foreach (var option in SearchOptions.Dietary)
            {
                DietaryRestrictions value;
                Enum.TryParse<DietaryRestrictions>(option, out value);
                switch (value)
                {
                    case DietaryRestrictions.None:
                        NoneChecked = true;
                        break;
                    case DietaryRestrictions.Gluten_Free:
                        GlutenChecked = true;
                        break;
                    case DietaryRestrictions.Keto:
                        KetoChecked = true;
                        break;
                    case DietaryRestrictions.Vegan:
                        VeganChecked = true;
                        break;
                    case DietaryRestrictions.Vegetarian:
                        VegetarianChecked = true;
                        break;
                }

            }
            //TODO: Initialize search options to app defaults or user preferences if set

        }

        [RelayCommand]
        void KeywordSearch(string keyword)
        {
            SearchResults.Clear();
            //do the search here

            var results = searchHelper.SearchRecipeByKeyword(keyword, SearchOptions);

            foreach(var recipe in results)
            {
                SearchResults.Add(recipe);
            }
        }

        [RelayCommand]
        public void ToggleCuisine(string name)
        {
            //indicates whether the control was turned on or off
            bool selected = false;
            if (SearchOptions.Cuisine.Contains(name))
            {
                SearchOptions.Cuisine.Remove(name);
            }
            else
            {
                SearchOptions.Cuisine.Add(name);
                selected = true;
            }

            CuisineType value;
            Enum.TryParse<CuisineType>(name, out value);

            switch (value)
            {
                case CuisineType.Any:
                    AnyChecked = selected;
                    //Selected may be false which means the control was turned off
                    //if selected is true, that is the only time to turn off other controls
                    if (selected)
                    {
                        MexicanChecked = false;
                        AsianChecked = false;
                        ItalianChecked = false;
                        IndianChecked = false;
                        AmericanChecked = false;
                        SearchOptions.Cuisine.Remove(CuisineType.Mexican.ToString());
                        SearchOptions.Cuisine.Remove(CuisineType.Asian.ToString());
                        SearchOptions.Cuisine.Remove(CuisineType.Italian.ToString());
                        SearchOptions.Cuisine.Remove(CuisineType.Indian.ToString());
                        SearchOptions.Cuisine.Remove(CuisineType.American.ToString());
                    }
                    break;
                case CuisineType.Mexican:
                    MexicanChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        bool result = SearchOptions.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.Italian:
                    ItalianChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        SearchOptions.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.Asian:
                    AsianChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        SearchOptions.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.Indian:
                    IndianChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        SearchOptions.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.American:
                    AmericanChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        SearchOptions.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
            }

        }

        [RelayCommand]
        public void ToggleDietary(string name)
        {
            //indicates whether the control was turned on or off
            bool selected = false;
            if (SearchOptions.Dietary.Contains(name))
            {
                SearchOptions.Dietary.Remove(name);
            }
            else
            {
                SearchOptions.Dietary.Add(name);
                selected = true;
            }

            DietaryRestrictions value;
            Enum.TryParse<DietaryRestrictions>(name, out value);
            switch (value)
            {
                case DietaryRestrictions.None:
                    NoneChecked = selected;

                    //Remove other options if None is checked
                    if (selected)
                    {
                        GlutenChecked = false;
                        KetoChecked = false;
                        VeganChecked = false;
                        VegetarianChecked = false;
                        SearchOptions.Dietary.Remove(DietaryRestrictions.Gluten_Free.ToString());
                        SearchOptions.Dietary.Remove(DietaryRestrictions.Keto.ToString());
                        SearchOptions.Dietary.Remove(DietaryRestrictions.Vegan.ToString());
                        SearchOptions.Dietary.Remove(DietaryRestrictions.Vegetarian.ToString());
                    }
                    break;
                case DietaryRestrictions.Gluten_Free:
                    GlutenChecked = selected;

                    if (selected)
                    {
                        NoneChecked = false;
                        SearchOptions.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
                case DietaryRestrictions.Keto:
                    KetoChecked = selected;
                    if (selected)
                    {
                        NoneChecked = false;
                        SearchOptions.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
                case DietaryRestrictions.Vegan:
                    VeganChecked = selected;

                    if (selected)
                    {
                        NoneChecked = false;
                        SearchOptions.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
                case DietaryRestrictions.Vegetarian:
                    VegetarianChecked = selected;
                    if (selected)
                    {
                        NoneChecked = false;
                        SearchOptions.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
            }
        }

    }
}
