using Microsoft.Extensions.Logging;

namespace print_html_dotnet_maui_using_native_apis;

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

#if ANDROID
        builder.Services.AddTransient<IPrintService, print_html_dotnet_maui_using_native_apis.Platforms.Android.PrintService>();
#elif IOS
        builder.Services.AddTransient<IPrintService, print_html_dotnet_maui_using_native_apis.Platforms.iOS.PrintService>();
#endif

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
