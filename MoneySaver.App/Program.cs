using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using MoneySaver.App.Services;

namespace MoneySaver.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("api")
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                    .ConfigureHandler(
                        authorizedUrls: new[] { "https://localhost:6001" },
                        scopes: new[] { "manage" }
                        );

<<<<<<< Updated upstream
                    return handler;
                });
            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));
=======
<<<<<<< Updated upstream
=======
                    return handler;
                });
            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));
            builder.Services.AddScoped<ITransactionService, TransactionService>();
>>>>>>> Stashed changes
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });
            
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
>>>>>>> Stashed changes
            await builder.Build().RunAsync();
        }
    }
}
