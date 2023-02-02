using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public partial class Ingredient : Food
    {
        [ObservableProperty]
        double quantityValue;
    }
}
