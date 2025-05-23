using DA.Kochbuch.Blazor.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("DA.Kochbuch.Blazor.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

string graphQLServerPath = builder.HostEnvironment.BaseAddress + "graphql";
builder.Services.AddKochbuchClient().ConfigureHttpClient(client =>
{
	client.BaseAddress = new Uri(graphQLServerPath);
});

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DA.Kochbuch.Blazor.ServerAPI"));

await builder.Build().RunAsync();
