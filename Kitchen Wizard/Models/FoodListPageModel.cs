using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class FoodListPageModel : IKitchenWizardViewModel
    {

        public ObservableCollection<FoodListItem> FoodList { get; set; } = new();
        public ObservableCollection<FoodListItem> SearchResults { get; set; } = new();

        UserPreferences userPrefs { get; set; } = new();


        [ObservableProperty]
        string searchField;

        [ObservableProperty]
        string expirationTrackingPlaceholderText = "Expiration tracking is turned off\nfor this item";

        private ISearchHelper searchHelper;
        public FoodListPageModel(ISearchHelper _searchHelper)
        {
            searchHelper = _searchHelper;
            Title = "Food List";
        }

        public void InitData()
        {
            userPrefs.LoadPrefs();
            FoodList.Clear();

            List<FoodListItem> foodList = FoodListDBHelper.LoadFoodList().OrderBy(x => x.Name).ToList();

            foreach (var item in foodList)
            {
                FoodList.Add(item);

                if (item.IsSpice && userPrefs.InfiniteSpices == true)
                {
                    item.Unlimited = true;
                }
            }
            Debug.WriteLine("Finished loading food list\n");
        }


        public void AddFood(FoodListItem food)
        {
            food.ExpirationDate = DateTime.Today;

            if(userPrefs.TrackingExpirationDates)
            {
                food.TrackingExpiration = true;
            }

            FoodListDBHelper.Add(food);
            if(FoodList.Select(x => x.ID == food.ID).Count() < 1)
            {
                FoodList.Add(food);
                FoodList.OrderBy(x => x.Name);
            }
        }

        public void ClearFoodList()
        {
            FoodList.Clear();
            FoodListDBHelper.ClearFoodList();
        }

        public void UpdateExpirationDate(FoodListItem food)
        {
            food.ExpirationDate = DateTime.Today + food.ExpirationDateVisible;
            if (food.ExpirationDate < DateTime.Today)
            {
                food.Expired = true;
            }
            else
            {
                food.Expired = false;
            }
            FoodListDBHelper.Save(food);
        }

        [RelayCommand]
        public void ToggleExpirationTracking(FoodListItem food)
        {
            food.TrackingExpiration = !food.TrackingExpiration;

            //if (food.ExpirationDate == null)
            //{
            //    food.ExpirationDate = DateTime.Today;
            //}
            FoodListDBHelper.Save(food);
        }



        [RelayCommand]
        public void DeleteFood(FoodListItem food)
        {
            FoodList.Remove(food);
            FoodListDBHelper.Delete(food);

            LocalNotificationCenter.Current.Cancel(food.ID);
        }

        [RelayCommand]
        public void FoodListSearch(string keyword)
        {
            SearchResults.Clear();
            List<FoodListItem> results = searchHelper.SearchFoodDB(keyword).OrderBy(x => x.Name).ToList();

            foreach(var result in results)
            {
                SearchResults.Add(result);
            }

        }
        [RelayCommand]
        public void ToggleInfinite(FoodListItem item)
        {
            item.Unlimited = !item.Unlimited;

            FoodListDBHelper.Save(item);

            //LoadFoodList();

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
        public void IncrementQuantity(FoodListItem item)
        {
            item.QuantityValue += 1;
            FoodListDBHelper.Save(item);

            foreach (var food in FoodList)
            {
                if(food.ID == item.ID)
                {
                    food.QuantityValue = item.QuantityValue;
                    return;
                }
            }
            //LoadFoodList();
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

            foreach (var food in FoodList)
            {
                if (food.ID == item.ID)
                {
                    food.QuantityValue = item.QuantityValue;
                    return;
                }
            }
        }
    }
}
