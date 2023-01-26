using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class FoodListPageModel : IKitchenWizardViewModel
    {

        public ObservableCollection<FoodListItem> FoodList { get; set; } = new();

        
        public void LoadFoodList()
        {
            FoodList.Clear();

            List<FoodListItem> foodList = FoodListDBHelper.LoadFoodList().OrderBy(x => x.Name).ToList();

            foreach(var item in foodList)
            {
                FoodList.Add(item);
            }
        }

        [RelayCommand]
        public void ToggleInfinite(FoodListItem item)
        {
            item.Unlimited = !item.Unlimited;

            FoodListDBHelper.Save(item);

            //I wanted to use FoodList.Where to do this but you can't change values it seems
            foreach (var food in FoodList)
            {
                if(food.ID == item.ID)
                {
                    food.Unlimited = item.Unlimited;
                    return;
                }
            }

        }

        [RelayCommand]
        public void IncrementQuantity(FoodListItem item)
        {
            item.QuantityValue += 1;

            FoodListDBHelper.Save(item);
            //I wanted to use FoodList.Where to do this but you can't change values it seems
            foreach (var food in FoodList)
            {
                if (food.ID == item.ID)
                {
                    food.Unlimited = item.Unlimited;
                    return;
                }
            }
        }

        [RelayCommand]
        public void DecrementQuantity(FoodListItem item)
        {
            //bail out if the decrement would produce a negative quantity
            if(item.QuantityValue - 1 < 0)
            {
                return;
            }

            item.QuantityValue -= 1;

            FoodListDBHelper.Save(item);
            //I wanted to use FoodList.Where to do this but you can't change values it seems
            foreach (var food in FoodList)
            {
                if (food.ID == item.ID)
                {
                    food.Unlimited = item.Unlimited;
                    return;
                }
            }
        }
    }
}
