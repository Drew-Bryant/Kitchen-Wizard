using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface IFavoritesHelper
    {
        public void Add(Recipe recipe);
        public void Remove(Recipe recipe);
        public List<Recipe> LoadFavorites();
    }
}
