using Frontend.Web.CrossCutting.Settings;
using Frontend.Web.Services.BaseServices;
using Frontend.Web.Services.CouponServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddTransient<IBaseService, BaseService>();
builder.Services.Configure<ServicesUrl>(builder.Configuration.GetSection("ServicesUrl"));
builder.Services.AddTransient<ICouponService, CouponService>();

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
