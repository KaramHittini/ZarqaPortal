using Microsoft.EntityFrameworkCore;
using ZarqaPortal.Web.Features.Courses.Services;
using ZarqaPortal.Web.Features.Students.Services;
using ZarqaPortal.Web.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database configuration
builder.Services.AddDbContext<ZarqaPortalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentProfileService, StudentProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Special route for landing page (root)
app.MapControllerRoute(
    name: "landing",
    pattern: "",
    defaults: new { controller = "Home", action = "Welcome" });

app.Run();
