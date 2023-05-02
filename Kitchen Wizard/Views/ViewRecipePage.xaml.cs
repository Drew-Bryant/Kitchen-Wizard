using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views.Embedded_Views;

namespace Kitchen_Wizard.Views;

public partial class ViewRecipePage : ContentPage
{
    public ViewRecipePageModel model;
	public ViewRecipePage(ViewRecipePageModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
        model = viewModel;

    }

    private void IngredientsClicked(object sender, EventArgs e)
    {

        Shell.Current.GoToAsync(nameof(IngredientsTab));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        model.InitProperties();

    }

    private void MakeRecipeClicked(object sender, EventArgs e)
    {
        UserPreferences prefs = new();

        if(prefs.SubtractFoods)
        {
            FoodListDBHelper.MakeRecipe(model.Recipe);
        }
    }
}