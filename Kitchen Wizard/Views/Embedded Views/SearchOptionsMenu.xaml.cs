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

	private void TapCuisineOptionStackPart(object sender, EventArgs e)
	{
        string option = ((VisualElement)sender).BindingContext as string;

        //((sender as HorizontalStackLayout).Children[0] as CheckBox).IsChecked = model.ToggleCuisine(option);
    }

    private void TapCuisineOptionCheckBox(object sender, EventArgs e)
    {
        string option = ((VisualElement)sender).BindingContext as string;

        //(sender as CheckBox).IsChecked = model.ToggleCuisine(option);
    }

    private void TapDietaryOptionStackPart(object sender, EventArgs e)
	{
        string option = ((VisualElement)sender).BindingContext as string;

        //((sender as HorizontalStackLayout).Children[0] as CheckBox).IsChecked = model.ToggleDietary(option);

    }
    private void TapDietaryOptionCheckBox(object sender, EventArgs e)
    {
        string option = ((VisualElement)sender).BindingContext as string;

        //(sender as CheckBox).IsChecked = model.ToggleDietary(option);
    }
}