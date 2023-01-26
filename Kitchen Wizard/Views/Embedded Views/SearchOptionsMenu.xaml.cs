using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views.Embedded_Views;

public partial class SearchOptionsMenu : ContentPage
{
	private RecipeSearchPageModel model;
	public SearchOptionsMenu(RecipeSearchPageModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
		model = viewModel;
	}

	private void SetOptions(object sender, EventArgs e)
	{
		model.SetDefaultOptions();
		model.IsBack = true;
	}
}