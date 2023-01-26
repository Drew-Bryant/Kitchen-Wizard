using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface IFavoritesHelper
    {
        public Task Add(RecipeClass recipe);
        public Task Remove(RecipeClass recipe);
        public Task<List<RecipeClass>> LoadFavorites();
    }
}
