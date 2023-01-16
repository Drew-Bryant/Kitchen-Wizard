using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Static_Helpers
{
    public class StaticRecipeHelper : IRecipeHelper
    {
        public Recipe GetFullByID(int recipeID)
        {
            Recipe recipe = new();
            recipe.Name = "Yummy Tasty Recipe";
            recipe.ID = recipeID;
            recipe.Description = "This is the description for the recipe";

            return recipe;
        }

        public Recipe GetFullByName(string recipeName)
        {
            throw new NotImplementedException();
        }


        public Recipe GetPartialByID(int recipeID)
        {
            throw new NotImplementedException();
        }
    }
}
