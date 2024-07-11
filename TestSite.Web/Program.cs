using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSite.Domain;
using TestSite.Domain.Abstractions;
using TestSite.Infrastructure;
using TestSite.Infrastructure.EmailInfrastructure;
using TestSite.Infrastructure.Repositories;
using TestSite.Mappings.utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
	// options.Conventions.AuthorizePage("/CreateTest");
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
	{
		options.SignIn.RequireConfirmedAccount = true;
		options.User.RequireUniqueEmail = true;
	})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<ITestResultRepository, TestResultRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IEmailSender<User>, EmailSender>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(opt =>
	{
		opt.Cookie.Name = "AspNetCookies";
		opt.LogoutPath = "/Identity/Account/Logout";
		opt.LoginPath = "/Identity/Account/Login";
		opt.AccessDeniedPath = "/Identity/Account/AccessDenied";
		opt.SlidingExpiration = true;
	});

builder.Services.ConfigureApplicationCookie(
	opt =>
	{
		opt.Cookie.Name = "AspNetCookies";
		opt.LogoutPath = "/Identity/Account/Logout";
		opt.LoginPath = "/Identity/Account/Login";
		opt.AccessDeniedPath = "/Identity/Account/AccessDenied";
		opt.SlidingExpiration = true;
	});

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(AssemblyReference.Assembly);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.MapPost("AddTime", async (
	[FromServices] ITestResultRepository testResultRepository, 
	[FromServices] IUnitOfWork uow, 
	HttpContext  httpContext,
	[FromServices] IMapper mapper,
	[FromQuery] Guid testId
	) =>
{
	var userId = httpContext
		.User.Claims
		.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
		?.Value ?? throw new NullReferenceException();

	var testCompletionStatusResult = await testResultRepository.GetTestCompletionStatusAsync(new Guid(userId), testId);

	if (testCompletionStatusResult.IsFailure)
	{
		return Results.BadRequest(testCompletionStatusResult.Error);
	}

	var testCompletionStatus = testCompletionStatusResult.Value!;
	var incrementResult = testCompletionStatus.IncrementTime(new TimeSpan(0, 0, 30));
	var result = await testResultRepository.UpdateTestCompletionStatusAsync(testCompletionStatus);

	return Results.Ok(new { IsTimeOut = incrementResult.Value });
}).RequireAuthorization(); 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   
app.UseAuthorization();     

app.MapRazorPages();

await using var dataContext = app.Services.CreateScope().ServiceProvider.GetService<AppDbContext>()!;
await dataContext.Database.MigrateAsync();

app.Run();