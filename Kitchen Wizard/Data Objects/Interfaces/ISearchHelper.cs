using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface ISearchHelper
    {
        public List<RecipeClass> SearchRecipeByKeyword(string keyword, UserPreferences prefs);
        public List<RecipeClass> SearchRecipeByFoodList(UserPreferences prefs);
        public List<FoodListItem> SearchFoodDB(string keyword);
    }
}
