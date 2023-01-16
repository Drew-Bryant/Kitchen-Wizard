using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Database_Helpers
{
    public class StaticSearchHelper : ISearchHelper
    {
        public async Task<List<Recipe>> SearchRecipeByKeyword(string keyword, UserPreferences prefs)
        {

            Random random = new Random(Guid.NewGuid().GetHashCode());
            
            List<Recipe> recipes = new();
            for(int ii = 0; ii < 10; ii++)
            {
                Recipe recipe = new Recipe();
                int next = random.Next(25);
                //recipe.Name = "Recipe number" + (next);
                //recipe.ID = next;
                recipe.Name = "Recipe number" + (ii + 1);
                recipe.ID = ii + 1;

                recipes.Add(recipe);
            }

            return recipes;
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
