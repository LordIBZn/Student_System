using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student_System.Data;
using Microsoft.AspNetCore.Identity;
using Student_System.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Student_System.Services;
using Student_System.Providers;
using Hangfire;
using Hangfire.SqlServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Student_SystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Student_SystemContext") ?? throw new InvalidOperationException("Connection string 'Student_SystemContext' not found.")));

builder.Services.AddDefaultIdentity<AspNetUsers>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Student_SystemContext>()
    .AddDefaultTokenProviders();

var conectionString = builder.Configuration.GetConnectionString("HangfireConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSingleton<PathProvider>();
//builder.Services.AddSingleton<UploadFilesHelper>();

builder.Services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
.UseSimpleAssemblyNameTypeSerializer()
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UseSqlServerStorage(conectionString, new SqlServerStorageOptions
{
    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
    QueuePollInterval = TimeSpan.Zero,
    UseRecommendedIsolationLevel = true,
    DisableGlobalLocks = true
}));

builder.Services.AddHangfireServer();

var app = builder.Build();

//Se crea el alcance para el SeedData
app.Services.CreateScope();

//SeeData
using (var Scope = app.Services.CreateScope())
{
    var roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var _userManager = Scope.ServiceProvider.GetRequiredService<UserManager<AspNetUsers>>();
    var Contex = Scope.ServiceProvider.GetRequiredService<Student_SystemContext>();
    DbInitializer.Initialize(Contex,_userManager,roleManager);
}

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

app.UseHangfireDashboard();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();