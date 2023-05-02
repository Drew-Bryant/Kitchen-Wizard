using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views.Embedded_Views;

namespace Kitchen_Wizard.Views;

public partial class FoodListPage : ContentPage
{
	private FoodListPageModel model;

	bool pageLoading = false;
	public FoodListPage(FoodListPageModel viewModel)
	{
        BindingContext = viewModel;
        InitializeComponent();

		model = viewModel;
		
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

		if(item == null)
		{
			return;
		}

		//get the entry element that was changed
		Entry entry = sender as Entry;

		if(entry.Text == "" || entry.Text == null || entry.Text.StartsWith("-"))
		{
			return;
		}

        //parse the text into a value
        double value = double.Parse(entry.Text);


		item.QuantityValue = value;
        FoodListDBHelper.UpdateNotificationStatus(item);

        FoodListDBHelper.Save(item);
		
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
            //item.ConvertTo(unit);
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

	private void ExpirationDateChanged(object sender, DateChangedEventArgs e)
	{
		FoodListItem item = (sender as VisualElement).BindingContext as FoodListItem;

        //For some reason all the xaml.cs functions run on page appearing
        //causing null values to be passed into functions because
        //the model hasn't been set yet
        if (item == null)
		{
			return;
		}


		model.UpdateExpirationDate(item);
	}

	//private void SwitchToggled(object sender, ToggledEventArgs e)
	//{
	//	Switch control = sender as Switch;

	//	control.IsToggled = !control.IsToggled;

	//}
}