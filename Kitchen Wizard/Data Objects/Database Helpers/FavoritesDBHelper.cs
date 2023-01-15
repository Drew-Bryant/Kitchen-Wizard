using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Views;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Database_Helpers
{
    public class FavoritesDBHelper : IFavoritesHelper
    {

        private const string favoritesKey = "favorites";
        private IRecipeHelper recipeHelper;

        public FavoritesDBHelper(IRecipeHelper helper)
        {
            recipeHelper = helper;
        }
        public void Add(Recipe recipe)
        {
            using var db = new LiteDatabase(GetFilePath());
            var collection = db.GetCollection<int>(favoritesKey);
            collection.Insert(recipe.ID);
        }

        public List<Recipe> LoadFavorites()
        {
            using var db = new LiteDatabase(GetFilePath());
            var collection = db.GetCollection<int>(favoritesKey);
            IEnumerable<int> ids = collection.FindAll();

            List<Recipe> recipes = new();

            foreach(int id in ids)
            {
                Recipe recipe = recipeHelper.GetFullByID(id);
                recipes.Add(recipe);
            }
            return recipes;
        }

        public void Remove(Recipe recipe)
        {
            using var db = new LiteDatabase(GetFilePath());
            var collection = db.GetCollection<int>(favoritesKey);
            collection.Delete(recipe.ID);
        }

        private string GetFilePath()
        {
            var path = FileSystem.Current.AppDataDirectory;

            return Path.Combine(path, "localKitchenWizardDB.db");
           
        }

        


    }
}
