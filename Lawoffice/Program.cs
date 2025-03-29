
using Lawoffice.Models.LawOfficeModels;
using Lawoffice.Services.AccountService;
using Lawoffice.Services.CaseService;
using Lawoffice.Services.ProcedureService;
using Lawoffice.Services.SessionService;
using Lawoffice.Services.TypeService;
using Lawoffice.Services.UserService;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
 
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "admin";
})
.AddCookie("admin", options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Home/Error";
});

var cultures = new CultureInfo[]
     {
       new CultureInfo("en-US"), // English (United States)
       new CultureInfo("ar-EG"), // Arabic (Egypt)
     };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("ar-EG");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
});
builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1); // You can set the session timeout here.
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var LawOfficeConnectionString = builder.Configuration.GetConnectionString("LawOffice_Database");
builder.Services.AddDbContext<LawOfficeContext>(options => options.UseSqlServer(LawOfficeConnectionString));
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICaseService, CaseService>();
builder.Services.AddScoped<IProcedureService, ProcedureService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<ITypeService, TypeService>();


builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


// Configure the HTTP request pipeline. 
if (!app.Environment.IsDevelopment())
{
    // Redirect to '/' on exceptions
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            // Redirect to home page on any exception
            context.Response.Redirect("/");
        });
    });

    app.UseHsts();
}
else
{
    // Use Developer exception page in development mode
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseRequestLocalization(option =>
{
    option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ar-EG");
    option.SupportedCultures = cultures;
    option.SupportedUICultures = cultures;
    option.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
});
// Ensure authentication middleware is used before custom middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    // Default routes
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Cases}/{action=Index}/{id?}");
});

app.Run();
