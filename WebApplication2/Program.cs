using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using WebApplication2.ExtendMethods;
using WebApplication2.Models;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);

// Use SQL Server
var connectString = builder.Configuration.GetConnectionString("AppMvcConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    // Default -- View/Controller/Action.cshtml
    // New link

    // {0} -- Action
    // {1} -- Controller
    // {2} -- Area
    // Mặc định, giá trị của RazorViewEngine.ViewExtension là ".cshtml"
    options.ViewLocationFormats.Add("/MyView/{1}/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddSingleton<PlanetService>();
/// services.AddSingleton<TService, TImplementation>();
builder.Services.AddSingleton(typeof(ProductService), typeof(ProductService));
//builder.Services.AddSingleton<ProductService, ProductService>();

//builder.Services.AddSingleton(typeof(ProductService));
//builder.Services.AddSingleton<ProductService>();

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

// ExtendMethods/AppExtend.cs
app.AddStatusCodePage();

app.UseRouting();

app.UseAuthorization();

// Controller không có Area
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Cài đặt Route cho Areas
app.MapAreaControllerRoute(
    name:"product",
    pattern: "/{controller=Home}/{action=Index}/{id?}",
    areaName: "ProductManage");

app.MapControllerRoute(
    name: "test",
    pattern: "{url:regex(^((viewproduct)|(seeproduct))$)}/{id:range(1,5)}",
    defaults: new
    {
        controller = "Test",
        action = "ViewProduct"
    }
    //// IRouteConstraint - Điều kiện bắt buộc Route
    //constraints: new
    //{
    //    url = new RegexRouteConstraint(@"^((viewproduct)|(seeproduct))$"),
    //    id = new RangeRouteConstraint(1, 5)
    //}
);

// Endpoint trả về các Razor Page
app.MapRazorPages();

// Nhận 1 HTTP GET request và trả về 1 hàm
app.MapGet("/sayhi", async (context) =>
{
    await context.Response.WriteAsync($"Today is {DateTime.Now}");
});

app.Run();
