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
    }

}