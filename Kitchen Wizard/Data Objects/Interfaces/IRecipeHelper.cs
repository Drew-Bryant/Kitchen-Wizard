using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface IRecipeHelper
    {
        public Recipe GetFullByName(string recipeName);
        public Recipe GetFullByID(int recipeID);
        public Recipe GetPartialByID(int recipeID);
    }
}
