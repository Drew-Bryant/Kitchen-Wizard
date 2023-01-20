using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public IEnumerable<string> Steps { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public int Servings { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsNotFavorite { get; set; }
        public bool IsHistory { get; set; }
        public bool IsNotHistory { get; set; }
        public int ID { get; set; }
        public CuisineType Cuisine { get; set; }

        public List<DietaryRestrictions> Dietary { get; set; }

        public void AdjustServings(int newServings)
        {
            ////!!!!!!!!!!!!!!!!!!!!
            ////make sure the following line does floating point division!!!!!!
            //double ratio = newServings / oldServings

            //foreach(Ingredient ingredient in Ingredients)
            //{
                //quantity *= ratio;
                ////-----------do something to keep the numbers from having huge decimal remainders
            //}
        }
    }
}
