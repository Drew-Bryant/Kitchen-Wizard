namespace Kitchen_Wizard;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Views.FavoritesAndHistoryPage), typeof(Views.FavoritesAndHistoryPage));

    }
}
