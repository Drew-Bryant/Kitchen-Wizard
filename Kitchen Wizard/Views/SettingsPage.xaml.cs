using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsPageModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}