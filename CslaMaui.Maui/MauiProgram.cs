using Csla;
using Csla.Configuration;
using Csla.Blazor.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Csla.Blazor;

namespace CslaMaui.Maui
{
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CslaAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CslaAuthenticationStateProvider>());

            builder.Services.AddTransient<HttpClient>();
            builder.Services.AddCsla(o => o
            .AddBlazorWebAssembly()
                .DataPortal(dpo => dpo
                    .UseHttpProxy(options => options.DataPortalUrl = "https://localhost:7078/api/DataPortal")));

            return builder.Build();
        }
    }
}