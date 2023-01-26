using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Database_Helpers
{
    public class SearchDBHelper : ISearchHelper
    {
        public List<RecipeClass>SearchRecipeByKeyword(string keyword, UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
        public List<RecipeClass> SearchRecipeByFoodList(UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
        public List<FoodListItem> SearchFoodDB(string keyword)
        {
            //this is where to initialize the foodlistitem and it defaults to infinite being OFF
            throw new NotImplementedException();
        }
        private List<RecipeClass> ValidateFoodList(List<RecipeClass> recipeList, List<Food> foodList, UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
    }
}
