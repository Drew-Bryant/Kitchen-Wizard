using Kitchen_Wizard.Views;
using Kitchen_Wizard.Views.Embedded_Views;

namespace Kitchen_Wizard;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(ViewRecipePage), typeof(ViewRecipePage));
        Routing.RegisterRoute(nameof(SearchOptionsMenu), typeof(SearchOptionsMenu));


    }
}
