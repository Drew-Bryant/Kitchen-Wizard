﻿using Kitchen_Wizard.Data_Objects.Interfaces;
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
        public string foodTrackingKey = "foodTracking";
        string notificationsKey = "notifications";
        string cuisineKey = "cuisineKey";
        string dietaryKey = "dietaryKey";
        string restockKey = "restockKey";
        string gtKey = "gtKey";
        string gtAllowKey = "gtAllowKey";
        string infSpicesKey = "infSpices";



        public bool FoodTracking { get; set; }
        public bool Notifications { get; set; }
        public List<string> Cuisine { get; set; } = new();
        public List<string> Dietary { get; set; } = new();
        public bool Restock { get; set; }
        public bool GroceryTrip { get; set; }
        public int GTAllowance { get; set; }
        public bool InfiniteSpices { get; set; }

        //TODO: What do I do with this class?
        //      Do I make it dependency injection and have it just be static on the page
        //      or do I make a new one and grab the prefs every time I call the search function?
        //
        //      either way, the constructor should call Get() to populate the object
        public UserPreferences()
        {
            PopulateObject();
            //TODO:
            //try to load preferences from user's local storage
            //if there, load them, otherwise just build the defaults
        }
        public void PopulateObject()
        {

            var cuisineList = new List<string>() {  };
            var dietaryList = new List<string>() { DietaryRestrictions.None.ToString() };

            FoodTracking = Preferences.Default.Get<bool>(foodTrackingKey, false);
            Notifications = Preferences.Default.Get<bool>(notificationsKey, false);

            Cuisine = Preferences.Default.Get<List<string>>(cuisineKey, null);
            if(Cuisine == null)
            {
                Cuisine = new List<string>() { CuisineType.Any.ToString() };
            }

            Dietary = Preferences.Default.Get<List<string>>(dietaryKey, null);
            if (Dietary == null)
            {
                Dietary = new List<string>() { DietaryRestrictions.None.ToString() };
            }
            Restock = Preferences.Default.Get<bool>(restockKey, false);
            GroceryTrip = Preferences.Default.Get<bool>(gtKey, false);
            GTAllowance = Preferences.Default.Get<int>(gtAllowKey, 0);
            InfiniteSpices = Preferences.Default.Get<bool>(infSpicesKey, false);
        }
        public void Update(UserPreferences prefs)
        {
            //save it in local storage
        }
    }
}
