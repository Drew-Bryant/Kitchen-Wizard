using CommunityToolkit.Mvvm.ComponentModel;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class FavoritesAndHistoryPageModel : IKitchenWizardViewModel
    {
        private IFavoritesHelper favoritesHelper;
        private IHistoryHelper historyHelper;
        public FavoritesAndHistoryPageModel(IFavoritesHelper _favHelper, IHistoryHelper _historyHelper)
        {
            favoritesHelper = _favHelper;
            historyHelper = _historyHelper;

            Title = "Favorites and History";
        }

        public ObservableCollection<Recipe> Favorites { get; set; } = new();
        public ObservableCollection<Recipe> History { get; set; } = new();

}
}
