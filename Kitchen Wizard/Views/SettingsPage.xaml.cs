using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views;

public partial class SettingsPage : ContentPage
{
    private SettingsPageModel model;
	public SettingsPage(SettingsPageModel viewModel)
	{
        model = viewModel;
        BindingContext = viewModel;
        InitializeComponent();

        SearchOptionsGrid.IsVisible = false;

	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        model.UserPrefs.LoadPrefs();
        
    }
    private void ToggleSearchOptionsGrid(object sender, EventArgs e)
    {
        
        if(ShowSearchOptionsButton.Text == "Show")
        {
            ShowSearchOptionsButton.Text = "Hide";
        }
        else
        {
            ShowSearchOptionsButton.Text = "Show";
        }

        SearchOptionsGrid.IsVisible = !SearchOptionsGrid.IsVisible;
    }

    private void QuantityUpdated(object sender, TextChangedEventArgs e)
    {

        //get the entry element that was changed
        Entry sent = sender as Entry;


        if (sent.Text == "" || sent.Text == null || sent.Text.StartsWith("-"))
        {
            return;
        }

        //parse the text into a value
        double value = double.Parse(sent.Text);


        //if the value isn't negative, update the item and save it
        //otherwise just reset the entered value back to its previous value
        if (value >= 0)
        {
            model.UserPrefs.LowWeightNotificationThreshold = value;

            model.UserPrefs.Save();
        }

        FoodListDBHelper.ClearInvalidatedNotifications();

        var foodList = FoodListDBHelper.LoadFoodList();

    }

    private void WeightUnitsChanged(object sender, EventArgs e)
    {
        Picker picker = sender as Picker;

        if (picker.SelectedIndex != model.UserPrefs.LowWeightIndex)
        {
            model.UserPrefs.LowWeightIndex = picker.SelectedIndex;
            model.UserPrefs.LowWeightUnit = (Unit)picker.SelectedItem;
            model.UserPrefs.Save();
        }
    }

    private void VolumeUnitsChanged(object sender, EventArgs e)
    {
        Picker picker = sender as Picker;

        if (picker.SelectedIndex != model.UserPrefs.LowVolumeIndex)
        {
            model.UserPrefs.LowVolumeIndex = picker.SelectedIndex;
            model.UserPrefs.LowVolumeUnit = (Unit)picker.SelectedItem;
            model.UserPrefs.Save();
        }
    }

    private void ClearValue(object sender, FocusEventArgs e)
    {
        Entry entry = sender as Entry;

        entry.Text = "";
    }
}