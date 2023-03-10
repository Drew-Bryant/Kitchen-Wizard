using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views.Embedded_Views;

public partial class FavoritesTab : ContentPage
{
    private IRecipeHelper recipeHelper;
    FavoritesAndHistoryPageModel model;
	public FavoritesTab(IRecipeHelper helper, FavoritesAndHistoryPageModel viewModel)
	{
		InitializeComponent();

        recipeHelper = helper;

        BindingContext = viewModel;

        model = viewModel;
	}


    protected override void OnAppearing()
    {
        base.OnAppearing();

        model.LoadFavorites();
    }
	private async void FavoritesItemTapped(object sender, EventArgs e)
	{
        RecipeClass recipe = ((VisualElement)sender).BindingContext as RecipeClass;

        //should never happen...sanity check
        if (recipe == null)
            return;

        recipe = recipeHelper.GetFullByID(recipe.ID);

        await Shell.Current.GoToAsync(nameof(ViewRecipePage), true, new Dictionary<string, object>
        {
            {"Recipe", recipe}
        });
    }
}