using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views.Embedded_Views;

namespace Kitchen_Wizard.Views;

public partial class ViewRecipePage : ContentPage
{
	public ViewRecipePage(ViewRecipePageModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
    }

    private void IngredientsClicked(object sender, EventArgs e)
    {

        Shell.Current.GoToAsync(nameof(IngredientsTab));
    }
}