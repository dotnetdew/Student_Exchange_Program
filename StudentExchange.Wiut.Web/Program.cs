using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using StudentExchange.Wiut.Web.Data;
using StudentExchange.Wiut.Web.Models;
using StudentExchange.Wiut.Web.Repositories;
using StudentExchange.Wiut.Web.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Email Service config
builder.Services.AddTransient<IEmailSender, MailRuEmailSender>(provider =>
    new MailRuEmailSender("exchange@wiut.uz", "2873KLMS365"));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDefaultIdentity<Student>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

//adding admin role 
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Student>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await DataSeeder.SeedAsync(context, userManager, roleManager);
}

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "Admin/{action=Index}/{id?}",
        defaults: new { controller = "Admin" });
});

app.MapRazorPages();

app.Run();
