using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
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
    public class UserPreferences : IUserPreferences
    {

        //key values for local storage
        string foodTrackingKey = "foodTracking";
        string notificationsKey = "notifications";
        string cuisineKey = "cuisineKey";
        string dietaryKey = "dietaryKey";
        string restockKey = "restockKey";
        string gtKey = "gtKey";
        string gtAllowKey = "gtAllowKey";
        string infSpicesKey = "infSpices";

        string cuisineDefault = "Any";
        string dietaryDefault = "None";


        public bool FoodTracking { get; set; }
        public bool Notifications { get; set; }
        public List<string> Cuisine { get; set; } = new();
        public List<string> Dietary { get; set; } = new();
        public bool Restock { get; set; }
        public bool GroceryTrip { get; set; }
        public int GTAllowance { get; set; }
        public bool InfiniteSpices { get; set; }

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

            Restock = Preferences.Default.Get<bool>(restockKey, false);
            GroceryTrip = Preferences.Default.Get<bool>(gtKey, false);
            GTAllowance = Preferences.Default.Get<int>(gtAllowKey, 0);
            InfiniteSpices = Preferences.Default.Get<bool>(infSpicesKey, false);
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

            FoodTracking = Preferences.Default.Get<bool>(foodTrackingKey, false);
            Notifications = Preferences.Default.Get<bool>(notificationsKey, false);

            Cuisine = StringToList(Preferences.Default.Get<string>(cuisineKey, cuisineDefault));

            Dietary = StringToList(Preferences.Default.Get<string>(dietaryKey, dietaryDefault));

            Restock = Preferences.Default.Get<bool>(restockKey, false);
            GroceryTrip = Preferences.Default.Get<bool>(gtKey, false);
            GTAllowance = Preferences.Default.Get<int>(gtAllowKey, 0);
            InfiniteSpices = Preferences.Default.Get<bool>(infSpicesKey, false);

        }
    }
}
