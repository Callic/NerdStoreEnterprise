using NSE.WebApp.MVC.Configuration;
using NSE.WebApp.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityConfiguration();
builder.Services.AddDependencyInjection();

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddControllersWithViews()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });


var app = builder.Build();

app.UseExceptionHandler("/erro/500");
app.UseStatusCodePagesWithRedirects("/erro/{0}");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityConfiguration();
app.UseMiddleware<ExceptionMiddleware>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
