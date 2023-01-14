using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Interfaces
{
    public interface IFoodListHelper
    {
        public void Add(Food food);
        public void Save(Food food);
        public void Remove(Food food);
        public List<FoodListItem> LoadFoodList();
        public void ClearFoodList();
        public int CheckExpirations();
    }
}
