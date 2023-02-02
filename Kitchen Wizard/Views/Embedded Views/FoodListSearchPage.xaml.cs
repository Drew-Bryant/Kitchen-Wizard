using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views.Embedded_Views;

public partial class FoodListSearchPage : ContentPage
{
    private FoodListPageModel model;

	public FoodListSearchPage(FoodListPageModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        model = viewModel;
	}

	private void SearchResultTapped(object sender, EventArgs e)
	{
        FoodListItem food = ((VisualElement)sender).BindingContext as FoodListItem;

        //should never happen...sanity check
        if (food == null)
            return;

        model.AddFood(food);
        
        Shell.Current.GoToAsync("..");
    }
}