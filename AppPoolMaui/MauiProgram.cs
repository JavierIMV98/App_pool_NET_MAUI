using AppPoolMaui.Repos;
using Microsoft.Extensions.Logging;

namespace AppPoolMaui;

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
        string dbPath = FileAccessHelper.GetLocalFilePath("basededatos.db3");
        builder.Services.AddSingleton<MesaRepository>(s => ActivatorUtilities.
			CreateInstance<MesaRepository>(s, dbPath));
        builder.Services.AddSingleton<ProductoRepository>(s => ActivatorUtilities.
            CreateInstance<ProductoRepository>(s, dbPath));
		builder.Services.AddSingleton<OrdenRepository>(s => ActivatorUtilities.
			CreateInstance<OrdenRepository>(s, dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
