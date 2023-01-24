using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Models;
using System.Collections.ObjectModel;

namespace Kitchen_Wizard.Views;

public partial class RecipeSearchPage : ContentPage
{

	private IRecipeHelper recipeHelper;
	private RecipeSearchPageModel model;

    public RecipeSearchPage(RecipeSearchPageModel viewModel, IRecipeHelper helper)
	{
        InitializeComponent();

        BindingContext = viewModel;
		recipeHelper = helper;
		model = viewModel;
		model.LoadPrefs();
	}


    private async void SearchOptionsClicked(object sender, EventArgs e)
    {
        model.LoadPrefs();
        await Shell.Current.GoToAsync("SearchOptionsMenu");

    }

    private void SearchResultTapped(object sender, EventArgs e)
	{
		Recipe recipe = ((VisualElement)sender).BindingContext as Recipe;

		//should never happen...sanity check
		if (recipe == null)
			return;

		recipe = recipeHelper.GetFullByID(recipe.ID);

		Shell.Current.GoToAsync(nameof(ViewRecipePage), true, new Dictionary<string, object>
		{
			{"Recipe", recipe},
		});
	}

}