using Microsoft.AspNetCore.Authentication.Cookies;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Task1_Marketplace;
using Task1_Marketplace.Configuration;
using Task1_Marketplace.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

var mongoConfigSection = builder.Configuration.GetSection("Database");
var pack = new ConventionPack();
pack.Add(new CamelCaseElementNameConvention());

ConventionRegistry.Register("Camel case convention", pack, t => true);
builder.Services.Configure<MongoDbConfiguration>(mongoConfigSection);
var mongoClient = new MongoClient(mongoConfigSection["ConnectionString"]);
SeedMongoDb.Seed(mongoClient, mongoConfigSection.Get<MongoDbConfiguration>());
builder.Services.AddSingleton(mongoClient.GetDatabase(mongoConfigSection["DatabaseName"]));

builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
