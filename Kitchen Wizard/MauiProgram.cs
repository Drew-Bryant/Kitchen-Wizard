﻿using Kitchen_Wizard.Data_Objects;
using Kitchen_Wizard.Data_Objects.Database_Helpers;
using Kitchen_Wizard.Data_Objects.Interfaces;
using Kitchen_Wizard.Data_Objects.Static_Helpers;
using Kitchen_Wizard.Models;
using Kitchen_Wizard.Views;
using Kitchen_Wizard.Views.Embedded_Views;
using Plugin.LocalNotification;
using Sharpnado.Tabs;

namespace Kitchen_Wizard;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseLocalNotification()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//register dependency injection services
        //builder.Services.AddTransient<IRecipeHelper, RecipeDBHelper>();
        //builder.Services.AddTransient<ISearchHelper, SearchDBHelper>();
        builder.Services.AddTransient<IUserPreferences, UserPreferences>();


		//static helpers(for now)
        builder.Services.AddTransient<ISearchHelper, StaticSearchHelper>();
        builder.Services.AddTransient<IRecipeHelper, StaticRecipeHelper>();

        //register views for dependency injection
        builder.Services.AddTransient<RecipeSearchPage>();
		builder.Services.AddTransient<ViewRecipePage>();
        builder.Services.AddTransient<FavoritesAndHistoryPage>();
        builder.Services.AddTransient<FoodListPage>();
        builder.Services.AddTransient<DatabaseManagement>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<MainPage>();

        //embedded views
        builder.Services.AddTransient<FavoritesTab>();
        builder.Services.AddTransient<HistoryTab>();
        builder.Services.AddTransient<IngredientsTab>();
        builder.Services.AddTransient<SearchOptionsMenu>();
        builder.Services.AddTransient<FoodListSearchPage>();

        //register viewmodels for data binding
        builder.Services.AddSingleton<RecipeSearchPageModel>(); //Singleton to try and maintain state of UserPrefs between search page and options
		builder.Services.AddSingleton<ViewRecipePageModel>();
        builder.Services.AddTransient<FavoritesAndHistoryPageModel>();
        builder.Services.AddTransient<DatabaseManagementPageModel>();
        builder.Services.AddTransient<SettingsPageModel>();


        builder.Services.AddSingleton<FoodListPageModel>();

        return builder.Build();
	}
}
