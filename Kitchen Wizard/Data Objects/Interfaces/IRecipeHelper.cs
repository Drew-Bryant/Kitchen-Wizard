using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface IRecipeHelper
    {
        public RecipeClass GetFullByName(string recipeName);
        public RecipeClass GetFullByID(int recipeID);
        public RecipeClass GetPartialByID(int recipeID);
    }
}
