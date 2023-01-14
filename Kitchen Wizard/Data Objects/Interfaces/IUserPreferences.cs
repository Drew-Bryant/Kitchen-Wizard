using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface IUserPreferences
    {

        public bool FoodTracking { get; set; }
        public bool Notifications { get; set; }
        public CuisineType Cuisine { get; set; }
        public DietaryRestrictions Dietary { get; set; }
        public bool Restock { get; set; }
        public bool GroceryTrip { get; set; }
        public int GTAllowance { get; set; }
        public bool InfiniteSpices { get; set; }

        //TODO: What do I do with this class?
        //      Do I make it dependency injection and have it just be static on the page
        //      or do I make a new one and grab the prefs every time I call the search function?
        //
        //      either way, the constructor should call Get() to populate the object
        public UserPreferences Get();
        public void Update(UserPreferences prefs);
    }
}
