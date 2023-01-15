namespace Kitchen_Wizard;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(Views.ViewRecipePage), typeof(Views.ViewRecipePage));

    }
}
