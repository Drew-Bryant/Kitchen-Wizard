using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.EventArgs;
namespace Kitchen_Wizard.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            DeviceDisplay.KeepScreenOn = true;
        }

        FoodListDBHelper.CheckExpirations();
        FoodListDBHelper.CheckLowFoods();

        LocalNotificationCenter.Current.NotificationActionTapped +=
            NotificationTapped;

   
    }

    private void NotificationTapped(NotificationActionEventArgs e)
    {
        if(e.IsTapped)
        {
            Shell.Current.GoToAsync(nameof(FoodListPage));
        }
    }

}