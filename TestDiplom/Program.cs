using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TestDiplom.Areas.AdminPanel.Data;
using TestDiplom.Areas.Identity.Data;
using TestDiplom.Controllers;
using TestDiplom.Models;
using static TestDiplom.Controllers.HomeController;
using Message = TestDiplom.Models.Message;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TestAuthConnection") ?? throw new InvalidOperationException("Connection string 'TestAuthConnection' not found.");

builder.Services.AddDbContext<TestAuth>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TestAuth>();

builder.Services.AddDbContext<ReaderBook>(options =>
    options.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=10;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True").EnableSensitiveDataLogging());

builder.Services.AddDbContext<ShopCarti>(options =>
    options.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=10;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True").EnableSensitiveDataLogging());

builder.Services.AddDbContext<TeleMessages>(options =>
    options.UseSqlServer("Data Source=LAPTOP-PM4SK01C;Initial Catalog=Test4;Integrated Security=True;Connect Timeout=10;Encrypt=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;TrustServerCertificate=True;Trusted_Connection=True").EnableSensitiveDataLogging());


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=StartPage}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calendar}/{action=Maper}/{id?}");
app.MapControllerRoute(
    name: "AdminPanel",
    pattern: "{area:exists}/{controller=Corzina}/{action=CorzinaBook}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Roles}/{action=UserRole}/{id?}");
    
    
    
});



app.UseStaticFiles();
app.MapRazorPages();

string fileName = "updates.json";
List<BotUpdate> botUpdates = new List<BotUpdate>();
var botUpdatesString = System.IO.File.ReadAllText(fileName);

botUpdates = JsonConvert.DeserializeObject<List<BotUpdate>>(botUpdatesString) ?? botUpdates;
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = new UpdateType[]
    {
                                    UpdateType.Message,
                                    UpdateType.EditedMessage,
    }
};
var bot = new Telegram.Bot.TelegramBotClient("6002571377:AAEJdjdOsCSqTRXjHhmwc4-8VOGjBMs3Lw4");
bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);
app.Run();


