using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{

    public partial class FoodListItem : Food
    {
        public string UnlimitedButtonText { get; set; }

        [ObservableProperty]
        double quantityValue;

        [ObservableProperty]
        bool unlimited;
        public DateTime ExpirationDate { get; set; }

        public void ConvertTo(Unit units)
        {

            if (!MyUnits.Contains(units))
            {
                return;
            }

            //get conversion factor and multiply by it
            QuantityValue = QuantityValue * base.GetConversionFactor(Units, units);

            //update units to reflect new unit
            Units = units;

        }
    }
}
