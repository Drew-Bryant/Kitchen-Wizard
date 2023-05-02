using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public static class ConversionService
    {

        public static void ConvertTo(this Food item, Unit units)
        {

            if (!item.MyUnits.Contains(units))
            {
                return;
            }

            //get conversion factor and multiply by it
            item.QuantityValue = item.QuantityValue * GetConversionFactor(item.Units, units);

            item.QuantityValue = Math.Round(item.QuantityValue, 2, MidpointRounding.ToEven);
            //update units to reflect new unit
            item.Units = units;

        }

        //GetConversionFactor is a helper method for the ConvertTo function
        private static double GetConversionFactor(Unit current, Unit newUnit)
        {
            //set factor to be 1
            double factor = 1;

            //switch statement for volume based measurements
            switch (current)
            {
                case Unit.tsp:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            //no conversion is done
                            break;
                        case Unit.tbsp:
                            factor = 1 / 3.0;
                            break;
                        case Unit.cups:
                            factor = 1 / 48.0;
                            break;
                        case Unit.cup:
                            factor = 1 / 48.0;
                            break;
                        case Unit.fl_oz:
                            factor = 1 / 6.0;
                            break;
                        case Unit.pints:
                            factor = 1 / 115.3;
                            break;
                        case Unit.quarts:
                            factor = 1 / 230.6;
                            break;
                        case Unit.gallons:
                            factor = 1 / 768.0;
                            break;
                        case Unit.liters:
                            factor = 1 / 202.9;
                            break;
                        case Unit.ml:
                            factor = 4.929;
                            break;
                    }
                    break;
                case Unit.tbsp:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 3;
                            break;
                        case Unit.tbsp:
                            //no conversion
                            break;
                        case Unit.cups:
                            factor = 1 / 16.0;
                            break;
                        case Unit.cup:
                            factor = 1 / 16.0;
                            break;
                        case Unit.fl_oz:
                            factor = 1 / 2.0;
                            break;
                        case Unit.pints:
                            factor = 1 / 32.0;
                            break;
                        case Unit.quarts:
                            factor = 1 / 64.0;
                            break;
                        case Unit.gallons:
                            factor = 1 / 256.0;
                            break;
                        case Unit.liters:
                            factor = 1 / 67.628;
                            break;
                        case Unit.ml:
                            factor = 14.787;
                            break;
                    }
                    break;
                case Unit.cups:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 48;
                            break;
                        case Unit.tbsp:
                            factor = 16;
                            break;
                        case Unit.cups:
                            //no conversion
                            break;
                        case Unit.cup:
                            //no conversion
                            break;
                        case Unit.fl_oz:
                            factor = 8;
                            break;
                        case Unit.pints:
                            factor = 1 / 2.0;
                            break;
                        case Unit.quarts:
                            factor = 1 / 4.0;
                            break;
                        case Unit.gallons:
                            factor = 1 / 16.0;
                            break;
                        case Unit.liters:
                            factor = 1 / 4.227;
                            break;
                        case Unit.ml:
                            factor = 1 / 236.6;
                            break;
                    }
                    break;
                case Unit.fl_oz:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 6;
                            break;
                        case Unit.tbsp:
                            factor = 2;
                            break;
                        case Unit.cups:
                            factor = 1 / 8.0;
                            break;
                        case Unit.cup:
                            factor = 1 / 8.0;
                            break;
                        case Unit.fl_oz:
                            //no conversion
                            break;
                        case Unit.pints:
                            factor = 1 / 16.0;
                            break;
                        case Unit.quarts:
                            factor = 1 / 32.0;
                            break;
                        case Unit.gallons:
                            factor = 1 / 128.0;
                            break;
                        case Unit.liters:
                            factor = 1 / 33.814;
                            break;
                        case Unit.ml:
                            factor = 29.574;
                            break;
                    }
                    break;
                case Unit.pints:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 96;
                            break;
                        case Unit.tbsp:
                            factor = 32;
                            break;
                        case Unit.cups:
                            factor = 2;
                            break;
                        case Unit.cup:
                            factor = 2;
                            break;
                        case Unit.fl_oz:
                            factor = 16;
                            break;
                        case Unit.pints:
                            //no conversion
                            break;
                        case Unit.quarts:
                            factor = 1 / 2.0;
                            break;
                        case Unit.gallons:
                            factor = 1 / 8.0;
                            break;
                        case Unit.liters:
                            factor = 1 / 2.113;
                            break;
                        case Unit.ml:
                            factor = 473.2;
                            break;
                    }
                    break;
                case Unit.quarts:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 192;
                            break;
                        case Unit.tbsp:
                            factor = 64;
                            break;
                        case Unit.cups:
                            factor = 4;
                            break;
                        case Unit.cup:
                            factor = 4;
                            break;
                        case Unit.fl_oz:
                            factor = 32;
                            break;
                        case Unit.pints:
                            factor = 2;
                            break;
                        case Unit.quarts:
                            //no conversion
                            break;
                        case Unit.gallons:
                            factor = 1 / 4.0;
                            break;
                        case Unit.liters:
                            factor = 1 / 1.057;
                            break;
                        case Unit.ml:
                            factor = 946.353;
                            break;
                    }
                    break;
                case Unit.gallons:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 768;
                            break;
                        case Unit.tbsp:
                            factor = 256;
                            break;
                        case Unit.cups:
                            factor = 16;
                            break;
                        case Unit.cup:
                            factor = 16;
                            break;
                        case Unit.fl_oz:
                            factor = 128;
                            break;
                        case Unit.pints:
                            factor = 8;
                            break;
                        case Unit.quarts:
                            factor = 4;
                            break;
                        case Unit.gallons:
                            //no conversion
                            break;
                        case Unit.liters:
                            factor = 3.785;
                            break;
                        case Unit.ml:
                            factor = 3785.41;
                            break;
                    }
                    break;
                case Unit.liters:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 202.884;
                            break;
                        case Unit.tbsp:
                            factor = 67.628;
                            break;
                        case Unit.cups:
                            factor = 4.227;
                            break;
                        case Unit.cup:
                            factor = 4.227;
                            break;
                        case Unit.fl_oz:
                            factor = 1 / 6.0;
                            break;
                        case Unit.pints:
                            factor = 33.814;
                            break;
                        case Unit.quarts:
                            factor = 1.057;
                            break;
                        case Unit.gallons:
                            factor = 1 / 3.785;
                            break;
                        case Unit.liters:
                            //no conversion
                            break;
                        case Unit.ml:
                            factor = 1000;
                            break;
                    }
                    break;
                case Unit.ml:
                    switch (newUnit)
                    {
                        case Unit.tsp:
                            factor = 1 / 4.929;
                            break;
                        case Unit.tbsp:
                            factor = 1 / 14.787;
                            break;
                        case Unit.cups:
                            factor = 1 / 236.6;
                            break;
                        case Unit.cup:
                            factor = 1 / 236.6;
                            break;
                        case Unit.fl_oz:
                            factor = 1 / 29.574;
                            break;
                        case Unit.pints:
                            factor = 1 / 473.2;
                            break;
                        case Unit.quarts:
                            factor = 1 / 946.4;
                            break;
                        case Unit.gallons:
                            factor = 1 / 3785.41;
                            break;
                        case Unit.liters:
                            factor = 1 / 1000;
                            break;
                        case Unit.ml:
                            //no conversion
                            break;
                    }
                    break;
            }

            //switch statement for mass based measurements
            //These are separate switch statements just for code neatness
            switch (current)
            {
                case Unit.lbs:
                    switch (newUnit)
                    {
                        case Unit.lbs:
                            //no conversion
                            break;
                        case Unit.lb:
                            //no conversion
                            break;
                        case Unit.mg:
                            factor = 453592;
                            break;
                        case Unit.grams:
                            factor = 453.592;
                            break;
                        case Unit.kgs:
                            factor = 1 / 2.205;
                            break;
                        case Unit.oz:
                            factor = 16;
                            break;
                    }
                    break;
                case Unit.mg:
                    switch (newUnit)
                    {
                        case Unit.lbs:
                            factor = 1 / 453592.0;
                            break;
                        case Unit.lb:
                            factor = 1 / 453592.0;
                            break;
                        case Unit.mg:
                            //no conversion
                            break;
                        case Unit.grams:
                            factor = 1 / 1000.0;
                            break;
                        case Unit.kgs:
                            factor = 1 / 1000000.0;
                            break;
                        case Unit.oz:
                            factor = 1 / 28350.0;
                            break;
                    }
                    break;
                case Unit.grams:
                    switch (newUnit)
                    {
                        case Unit.lbs:
                            factor = 1 / 453.6;
                            break;
                        case Unit.lb:
                            factor = 1 / 453.6;
                            break;
                        case Unit.mg:
                            factor = 1000;
                            break;
                        case Unit.grams:
                            //no conversion
                            break;
                        case Unit.kgs:
                            factor = 1 / 1000.0;
                            break;
                        case Unit.oz:
                            factor = 1 / 28.35;
                            break;
                    }
                    break;
                case Unit.kgs:
                    switch (newUnit)
                    {
                        case Unit.lbs:
                            factor = 2.205;
                            break;
                        case Unit.lb:
                            factor = 2.205;
                            break;
                        case Unit.mg:
                            factor = 1000000;
                            break;
                        case Unit.grams:
                            factor = 1000;
                            break;
                        case Unit.kgs:
                            //no conversion
                            break;
                        case Unit.oz:
                            factor = 35.274;
                            break;
                    }
                    break;
                case Unit.oz:
                    switch (newUnit)
                    {
                        case Unit.lbs:
                            factor = 1 / 16.0;
                            break;
                        case Unit.lb:
                            factor = 1 / 16.0;
                            break;
                        case Unit.mg:
                            factor = 28350;
                            break;
                        case Unit.grams:
                            factor = 28.35;
                            break;
                        case Unit.kgs:
                            factor = 1 / 35.274;
                            break;
                        case Unit.oz:
                            //no conversion
                            break;
                    }
                    break;
            }
            return factor;
        }

        public static Unit StringToUnitEnum(string unitString)
        {
            Unit unitEnum;
            Enum.TryParse(unitString, out unitEnum);

            return unitEnum;
        }

        public static CuisineType StringToCuisineTypeEnum(string cuisineString)
        {
            CuisineType cuisineEnum;

            Enum.TryParse(cuisineString, out cuisineEnum);

            return cuisineEnum;
        }

        public static DietaryRestrictions StringToDietaryRestrictionsEnum(string dietaryString)
        {
            DietaryRestrictions dietaryEnum;
            Enum.TryParse(dietaryString, out dietaryEnum); ;
            return dietaryEnum;
        }
    }
}
