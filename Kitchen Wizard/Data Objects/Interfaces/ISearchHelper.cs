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
        public List<Recipe> SearchRecipeByKeyword(string keyword, UserPreferences prefs);
        public List<Recipe> SearchRecipeByFoodList(UserPreferences prefs);
        public List<Food> SearchFoodDB(string keyword);
    }
}
