using Kitchen_Wizard.Views;

namespace Kitchen_Wizard;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		FavoritesAndHistoryPage favoritesAndHistoryPage = new FavoritesAndHistoryPage();
	}
}
