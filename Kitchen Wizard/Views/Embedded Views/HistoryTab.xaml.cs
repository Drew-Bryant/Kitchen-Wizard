using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views.Embedded_Views;

public partial class HistoryTab : ContentPage
{
    private IRecipeHelper recipeHelper;

    public HistoryTab(IRecipeHelper helper, FavoritesAndHistoryPageModel viewModel)
    {
        InitializeComponent();

        recipeHelper = helper;

        BindingContext = viewModel;
    }

    private async void HistoryItemTapped(object sender, EventArgs e)
    {
        Recipe recipe = ((VisualElement)sender).BindingContext as Recipe;

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