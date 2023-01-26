using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views.Embedded_Views;

namespace Kitchen_Wizard.Views;

public partial class FoodListPage : ContentPage
{
	private FoodListPageModel model;
	public FoodListPage(FoodListPageModel viewModel)
	{
		InitializeComponent();

		model = viewModel;
		BindingContext = viewModel;
		
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		model.LoadFoodList();
	}

	private async void OpenFoodListSearch(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(FoodListSearchPage));
	}

	private void QuantityUpdated(object sender, TextChangedEventArgs e)
	{
		//get the item being edited
        FoodListItem item = ((VisualElement)sender).BindingContext as FoodListItem;

		//get the entry element that was changed
        Entry sent = sender as Entry;

		//parse the text into a value
		double value = double.Parse(sent.Text);


		//if the value isn't negative, update the item and save it
		//otherwise just reset the entered value back to its previous value
		if(value >= 0)
		{
			item.QuantityValue = value;
			FoodListDBHelper.Save(item);
		}
		else
		{
			sent.Text = item.QuantityValue.ToString();
		}
	}
}