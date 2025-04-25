using DA.Kochbuch.Blazor.Server.DataAccess;
using DA.Kochbuch.Blazor.Server.GraphQL;
using DA.Kochbuch.Blazor.Server.Interfaces;
using DA.Kochbuch.Blazor.Server.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json");
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddPooledDbContextFactory<KochbuchContext>(opts =>
	opts.UseMySQL(builder.Configuration.GetConnectionString("default")!));
builder.Services.AddScoped<IKochbuch, KochbuchDataAccessLayer>();
builder.Services.AddGraphQLServer()
	.AddQueryType<KochbuchQueryResolver>()
	.AddMutationType<KochbuchMutationResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapGraphQL();
app.UseCors(policy =>
{
	policy.WithOrigins("https://localhost:7278")
		 .AllowAnyMethod()
		 .AllowAnyHeader()
		 .AllowCredentials();
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
