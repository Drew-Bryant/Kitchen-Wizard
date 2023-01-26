
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views;
using LiteDB;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Database_Helpers
{
    public class FavoritesDBItem
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string recipeName { get; set; }
    }

    public class HistoryDBItem
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string recipeName { get; set; }
        public DateTime timestamp { get; set; }
    }

    public static class FavoritesHistoryDBHelper
    {
         static SQLiteConnection db;
        static void Init()
        {
            if (db == null)
            {
                db = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, "LocalKitchenWizardDB"));
            }


            db.CreateTable<FavoritesDBItem>();
            db.CreateTable<HistoryDBItem>();
        }

        public static void AddFavorite(RecipeClass recipe)
        {
            Init();
            var dbItem = new FavoritesDBItem();
            dbItem.recipeName = recipe.Name;
            dbItem.ID = recipe.ID;
            db.InsertOrReplace(dbItem, typeof(FavoritesDBItem));

        }

        public static DateTime AddHistory(RecipeClass recipe)
        {
            Init();

            var dbItem = new HistoryDBItem();
            dbItem.recipeName = recipe.Name;
            dbItem.ID = recipe.ID;
            dbItem.timestamp = DateTime.Now;

            db.InsertOrReplace(dbItem, typeof(HistoryDBItem));

            return dbItem.timestamp;

        }


        public static void RemoveFavorite(RecipeClass recipe)
        {
            Init();

            db.Delete<FavoritesDBItem>(recipe.ID);
        }

        public static void RemoveHistory(RecipeClass recipe)
        {
            Init();


            db.Table<HistoryDBItem>().Delete(x => x.timestamp == recipe.HistoryDate);
        }

        public static List<RecipeClass> LoadFavorites()
        {
            Init();

            List<FavoritesDBItem> dbList = db.Table<FavoritesDBItem>().ToList();

            List<RecipeClass> recipes = new();

            foreach(var item in dbList)
            {
                RecipeClass recipe = new RecipeClass();
                recipe.ID = item.ID;
                recipe.Name = item.recipeName;
                recipe.IsFavorite = true;
                recipes.Add(recipe);
            }

            return recipes;
        }

        public static List<RecipeClass> LoadHistory()
        {
            Init();

            List<HistoryDBItem> dbList = db.Table<HistoryDBItem>().ToList();

            List<RecipeClass> recipes = new();

            foreach (var item in dbList)
            {
                RecipeClass recipe = new RecipeClass();
                recipe.ID = item.ID;
                recipe.Name = item.recipeName;
                recipe.HistoryDate = item.timestamp;
                recipe.IsHistory = true;
                recipes.Add(recipe);
            }

            return recipes;
        }

        public static void ClearFavorites()
        {
            Init();

            IEnumerable<TableMapping> mappings = db.TableMappings;

            foreach (var mapping in mappings)
            {
                if (mapping.MappedType == typeof(FavoritesDBItem))
                {
                    db.DropTable(mapping);
                    Debug.WriteLine("Favorites Table Dropped");
                    return;
                }
            }
        }
        public static void ClearHistory()
        {
            Init();

            var mappings = db.TableMappings;

            foreach (var mapping in mappings)
            {
                if(mapping.MappedType == typeof(HistoryDBItem))
                {
                    db.DropTable(mapping);
                    Debug.WriteLine("History Table Dropped");
                    return;

                }
            }
        }

        public static bool IsFavorite(int recipeID)
        {
            Init();

            //return true if the table contains this ID
            return db.Table<FavoritesDBItem>().Where(x => recipeID == x.ID).Count() > 0;
        }

        public static bool IsHistory(int recipeID)
        {
            Init();

            //return true if the table contains this ID
            return db.Table<HistoryDBItem>().Where(x => recipeID == x.ID).Count() > 0;
        }

        public static DateTime GetHistoryDate(RecipeClass recipe)
        {
            List<HistoryDBItem> items = db.Table<HistoryDBItem>().Where(x => x.ID == recipe.ID).ToList();

            HistoryDBItem dbItem = items[0];
            if(items.Count > 1)
            {
                foreach(var item in items)
                {
                    if(item.timestamp > dbItem.timestamp)
                    {
                        dbItem = item;
                    }
                }
            }

            return dbItem.timestamp;
        }

    }
}
