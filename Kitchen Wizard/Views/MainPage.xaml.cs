namespace Kitchen_Wizard.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        DeviceDisplay.KeepScreenOn = true;
    }

    private async void GoToFavoritesHistory(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync(nameof(FavoritesAndHistoryPage));
    }
}