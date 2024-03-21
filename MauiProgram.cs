using Microsoft.Extensions.Logging;
using PanCardView;

namespace EventyMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseCardsView()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("JosefinSans-Light.ttf", "JosefinSansLight");
                    fonts.AddFont("JosefinSans-Regular.ttf", "JosefinSansRegular");
                    fonts.AddFont("JosefinSans-SemiBold.ttf", "JosefinSansSemiBold");
                    fonts.AddFont("JosefinSans-Bold.ttf", "JosefinSansBold");
                    fonts.AddFont("JosefinSans-BoldItalic.ttf", "JosefinSansBoldItalic");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "IconFontTypes");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
