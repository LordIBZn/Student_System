using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Student_System.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Student_SystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Student_SystemContext") ?? throw new InvalidOperationException("Connection string 'Student_SystemContext' not found.")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        option.AccessDeniedPath = "/Home/Privacy";
    });

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
    pattern: "{controller=Access}/{action=Index}/{id?}");


app.Run();
