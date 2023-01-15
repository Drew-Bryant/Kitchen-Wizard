using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Data_Objects.Static_Helpers;
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
        //builder.Services.AddTransient<IRecipeHelper, RecipeDBHelper>();
        //builder.Services.AddTransient<ISearchHelper, SearchDBHelper>();
        builder.Services.AddTransient<IUserPreferences, UserPreferences>();


		//static helpers(for now)
        builder.Services.AddTransient<ISearchHelper, StaticSearchHelper>();
        builder.Services.AddTransient<IRecipeHelper, StaticRecipeHelper>();




        //NOTE: SINGLETON MAY NOT BE THE RIGHT THING FOR VIEWS AND MODELS

        //register views for dependency injection
        builder.Services.AddTransient<RecipeSearchPage>();
		builder.Services.AddSingleton<ViewRecipePage>();

		//register viewmodels for data binding
		builder.Services.AddTransient<RecipeSearchPageModel>();
		builder.Services.AddSingleton<ViewRecipePageModel>();


        return builder.Build();
	}
}
