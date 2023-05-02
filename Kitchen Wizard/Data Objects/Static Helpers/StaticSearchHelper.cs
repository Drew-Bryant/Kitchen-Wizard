using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Data_Objects.Static_Helpers;
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
            List<RecipeClass> recipes = SearchRecipeByKeyword("", prefs);
            List<FoodListItem> foodList = FoodListDBHelper.LoadFoodList();

            List<RecipeClass> filteredList = ValidateFoodList(recipes, foodList, prefs);

            return FilterByCuisineAndDietary(filteredList, prefs);
            //Should get values from the database based on matching cuisine types
        }
        public List<FoodListItem> SearchFoodDB(string keyword)
        {
            List<FoodListItem> results = new List<FoodListItem>();
            Dictionary<int, string> ingredients = new Dictionary<int, string>()
            {
                {0, "Spinach"},
                {1, "Mushrooms"},
                {2, "Olive Oil"},
                {3, "Eggs"},
                {4, "Swiss Cheese"},
            };

            Dictionary<int, Unit> units = new Dictionary<int, Unit>()
            {
                {0, Unit.cup},
                {1, Unit.cup},
                {2, Unit.tbsp},
                {3, Unit.oz},
                {4, Unit.oz},
            };

            for (int ii = 0; ii < 5; ii++)
            {
                FoodListItem item = new FoodListItem();

                if(ii < 5)
                {
                    item.Name = ingredients[ii];
                }
                else
                {
                    item.Name = "Food Number" + ii;
                }

                item.QuantityValue = 0;
                item.QuantityString = "0";

                if (ii < 5)
                {
                    item.Units = units[ii];
                }
                else
                {
                    item.Units = (ii % 2 == 0) ? Unit.cups : Unit.grams; //make half of them cups and half grams
                }

                item.IsSpice = false;

                if (ii >= 5)
                {
                    item.IsSpice = (ii % 2 == 0); //make half of them spices for testing
                }

                item.ID = ii;
                item.Unlimited = false; //default unlimited to false on searched items

                if(item.IsSpice)
                {
                    item.Name = $"Spice number {ii}"; 
                }

                results.Add(item);
            }

            return results;
        }

        //future changes to this function:
        //if restock == true, skip
        private List<RecipeClass> ValidateFoodList(List<RecipeClass> recipeList, List<FoodListItem> foodList, UserPreferences prefs)
        {
            StaticRecipeHelper recipeHelper = new StaticRecipeHelper();
            List<RecipeClass> filteredList = new List<RecipeClass>();
            foreach(var recipe in recipeList)
            {
                RecipeClass fullRecipe = recipeHelper.GetFullByID(recipe.ID);

                bool isValid = true;
                int unMatchedCount = 0;
                foreach(var ingredient in fullRecipe.Ingredients)
                { 
                    //Check for existence of the ingredient in the food list
                    //if grocery trip is on, it will allow x number of foods not in the list
                    //if even one ingredient doesn't match under normal circumstances, there is no need
                    //to loop through the rest of the ingredients
                    if (!foodList.Any(food => (food.Name).ToLower() == (ingredient.Name).ToLower()))
                    {
                        unMatchedCount++;

                        if(prefs.GroceryTrip && unMatchedCount > prefs.GTAllowance)
                        {
                            isValid = false;
                            break;
                        }
                        else if(!prefs.GroceryTrip && unMatchedCount > 0)
                        {
                            isValid = false;
                            break;
                        }
                    }

                    //Check for valid quantity of the food, assuming it was found
                    //if restock is on, this step is skipped
                    if (!prefs.Restock)
                    {
                        List<FoodListItem> items = foodList.Where(food => (food.Name).ToLower() == (ingredient.Name).ToLower()).ToList();

                        //only one in the list
                        //convert its units and make sure there's enough
                        foreach (var food in items)
                        {
                            food.ConvertTo(ingredient.Units);
                            if (food.QuantityValue < ingredient.QuantityValue && !food.Unlimited)
                            {
                                isValid = false;
                                break;
                            }
                        }
                    }
                    
                }

                //add the food to the filtered list only if it passed all checks
                if(isValid)
                {
                    filteredList.Add(recipe);
                }
            }
            return filteredList;
        }
    }
}
