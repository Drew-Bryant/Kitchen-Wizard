using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class SettingsPageModel : IKitchenWizardViewModel
    {
        [ObservableProperty]
        UserPreferences userPrefs = new();

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

        IUserPreferences prefsHelper;
        public SettingsPageModel(IUserPreferences _prefs)
        {
            prefsHelper = _prefs;
            UserPrefs.LoadPrefs();

            Title = "App Preferences";

            SetDefaultOptions();
        }


        public void SetDefaultOptions()
        {
            AnyChecked = false;
            MexicanChecked = false;
            ItalianChecked = false;
            AsianChecked = false;
            IndianChecked = false;
            AmericanChecked = false;
            NoneChecked = false;
            GlutenChecked = false;
            KetoChecked = false;
            VeganChecked = false;
            VegetarianChecked = false;

            foreach (var option in UserPrefs.Cuisine)
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

            foreach (var option in UserPrefs.Dietary)
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
        }
        [RelayCommand]
        public void ToggleCuisine(string name)
        {
            //indicates whether the control was turned on or off
            bool selected = false;
            if (UserPrefs.Cuisine.Contains(name))
            {
                UserPrefs.Cuisine.Remove(name);
            }
            else
            {
                UserPrefs.Cuisine.Add(name);
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
                        UserPrefs.Cuisine.Remove(CuisineType.Mexican.ToString());
                        UserPrefs.Cuisine.Remove(CuisineType.Asian.ToString());
                        UserPrefs.Cuisine.Remove(CuisineType.Italian.ToString());
                        UserPrefs.Cuisine.Remove(CuisineType.Indian.ToString());
                        UserPrefs.Cuisine.Remove(CuisineType.American.ToString());
                    }
                    break;
                case CuisineType.Mexican:
                    MexicanChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        bool result = UserPrefs.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.Italian:
                    ItalianChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        UserPrefs.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.Asian:
                    AsianChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        UserPrefs.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.Indian:
                    IndianChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        UserPrefs.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
                case CuisineType.American:
                    AmericanChecked = selected;

                    if (selected)
                    {
                        AnyChecked = false;
                        UserPrefs.Cuisine.Remove(CuisineType.Any.ToString());
                    }
                    break;
            }

            prefsHelper.Save(UserPrefs);
        }

        [RelayCommand]
        public void ToggleDietary(string name)
        {
            //indicates whether the control was turned on or off
            bool selected = false;
            if (UserPrefs.Dietary.Contains(name))
            {
                UserPrefs.Dietary.Remove(name);
            }
            else
            {
                UserPrefs.Dietary.Add(name);
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
                        UserPrefs.Dietary.Remove(DietaryRestrictions.Gluten_Free.ToString());
                        UserPrefs.Dietary.Remove(DietaryRestrictions.Keto.ToString());
                        UserPrefs.Dietary.Remove(DietaryRestrictions.Vegan.ToString());
                        UserPrefs.Dietary.Remove(DietaryRestrictions.Vegetarian.ToString());
                    }
                    break;
                case DietaryRestrictions.Gluten_Free:
                    GlutenChecked = selected;

                    if (selected)
                    {
                        NoneChecked = false;
                        UserPrefs.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
                case DietaryRestrictions.Keto:
                    KetoChecked = selected;
                    if (selected)
                    {
                        NoneChecked = false;
                        UserPrefs.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
                case DietaryRestrictions.Vegan:
                    VeganChecked = selected;

                    if (selected)
                    {
                        NoneChecked = false;
                        UserPrefs.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
                case DietaryRestrictions.Vegetarian:
                    VegetarianChecked = selected;
                    if (selected)
                    {
                        NoneChecked = false;
                        UserPrefs.Dietary.Remove(DietaryRestrictions.None.ToString());
                    }
                    break;
            }

            prefsHelper.Save(UserPrefs);
        }
    }
}
