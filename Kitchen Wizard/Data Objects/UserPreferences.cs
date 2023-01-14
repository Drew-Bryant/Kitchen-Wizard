using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Gluten,
        Keto,
        None,
        Vegan,
        Vegetarian
    }
    public class UserPreferences : IUserPreferences
    {


        public bool FoodTracking { get; set; } = false;
        public bool Notifications { get; set; } = false;
        public CuisineType Cuisine { get; set; } = CuisineType.Any;
        public DietaryRestrictions Dietary { get; set; } = DietaryRestrictions.None;
        public bool Restock { get; set; } = false;
        public bool GroceryTrip { get; set; } = false;
        public int GTAllowance { get; set; } = 0;
        public bool InfiniteSpices { get; set; } = false; 

        //TODO: What do I do with this class?
        //      Do I make it dependency injection and have it just be static on the page
        //      or do I make a new one and grab the prefs every time I call the search function?
        //
        //      either way, the constructor should call Get() to populate the object
        public UserPreferences()
        {
            //TODO:
            //try to load preferences from user's local storage
            //if there, load them, otherwise just build the defaults
        }
        public UserPreferences Get()
        {
            //get user's preferences from local device storage
            //then set each property
            throw new NotImplementedException();
        }
        public void Update(UserPreferences prefs)
        {
            //save it in local storage
        }
    }
}
