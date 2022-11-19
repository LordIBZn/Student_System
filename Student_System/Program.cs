using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student_System.Data;
using Microsoft.AspNetCore.Identity;
using Student_System.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Student_SystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Student_SystemContext") ?? throw new InvalidOperationException("Connection string 'Student_SystemContext' not found.")));

builder.Services.AddDefaultIdentity<Student>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Student_SystemContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

//Se crea el alcance para el SeedData
app.Services.CreateScope();

//SeeData
using (var Scope = app.Services.CreateScope()) { 
    var Contex = Scope.ServiceProvider.GetRequiredService<Student_SystemContext>();
    DbInitializer.Initialize(Contex);
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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();
