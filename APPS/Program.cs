using AppsServices.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


//Dependency Injection: appservices sýnýfýnýn baðýmlý olduðu ýappservice den baðýmsýz hareket edebilmesini saðlama
builder.Services.AddSingleton<IAPPSService, APPSServices>();

builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Home/Index";
        });

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
