﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public int Servings { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsHistory { get; set; }
        public int ID { get; set; }

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
