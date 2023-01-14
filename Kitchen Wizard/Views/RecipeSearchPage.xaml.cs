using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Models;
using System.Collections.ObjectModel;

namespace Kitchen_Wizard.Views;

public partial class RecipeSearchPage : ContentPage
{

	private ISearchHelper searchHelper;
	private IFoodListHelper foodListHelper;
	private IHistoryHelper historyHelper;
	private IFavoritesHelper favoritesHelper;
	private IRecipeHelper recipeHelper;
	private IUserPreferences userPrefs;
	public RecipeSearchPage(RecipeSearchPageModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

	private void SearchOptions_Clicked(object sender, EventArgs e)
	{
		//NOT IMPLEMENTED - PROBABLY B level
	}
}