using CommunityToolkit.Mvvm.ComponentModel;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kitchen_Wizard.Data_Objects.EnumOptions;

namespace Kitchen_Wizard.Data_Objects
{
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
        Gluten_Free,
        Keto,
        None,
        Vegan,
        Vegetarian
    }
    public partial class UserPreferences : ObservableObject, IUserPreferences
    {

        //key values for local storage
        string foodTrackingKey = "foodTrackingKey";
        string notificationsKey = "notificationsKey";
        string cuisineKey = "cuisineKey";
        string dietaryKey = "dietaryKey";
        string restockKey = "restockKey";
        string gtKey = "gtKey";
        string gtAllowKey = "gtAllowKey";
        string infSpicesKey = "infSpicesKey";
        string trackExpiryKey = "expiryKey";
        string weightUnitKey = "weightUnitKey";
        string lowWeightThresholdKey = "weightThresholdKey";
        string volumeUnitKey = "volumeUnitKey";
        string lowVolumeThresholdKey = "volumeThresholdKey";
        string subtractFoodsKey = "subtractFoodsKey";

        //default strings so they are bound to an object instead of splattered all over
        string cuisineDefault = "Any";
        string dietaryDefault = "None";


        public bool FoodTracking { get; set; }

        public List<string> Cuisine { get; set; } = new();
        public List<string> Dietary { get; set; } = new();

        [ObservableProperty]
        bool restock;

        [ObservableProperty]
        bool subtractFoods;

        [ObservableProperty]
        bool groceryTrip;

        [ObservableProperty]
        int gTAllowance;

        partial void OnGTAllowanceChanged(int value)
        {
            Preferences.Default.Set<int>(gtAllowKey, value);
        }

        public ObservableCollection<Unit> VolumeUnits { get; set; } = new()
        { Unit.tsp, Unit.tbsp, Unit.cups, Unit.fl_oz, Unit.pints, Unit.quarts, Unit.gallons, Unit.liters, Unit.ml};

        public ObservableCollection<Unit> MassUnits { get; set; } = new()
        {Unit.lbs, Unit.mg, Unit.grams, Unit.kgs, Unit.oz};


        [ObservableProperty]
        bool infiniteSpices;

        [ObservableProperty]
        Unit lowWeightUnit;

        [ObservableProperty]
        int lowWeightIndex;

        [ObservableProperty]
        double lowWeightNotificationThreshold;

        partial void OnLowWeightNotificationThresholdChanged(double value)
        {
            Preferences.Default.Set<double>(lowWeightThresholdKey, value);
            FoodListDBHelper.ClearInvalidatedNotifications();
            FoodListDBHelper.CheckLowFoods();
        }

        [ObservableProperty]
        Unit lowVolumeUnit;

        [ObservableProperty]
        int lowVolumeIndex;

        [ObservableProperty]
        double lowVolumeNotificationThreshold;

        partial void OnLowVolumeNotificationThresholdChanged(double value)
        {
            Preferences.Default.Set<double>(lowVolumeThresholdKey, value);
            FoodListDBHelper.ClearInvalidatedNotifications();
            FoodListDBHelper.CheckLowFoods();

        }

        [ObservableProperty]
        bool notifications;

        [ObservableProperty]
        bool trackingExpirationDates;
        public UserPreferences()
        {
            LoadPrefs();
        }

        public void LoadPrefs()
        {
            var cuisineList = new List<string>() {  };
            var dietaryList = new List<string>() { DietaryRestrictions.None.ToString() };

            FoodTracking = Preferences.Default.Get<bool>(foodTrackingKey, false);
            Notifications = Preferences.Default.Get<bool>(notificationsKey, false);


            Cuisine = StringToList(Preferences.Default.Get<string>(cuisineKey, cuisineDefault));

            Dietary = StringToList(Preferences.Default.Get<string>(dietaryKey, dietaryDefault));
            SubtractFoods = Preferences.Default.Get<bool>(subtractFoodsKey, false);

            Restock = Preferences.Default.Get<bool>(restockKey, false);
            GroceryTrip = Preferences.Default.Get<bool>(gtKey, false);
            GTAllowance = Preferences.Default.Get<int>(gtAllowKey, 0);
            InfiniteSpices = Preferences.Default.Get<bool>(infSpicesKey, false);
            TrackingExpirationDates = Preferences.Default.Get<bool>(trackExpiryKey, true);

            LowWeightUnit = ConversionService.StringToUnitEnum(Preferences.Default.Get<string>(weightUnitKey, "oz"));
            lowWeightNotificationThreshold = Preferences.Default.Get<double>(lowWeightThresholdKey, 0);

            LowVolumeUnit = ConversionService.StringToUnitEnum(Preferences.Default.Get<string>(volumeUnitKey, "cups"));
            lowVolumeNotificationThreshold = Preferences.Default.Get<double>(lowVolumeThresholdKey, 0);
        }

        //convert a comma-separated string into separate values
        private List<string> StringToList(string listString)
        {
            return listString.Split(',').ToList();
        }

        //convert a list of strings into a single comma-separated string
        private string ListToString(List<string> list)
        {
            string result = "";
            for(int ii = 0; ii < list.Count; ii++)
            {
                //don't put a comma after the last element
                if ((ii + 1) != list.Count)
                {
                    result += list[ii] + ',';
                }
                else
                {
                    result += list[ii];
                }
            }

            return result;
        }
        public void Save(UserPreferences prefs)
        {
            Preferences.Default.Set<bool>(foodTrackingKey, prefs.FoodTracking);
            Preferences.Default.Set<bool>(notificationsKey, prefs.Notifications);
            Preferences.Default.Set<string>(cuisineKey, ListToString(prefs.Cuisine));
            Preferences.Default.Set<string>(dietaryKey, ListToString(prefs.Dietary));
            Preferences.Default.Set<bool>(restockKey, prefs.Restock);
            Preferences.Default.Set<bool>(gtKey, prefs.GroceryTrip);
            Preferences.Default.Set<int>(gtAllowKey, prefs.GTAllowance);
            Preferences.Default.Set<bool>(infSpicesKey, prefs.InfiniteSpices);
            Preferences.Default.Set<bool>(trackExpiryKey, prefs.TrackingExpirationDates);

            Preferences.Default.Set<string>(weightUnitKey, prefs.LowWeightUnit.ToString());
            Preferences.Default.Set<double>(lowWeightThresholdKey, prefs.LowWeightNotificationThreshold);

            Preferences.Default.Set<string>(volumeUnitKey, prefs.LowVolumeUnit.ToString());
            Preferences.Default.Set<double>(lowVolumeThresholdKey, prefs.LowVolumeNotificationThreshold);
            Preferences.Default.Set<bool>(subtractFoodsKey, prefs.SubtractFoods);
            //Why am I getting all the preferences in the save?
            //Is it so they get updated properties?
            //this seems like a weird decision that is unnecessary.
            //Maybe delete this later and see what happens as a result
            //FoodTracking = Preferences.Default.Get<bool>(foodTrackingKey, false);
            //Notifications = Preferences.Default.Get<bool>(notificationsKey, false);

            //Cuisine = StringToList(Preferences.Default.Get<string>(cuisineKey, cuisineDefault));

            //Dietary = StringToList(Preferences.Default.Get<string>(dietaryKey, dietaryDefault));

            //Restock = Preferences.Default.Get<bool>(restockKey, false);
            //GroceryTrip = Preferences.Default.Get<bool>(gtKey, false);
            //GTAllowance = Preferences.Default.Get<int>(gtAllowKey, 0);
            //InfiniteSpices = Preferences.Default.Get<bool>(infSpicesKey, false);
            //TrackingExpirationDates = Preferences.Default.Get<bool>(trackExpiryKey, true);

        }

        public void Save()
        {
            Save(this);
        }
    }
}
