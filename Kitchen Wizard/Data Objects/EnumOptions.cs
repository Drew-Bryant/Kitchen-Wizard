using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public class EnumOptions
    {
        public enum CuisineType
        {
            Any,
            Mexican,
            Italian,
            Asian,
            Indian,
            American
        }

        public enum DietaryRestrictions
        {
            Gluten,
            Keto,
            None,
            Vegan,
            Vegetarian
        }
    }
}
