using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Static_Helpers
{
    public class StaticRecipeHelper : IRecipeHelper
    {

        //Build one Recipe to simulate the format that the real database will take
        //Including parsing strings of steps and ingredients into lists
        public RecipeClass GetFullByID(int recipeID)
        {

            RecipeClass recipe = new();

            //make a recipe specific for testing food list searching
            if (recipeID == 4)
            {
                recipe.Name = "Mushroom, spinach, and Swiss Omelette";
                recipe.ID = recipeID;
                recipe.Description = "A delicious vegetarian omelette";
                recipe.Steps = new List<string>() { "Steps go here"};

                string ingredientString = "1 Cup Spinach,1/2 cup Mushrooms,2 tbsp Olive_Oil,3 oz Eggs,6 oz Swiss_Cheese";
                recipe.Ingredients = ParseAllIngredients(ingredientString);
                recipe.Author = "Drew Bryant";

                recipe.Source = $"www.DrewsAmazingRecipes.com/recipe/{recipe.ID}";

                recipe.Servings = 1;

                recipe.IsFavorite = FavoritesHistoryDBHelper.IsFavorite(recipe.ID);
                recipe.IsHistory = FavoritesHistoryDBHelper.IsHistory(recipe.ID);

                recipe.Cuisine = CuisineType.American;

                recipe.Dietary = new();

                List<DietaryRestrictions> restrictions2 = new() { DietaryRestrictions.Keto, DietaryRestrictions.Vegetarian, DietaryRestrictions.Gluten_Free };
                recipe.Dietary.AddRange(restrictions2);

                return recipe;


            }


            recipe.Name = $"Recipe number {recipeID}";
            recipe.ID = recipeID;
            recipe.Description = "This is the description for the recipe";

            string stepString= "";
            for (int ii = 1; ii < 20; ii++)
            {
                
                stepString += $"{ii}.\tThis is step number {ii},";
            }

            recipe.Steps = ParseAllSteps(stepString);

            string ingredients = "1/2 Cup Oats,2 Cups Sugar,2/3 tsp Honey,12 oz Beer,1 lb Tasty_Ground_Beef";

            recipe.Ingredients = ParseAllIngredients(ingredients);

            recipe.Author = "Drew Bryant";
            recipe.Source = $"www.DrewsAmazingRecipes.com/recipe/{recipe.ID}";

            recipe.Servings = 1;

            recipe.IsFavorite = FavoritesHistoryDBHelper.IsFavorite(recipe.ID);
            recipe.IsHistory = FavoritesHistoryDBHelper.IsHistory(recipe.ID);

            recipe.Cuisine = CuisineType.American;

            recipe.Dietary = new();

            List<DietaryRestrictions> restrictions = new() { DietaryRestrictions.Keto, DietaryRestrictions.Vegetarian, DietaryRestrictions.Gluten_Free };            
            recipe.Dietary.AddRange(restrictions);

            return recipe;
        }

        public RecipeClass GetFullByName(string recipeName)
        {
            throw new NotImplementedException();
        }


        public RecipeClass GetPartialByID(int recipeID)
        {
            throw new NotImplementedException();
        }


        //Parse a string with comma separated ingredient
        //see ParseSingleIngredient below for individual ingredient string format
        private List<Ingredient> ParseAllIngredients(string ingredients)
        {
            //declare the list and split the string by comma to get separate ingredients
            List<Ingredient> ingredientList = new List<Ingredient>();
            string[] stringList = ingredients.Split(',');



            for (int ii = 0; ii < stringList.Length; ii++)
            {
                Ingredient ingredient = ParseSingleIngredient(stringList[ii]);
                ingredientList.Add(ingredient);
            }
            return ingredientList;
        }

        //parse a string representing an ingredient into an Ingredient object
        //ingredientString will be parsed on spaces and have the following format:
        //    <quantity(fractional or whole number)><space><Unit(must be in the Units enum defined in Food.cs)><space><Name of ingredient>
        //quantities must be fractional or whole to facilitate consistent calculation of actual values
        private Ingredient ParseSingleIngredient(string ingredientString)
        {
            Ingredient ingredient = new Ingredient();

            //Split the ingredient string based on spaces
            string[] ingredientParts = ingredientString.Split(' ');

            ingredient.QuantityString = ingredientParts[0];

            //split the quantity part of the string based on a slash
            //if there is only one result, the ingredient is a whole number
            //otherwise it has 2 results then they need to be ratio'd
            string[] quantityParts = ingredientParts[0].Split('/');
            if (quantityParts.Length > 1)
            {
                double a = double.Parse(quantityParts[0]);
                double b = double.Parse(quantityParts[1]);
                ingredient.QuantityValue = (a / b);
            }
            else
            {
                ingredient.QuantityValue = double.Parse(quantityParts[0]);
            }
            //convert the unit from string to enum type and assign it
            //TryParse should never fail if the database is populated correctly
            Unit unit;
            if (Enum.TryParse<Unit>(ingredientParts[1].ToLower(), out unit) == false)
            {
                Debug.WriteLine("Either your database is wrong or the Units enums need another value\n");
            }
            else
            ingredient.Units = unit;


            //parse a multi-word name to remove underscores
            string[] nameParts = ingredientParts[2].Split('_');

            for(int i = 0; i < nameParts.Length; i++)
            {
                ingredient.Name += nameParts[i];

                if (i != nameParts.Length - 1)
                {
                    ingredient.Name += " ";
                }
            }
            ingredient.Name.ToLower();

            return ingredient;
        }

        private List<string> ParseAllSteps(string stepString)
        {
            return stepString.Split(',').ToList();
        }

    }
}