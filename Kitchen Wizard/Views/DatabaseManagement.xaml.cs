using Kitchen_Wizard.Models;

namespace Kitchen_Wizard.Views;

public partial class DatabaseManagement : ContentPage
{
	public DatabaseManagement(DatabaseManagementPageModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}