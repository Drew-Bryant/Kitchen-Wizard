using CommunityToolkit.Mvvm.ComponentModel;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
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

        partial void OnQuantityValueChanged(double value)
        {
            if (FoodListDBHelper.LoadFoodListIDs().Contains(this.ID))
            {
                FoodListDBHelper.Save(this);
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NotExpired))]
        [NotifyPropertyChangedFor(nameof(ExpiryLabelVisible))]
        bool expired;

        public bool NotExpired => !Expired;

        public bool ExpiryLabelVisible => !Expired && TrackingExpiration;
        [ObservableProperty]
        bool unlimited;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NotTrackingExpiration))]
        [NotifyPropertyChangedFor(nameof (ExpiryLabelVisible))]
        bool trackingExpiration;


        public bool NotTrackingExpiration => !TrackingExpiration;

        [ObservableProperty]
        public DateTime expirationDate;

        [ObservableProperty]
        public TimeSpan expirationDateVisible;

        //partial void OnExpirationDateChanged(DateTime value)
        //{
        //    FoodListDBHelper.Save(this);
        //}


    }
}
