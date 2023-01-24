using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views.Embedded_Views;

public partial class SearchOptionsMenu : ContentPage
{
	public SearchOptionsMenu(RecipeSearchPageModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
        viewModel.LoadPrefs();
    }


}