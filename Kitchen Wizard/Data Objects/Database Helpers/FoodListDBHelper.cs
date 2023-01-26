using Kitchen_Wizard.Data_Objects.Interfaces;
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
        public int ID { get; set }
        public double QuantityValue { get; set; }
        //public string QuantityString { get; set; } not needed to store I think, just tostring the quantity
        public string UnitString { get; set; }
        public string Name { get; set; }
        public bool IsSpice { get; set; }
        public bool Unlimited { get; set; }
        public DateTime ExpirationDate { get; set; }
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
            item.ExpirationDate = food.ExpirationDate;
            db.Insert(item, typeof(FoodListDBItem));

        }


        //runs on app load and either removes automatically
        //or indicates when things are expired
        public static int CheckExpirations()
        {
            Init();
            throw new NotImplementedException();
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

        public static List<FoodListItem> LoadFoodList()
        {
            Init();
            List<FoodListDBItem> dbList = db.Table<FoodListDBItem>().ToList();

            List<FoodListItem> foodList = new();

            foreach (var item in dbList)
            {
                FoodListItem food = new FoodListItem();
                food.ID = item.ID;
                food.QuantityValue = food.QuantityValue;

                //parse the string into an enum
                Unit units;
                Enum.TryParse(item.UnitString, out units);
                food.Units = units;

                food.Name = item.Name;
                food.IsSpice = item.IsSpice;
                food.Unlimited = item.Unlimited;
                food.ExpirationDate = item.ExpirationDate;

                foodList.Add(food);
            }
            return foodList;
            //dont forget to convert enums and quantities
        }

        public static void Remove(FoodListItem food)
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
            item.UnitString = food.Units.ToString();
            item.Name = food.Name;
            item.IsSpice = food.IsSpice;
            item.Unlimited = food.Unlimited;
            item.ExpirationDate = food.ExpirationDate;

            db.Update(item, typeof(FoodListDBItem)); //it will already be there, just update based on PK
        }
    }
}
