using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MoneySaver.App.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MoneySaver.App.Models.Configurations;

namespace MoneySaver.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(DataApi)).Get<DataApi>());
            var dataApiConf = new DataApi();
            builder.Configuration.Bind(nameof(DataApi), dataApiConf);
            builder.Services.AddSingleton(dataApiConf);
            builder.Services.AddHttpClient("api")
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                    .ConfigureHandler(
                        authorizedUrls: new[] { dataApiConf.Url },
                        scopes: dataApiConf.Scopes
                        );

                    return handler;
                });

            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("api"));
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IBudgetService, BudgetService>();
            builder.Services.AddScoped<IReportDataService, ReportsDataService>();
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });
            
            await builder.Build().RunAsync();
        }
    }
}
