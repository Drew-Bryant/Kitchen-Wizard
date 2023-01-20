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
        public List<Recipe>SearchRecipeByKeyword(string keyword, UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
        public List<Recipe> SearchRecipeByFoodList(UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
        public List<Food> SearchFoodDB(string keyword)
        {
            throw new NotImplementedException();
        }
        private List<Recipe> ValidateFoodList(List<Recipe> recipeList, List<Food> foodList, UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
    }
}
