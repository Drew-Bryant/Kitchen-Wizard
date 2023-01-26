using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects.Database_Helpers
{
    public class StaticSearchHelper : ISearchHelper
    {
        public List<RecipeClass> SearchRecipeByKeyword(string keyword, UserPreferences prefs)
        {
         
            List<RecipeClass> recipes = new();

            for(int ii = 0; ii < 10; ii++)
            {
                RecipeClass recipe = new RecipeClass();

                recipe.Name = "Recipe number" + (ii + 1);
                recipe.Cuisine = CuisineType.American;
                recipe.Dietary = new() { DietaryRestrictions.Keto, DietaryRestrictions.Vegetarian, DietaryRestrictions.Gluten_Free };

                recipe.ID = ii + 1;

                recipes.Add(recipe);
            }



            return FilterByCuisineAndDietary(recipes, prefs);
        }

        private List<RecipeClass> FilterByCuisineAndDietary(List<RecipeClass> recipes, UserPreferences prefs)
        {
            List<RecipeClass> filteredList = FilterByCuisine(recipes, prefs);

            return FilterByDietary(filteredList, prefs);

        }

        private List<RecipeClass> FilterByCuisine(List<RecipeClass> recipes, UserPreferences prefs)
        {
            //If preference is Any, all recipes are fine
            if(prefs.Cuisine.Count == 1 && prefs.Cuisine[0] == CuisineType.Any.ToString())
            {
                return recipes;
            }

            //otherwise, filter by whatever preferences are selected
            return recipes.Where(recipe => prefs.Cuisine.Contains(recipe.Cuisine.ToString())).ToList();
        }

        private List<RecipeClass> FilterByDietary(List<RecipeClass> recipes, UserPreferences prefs)
        {
            //if there's no restrictions, the whole list is fine
            if(prefs.Dietary.Count == 1 && prefs.Dietary[0] == DietaryRestrictions.None.ToString())
            {
                return recipes;
            }

            List<RecipeClass> dietaryList = new();

            foreach(var recipe in recipes)
            {
                bool accepted = true;

                //for each dietary restriction that is compatible with the recipe
                foreach(var restriction in prefs.Dietary)
                {
                    DietaryRestrictions enumName;

                    Enum.TryParse<DietaryRestrictions>(restriction, out enumName);

                    //if even one restriction does not match the preference, this recipe is invalid
                    if(recipe.Dietary.Contains(enumName) == false)
                    {
                        accepted = false;
                    }
                }

                if(accepted)
                {
                    dietaryList.Add(recipe);
                }
            }

            return dietaryList;
        }
        public List<RecipeClass> SearchRecipeByFoodList(UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
        public List<FoodListItem> SearchFoodDB(string keyword)
        {
            List<FoodListItem> results = new List<FoodListItem>();

            for(int ii = 1; ii < 11; ii++)
            {
                FoodListItem item = new FoodListItem();

                item.Name = "Food Number" + ii;
                item.QuantityValue = 0;
                item.QuantityString = "0";
                item.Units = (ii % 2 == 0) ? Unit.Cup : Unit.Tbsp; //make half of them cups and half tbsp
                item.IsSpice = (ii % 2 == 0); //make half of them spices for testing
                item.ID = ii;
                item.Unlimited = false; //default unlimited to false on searched items
            }

            return results;
        }
        private List<RecipeClass> ValidateFoodList(List<RecipeClass> recipeList, List<Food> foodList, UserPreferences prefs)
        {
            throw new NotImplementedException();
        }
    }
}
