using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views.Embedded_Views;

public partial class IngredientsTab : ContentPage
{
	public IngredientsTab(ViewRecipePageModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}