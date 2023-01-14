using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Data_Objects
{
    public class FoodListItem : Food
    {

        public bool Unlimited { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
