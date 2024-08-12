using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Authentication_and_Authorization.Data;
using Authentication_and_Authorization.Areas.Identity.Data;
using Microsoft.CodeAnalysis.Options;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Authentication_and_AuthorizationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'Authentication_and_AuthorizationDbContextConnection' not found.");

builder.Services.AddDbContext<Authentication_and_AuthorizationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Authentication_and_AuthorizationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
}
);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
