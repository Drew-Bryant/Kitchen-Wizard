using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views;

namespace Kitchen_Wizard;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//register dependency injection services
        builder.Services.AddTransient<IFavoritesHelper, FavoritesDBHelper>();
        builder.Services.AddTransient<IFoodListHelper, FoodListDBHelper>();
        builder.Services.AddTransient<IHistoryHelper, HistoryDBHelper>();
        builder.Services.AddTransient<IRecipeHelper, RecipeDBHelper>();
        builder.Services.AddTransient<ISearchHelper, StaticSearchHelper>();
        builder.Services.AddTransient<IUserPreferences, UserPreferences>();

		//register views for dependency injection
        builder.Services.AddSingleton<RecipeSearchPage>();

		//register viewmodels for data binding
		builder.Services.AddSingleton<RecipeSearchPageModel>();


        return builder.Build();
	}
}
