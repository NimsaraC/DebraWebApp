using DebraWebApp.Models;
using DebraWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<PartnerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44317/");
});

builder.Services.AddHttpClient<TicketService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44317/");
});
builder.Services.AddHttpClient<EventService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44317/");
});
builder.Services.AddHttpClient<SellService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44317/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
