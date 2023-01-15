using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views;

public partial class ViewRecipePage : ContentPage
{
	public ViewRecipePage(ViewRecipePageModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
    }

}