using CommunityToolkit.Mvvm.ComponentModel;
using Kitchen_Wizard.Data_Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static Android.Provider.ContactsContract.CommonDataKinds;

namespace Kitchen_Wizard.Models
{
    public partial class ViewRecipePageModel : ObservableObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public int Servings { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsHistory { get; set; }
        public int ID { get; set; }
    }
}
