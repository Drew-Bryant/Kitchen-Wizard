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

		model.InitData();
	}

	private async void ClearFoodList(object sender, EventArgs e)
	{
		//ignore empty list
		if(model.FoodList.Count == 0)
		{
			return;
		}

        bool answer = await DisplayAlert("Warning!", "You are about to delete your entire food list. Do you want to continue?", "Yes", "No");

		if(answer == true)
		{
			model.ClearFoodList();
		}	
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

		/*
		//don't do anything until the user actually puts a number in
		if (sent.Text == "" || sent.Text == null || sent.Text == "0" || (sent.Text.Contains("-") && sent.Text.Length == 1)) 
		{
            return;
		}

        if(sent.Text.Contains("-") && sent.Text.Length > 1)
		{
			sent.Text = item.QuantityValue.ToString();
			return;
		}
		*/

		
	if(sent.Text == "" || sent.Text == null || sent.Text.StartsWith("-"))
		{
			return;
		}

        //parse the text into a value
        double value = double.Parse(sent.Text);


		//if the value isn't negative, update the item and save it
		//otherwise just reset the entered value back to its previous value
		if(value >= 0)
		{
			item.QuantityValue = value;

			FoodListDBHelper.Save(item);
		}
		
	}

	private void UnitsChanged(object sender, EventArgs e)
	{
        FoodListItem item = ((VisualElement)sender).BindingContext as FoodListItem;
		if(item == null)
		{
			return;
		}
        Picker picker = sender as Picker;

        if (picker.SelectedIndex != item.UnitIndex)
        {
			Unit unit = (Unit)picker.SelectedItem;
			//Unit unit;
			//Enum.TryParse(itemstring, out unit);
            item.ConvertTo(unit);
			item.Units = unit;
			item.UnitIndex = picker.SelectedIndex;
			FoodListDBHelper.Save(item);
        }
    }

	private void ClearValue(object sender, FocusEventArgs e)
	{
		Entry entry = sender as Entry;

		entry.Text = "";
	}

	//private void SwitchToggled(object sender, ToggledEventArgs e)
	//{
	//	Switch control = sender as Switch;

	//	control.IsToggled = !control.IsToggled;

	//}
}