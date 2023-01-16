
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Views;
using LiteDB;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
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
            if (db != null)
                return;

            db = new SQLiteConnection(Path.Combine(FileSystem.AppDataDirectory, "LocalKitchenWizardDB"));

            db.CreateTable<FavoritesDBItem>();
            db.CreateTable<HistoryDBItem>();
        }

        public static void AddFavorite(Recipe recipe)
        {
            Init();
            db.InsertOrReplace(new FavoritesDBItem() { ID = recipe.ID, recipeName = recipe.Name});
        }

        public static void AddHistory(Recipe recipe)
        {
            Init();


            var dbItem = new HistoryDBItem();
            dbItem.recipeName = recipe.Name;
            dbItem.ID = recipe.ID;

            db.InsertOrReplace(dbItem);

        }


        public static void RemoveFavorite(Recipe recipe)
        {
            Init();

            db.Delete<FavoritesDBItem>(recipe.ID);
        }

        public static void RemoveHistory(Recipe recipe)
        {
            Init();

            db.Delete<FavoritesDBItem>(recipe.ID);
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

    }
}
