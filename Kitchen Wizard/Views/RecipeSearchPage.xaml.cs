using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views.Embedded_Views;
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

		//load prefs first time search page is loaded
		model.LoadPrefs();
	}


    private async void SearchOptionsClicked(object sender, EventArgs e)
    {
        model.SetDefaultOptions();
        await Shell.Current.GoToAsync(nameof(SearchOptionsMenu));

    }

    private void SearchResultTapped(object sender, EventArgs e)
	{
		RecipeClass recipe = ((VisualElement)sender).BindingContext as RecipeClass;

		//should never happen...sanity check
		if (recipe == null)
			return;

		recipe = recipeHelper.GetFullByID(recipe.ID);

		Shell.Current.GoToAsync(nameof(ViewRecipePage), true, new Dictionary<string, object>
		{
			{"Recipe", recipe},
		});
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!model.IsBack)
        {
            model.LoadPrefs();
        }
        else
        {
            model.IsBack = false;
        }
        //a piece of code specified
    }
    private void SetOptions(object sender, EventArgs e)
	{
		if (!model.IsBack)
		{
			model.LoadPrefs();
		}
		else
		{
			model.IsBack = false;
		}
	}
}