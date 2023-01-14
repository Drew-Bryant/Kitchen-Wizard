using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public enum Unit
    {
        //Group A
        Tsp,
        Tbsp,
        Cup,
        Fl_Oz,
        Dry_Oz,
        Oz,
        Pint,
        Quart,
        Gallon,
        Pound,
        mg,
        g,
        kg,
        ml,
        l,
        F,
        C
    }
    public class Food
    {

        public double Quantity { get; set; }
        public Unit Units { get; set; }
        public string Name { get; set; }
        public bool IsSpice { get; set; }
        public bool ID { get; set; }

        public void ConvertTo(Unit units)
        {
            //convert current type to passed type
            
            //TODO: split units up into groups of compatible conversions
            //      Then have a switch or something for each one
            //      there is no return, it just updates the object
        }

    }
}
