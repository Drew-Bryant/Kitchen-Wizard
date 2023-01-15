using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class IKitchenWizardViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NotLoadingData))]
        bool loadingData;
        public bool NotLoadingData => !loadingData;

        [ObservableProperty]
        string title;
    }
}
