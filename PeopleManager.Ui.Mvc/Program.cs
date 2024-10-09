using Microsoft.AspNetCore.Authentication.Cookies;
using PeopleManager.Sdk.Extensions;
using PeopleManager.Ui.Mvc.Handlers;
using PeopleManager.Ui.Mvc.Settings;
using PeopleManager.Ui.Mvc.Stores;
using Vives.Presentation.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var apiSettings = new ApiSettings();
builder.Configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);

//builder.Services.AddScoped<RedirectToSignInHandler>();
builder.Services.AddApi(apiSettings.BaseUrl);
	//.AddHander<RedirectToSignInHandler>;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Account/SignIn";
		options.LogoutPath = "/Account/Logout";
	});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IBearerTokenStore, BearerTokenStore>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
