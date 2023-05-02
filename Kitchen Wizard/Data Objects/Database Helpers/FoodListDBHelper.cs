using Kitchen_Wizard.Data_Objects.Interfaces;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Database_Helpers
{

    public class FoodListDBItem
    {
        [PrimaryKey]
        public int ID { get; set; }
        public double QuantityValue { get; set; }
        public int UnitIndex { get; set; }
        public string UnitString { get; set; }
        public string Name { get; set; }
        public bool IsSpice { get; set; }
        public bool Unlimited { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool TrackingExpiration { get; set; }
    }
    public static class FoodListDBHelper
    {
        static SQLiteConnection db;
        static void Init()
        {
            if (db == null)
            {
                db = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, "LocalKitchenWizardDB"));
            }

            db.CreateTable<FoodListDBItem>();
        }


        //this method is used to add new foods after searching
        public static void Add(FoodListItem food)
        {
            Init();

            FoodListDBItem item = new FoodListDBItem();
            item.ID = food.ID;
            item.QuantityValue = food.QuantityValue;
            item.UnitString = food.Units.ToString();
            item.Name = food.Name;
            item.IsSpice = food.IsSpice;
            item.Unlimited = food.Unlimited;
            item.TrackingExpiration = food.TrackingExpiration;

            item.ExpirationDate = food.ExpirationDate;


            db.InsertOrReplace(item, typeof(FoodListDBItem));
            //db.Insert(item, typeof(FoodListDBItem));

        }

        public static void UpdateNotificationStatus(FoodListItem food)
        {
            UserPreferences prefs = new UserPreferences();
            if (food.MyUnits.Contains(prefs.LowWeightUnit))
            {
                food.ConvertTo(prefs.LowWeightUnit);
                if (food.QuantityValue >= prefs.LowWeightNotificationThreshold)
                {
                    LocalNotificationCenter.Current.Cancel(food.ID);
                }
            }
            else
            {
                food.ConvertTo(prefs.LowVolumeUnit);
                if (food.QuantityValue >= prefs.LowVolumeNotificationThreshold)
                {
                    LocalNotificationCenter.Current.Cancel(food.ID);
                }
            }

        }

        public static void ClearInvalidatedNotifications()
        {
            Init();

            UserPreferences prefs = new UserPreferences();
            List<FoodListItem> foodList = LoadFoodList();

            List<int> idList = new();

            foreach(var food in foodList)
            {
                FoodListItem copy = new();
                copy.MyUnits = food.MyUnits;
                copy.QuantityValue = food.QuantityValue;
                copy.Units = food.Units;
                //convert the food to the correct units and check if it is lower
                if (food.MyUnits.Contains(prefs.LowWeightUnit))
                {
                    copy.ConvertTo(prefs.LowWeightUnit);
                    if (copy.QuantityValue >= prefs.LowWeightNotificationThreshold)
                    {
                        idList.Add(food.ID);
                    }
                }
                else
                {
                    copy.ConvertTo(prefs.LowVolumeUnit);
                    if (copy.QuantityValue >= prefs.LowVolumeNotificationThreshold)
                    {
                        idList.Add(food.ID);
                    }
                }
            }

            LocalNotificationCenter.Current.Cancel(idList.ToArray());
        }
            public static void CheckLowFoods()
        {
            Init();

            UserPreferences prefs = new UserPreferences();
            List<FoodListItem> foodList = LoadFoodList();

            foreach (var food in foodList)
            {
                bool foodIsLow = false;
                FoodListItem copy = new();
                copy.MyUnits = food.MyUnits;
                copy.QuantityValue = food.QuantityValue;
                copy.Units = food.Units;

                //convert the food to the correct units and check if it is lower
                if (food.MyUnits.Contains(prefs.LowWeightUnit))
                {
                    copy.ConvertTo(prefs.LowWeightUnit);
                    if (copy.QuantityValue < prefs.LowWeightNotificationThreshold)
                    {
                        foodIsLow = true;
                    }
                }
                else
                {
                    copy.ConvertTo(prefs.LowVolumeUnit);
                    if (copy.QuantityValue < prefs.LowVolumeNotificationThreshold)
                    {
                        foodIsLow = true;
                    }
                }

                //set a notification for this food
                if (foodIsLow)
                {
                    //notify the user between an hour from now, and noon tomorrow
                    int notifyTime = food.ID % 36;
                    if (notifyTime < DateTime.Now.Hour)
                    {
                        notifyTime += 12;
                    }

                    var request = new NotificationRequest()
                    {
                        NotificationId = food.ID,
                        Title = $"You are running low on {food.Name}!",
                        Description = $"Tap to go to your food list and start making a shopping list!",
                        CategoryType = NotificationCategoryType.Recommendation,
                        Schedule = new NotificationRequestSchedule()
                        {
                            //Seconds and minutes for testing, in a real world setting it would be days
                            NotifyTime = DateTime.Now.AddSeconds(notifyTime),
                            NotifyRepeatInterval = TimeSpan.FromMinutes(1),
                            RepeatType = NotificationRepeat.TimeInterval,
                        },
                        Android = new AndroidOptions()
                        {
                            LaunchAppWhenTapped = true,
                            TimeoutAfter = TimeSpan.FromDays(7)
                        }
                    };

                    LocalNotificationCenter.Current.Show(request);
                }
            }

            foodList.Save();
        }

        //runs on app load and either removes automatically
        //or indicates when things are expired
        public static void CheckExpirations()
        {
            Init();
            List<FoodListItem> foodList = LoadFoodList();
            foreach(var food in foodList)
            {
                //if expired, mark as expired
                if(food.TrackingExpiration && (DateTime.Compare((DateTime)food.ExpirationDate, DateTime.Today) < 0))
                {
                        food.Expired = true;
                }
                else
                {
                    food.Expired = false;
                }
            }

            foodList.Save();
        }

        public static void ClearFoodList()
        {
            Init();

            IEnumerable<TableMapping> mappings = db.TableMappings;

            foreach (var mapping in mappings)
            {
                if (mapping.MappedType == typeof(FoodListDBItem))
                {
                    db.DropTable(mapping);
                    Debug.WriteLine("Food List Table Dropped");
                    return;
                }
            }
        }

        public static List<int> LoadFoodListIDs()
        {
            Init();
            var dbList = db.Table<FoodListDBItem>().ToList();
            List<int> ids = new();
            foreach (var item in dbList)
            {
                ids.Add(item.ID);
            }

            return ids;
        }
        public static List<FoodListItem> LoadFoodList()
        {
            Init();
            List<FoodListDBItem> dbList = db.Table<FoodListDBItem>().ToList();

            List<FoodListItem> foodList = new();

            foreach (var item in dbList)
            {
                FoodListItem food = new FoodListItem();
                food.ID = item.ID;
                food.TrackingExpiration = item.TrackingExpiration;

                food.Units = ConversionService.StringToUnitEnum(item.UnitString);

                if(food.VolumeUnits.Contains(food.Units))
                {
                    food.MyUnits = food.VolumeUnits;
                }
                else if(food.MassUnits.Contains(food.Units))
                {
                    food.MyUnits = food.MassUnits;
                }

                food.UnitIndex = food.MyUnits.IndexOf(food.Units);
                food.Name = item.Name;
                food.IsSpice = item.IsSpice;
                food.Unlimited = item.Unlimited;
                food.ExpirationDate = item.ExpirationDate;
                food.ExpirationDateVisible = item.ExpirationDate - DateTime.Today;
                
                food.QuantityValue = item.QuantityValue;
                foodList.Add(food);
            }
            return foodList;
            //dont forget to convert enums and quantities
        }

        public static void Delete(FoodListItem food)
        {
            Init();
            db.Table<FoodListDBItem>().Delete(x => x.ID == food.ID);
        }

        //this method is used to update existing food list items
        public static void Save(FoodListItem food)
        {
            Init();

            FoodListDBItem item = new FoodListDBItem();
            item.ID = food.ID;
            item.QuantityValue = food.QuantityValue;
            item.UnitIndex = food.UnitIndex;
            item.UnitString = food.Units.ToString();
            item.Name = food.Name;
            item.IsSpice = food.IsSpice;
            item.Unlimited = food.Unlimited;
            item.TrackingExpiration = food.TrackingExpiration;
            item.ExpirationDate = food.ExpirationDate;

            var updated = db.Update(item, typeof(FoodListDBItem)); //it will already be there, just update based on PK

        }

        public static void Save(this List<FoodListItem> foodList)
        {
            foreach(var food in foodList)
            {
                Save(food);
            }
        }

        public static void MakeRecipe(RecipeClass recipe)
        {
            List<FoodListItem> foodList = LoadFoodList();


            foreach(var ingredient in recipe.Ingredients)
            {
                FoodListItem food = foodList.Find(x => x.Name.ToLower() == ingredient.Name.ToLower());

                //lazy workaround for grocery trip
                if (food != null)
                {
                    Unit unit = ingredient.Units;
                    double value = ingredient.QuantityValue;
                    ConversionService.ConvertTo(ingredient, food.Units);

                    food.QuantityValue -= ingredient.QuantityValue;

                    //lazy workaround for restock being on
                    if (food.QuantityValue < 0)
                    {
                        food.QuantityValue = 0;
                    }
                    ingredient.Units = unit;
                    ingredient.QuantityValue = value;
                }

            }
        }
    }
}
