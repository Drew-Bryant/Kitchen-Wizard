using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen_Wizard.Models
{
    public partial class DatabaseManagementPageModel : ObservableObject
    {

        [RelayCommand]
        void ClearFavorites()
        {
            FavoritesHistoryDBHelper.ClearFavorites();
        }


        [RelayCommand]
        void ClearHistory()
        {
            FavoritesHistoryDBHelper.ClearHistory();
        }

    }
}
