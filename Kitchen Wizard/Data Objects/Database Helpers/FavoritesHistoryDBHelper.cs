
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

        public static void AddFavorite(Recipe recipe)
        {
            Init();
            var dbItem = new FavoritesDBItem();
            dbItem.recipeName = recipe.Name;
            dbItem.ID = recipe.ID;
            db.InsertOrReplace(dbItem, typeof(FavoritesDBItem));

        }

        public static void AddHistory(Recipe recipe)
        {
            Init();

            var dbItem = new HistoryDBItem();
            dbItem.recipeName = recipe.Name;
            dbItem.ID = recipe.ID;

            db.InsertOrReplace(dbItem, typeof(HistoryDBItem));

        }


        public static void RemoveFavorite(Recipe recipe)
        {
            Init();

            db.Delete<FavoritesDBItem>(recipe.ID);
        }

        public static void RemoveHistory(Recipe recipe)
        {
            Init();

            db.Delete<HistoryDBItem>(recipe.ID);
        }

        public static List<Recipe> LoadFavorites()
        {
            Init();

            List<FavoritesDBItem> dbList = db.Table<FavoritesDBItem>().ToList();

            List<Recipe> recipes = new();

            foreach(var item in dbList)
            {
                Recipe recipe = new Recipe();
                recipe.ID = item.ID;
                recipe.Name = item.recipeName;
                recipe.IsFavorite = true;
                recipes.Add(recipe);
            }

            return recipes;
        }

        public static List<Recipe> LoadHistory()
        {
            Init();

            List<HistoryDBItem> dbList = db.Table<HistoryDBItem>().ToList();

            List<Recipe> recipes = new();

            foreach (var item in dbList)
            {
                Recipe recipe = new Recipe();
                recipe.ID = item.ID;
                recipe.Name = item.recipeName;
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

    }
}
