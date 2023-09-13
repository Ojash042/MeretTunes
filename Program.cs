using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MeretTune.Data;
using MeretTune.Factory;
using MeretTune.Models;
using MeretTune.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DataServices>();
builder.Services.AddScoped<FileServices>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromDays(14);
	options.Cookie.Name = ".MeretTune.Sessions";
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});


var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));


builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders().AddDefaultUI();

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options => {
	options.Password.RequireNonAlphanumeric = false;
	options.User.RequireUniqueEmail = true;
	options.Password.RequiredLength = 5;
	options.Password.RequireDigit = false;
	options.Password.RequireUppercase = false;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-.";
});

builder.Services.ConfigureApplicationCookie(options => {
	options.ExpireTimeSpan = TimeSpan.FromDays(14);
	options.LoginPath = "/Login";
});

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options => {
	options.AddPolicy("EmailId",
		policy => policy.RequireClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
			"admin@admin.com"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();