using Microsoft.EntityFrameworkCore;
using ZarqaPortal.Web.Features.Authentication.Services;
using ZarqaPortal.Web.Features.Courses.Services;
using ZarqaPortal.Web.Features.Schedule.Services;
using ZarqaPortal.Web.Features.Students.Services;
using ZarqaPortal.Web.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Database configuration
builder.Services.AddDbContext<ZarqaPortalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentProfileService, StudentProfileService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

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

app.UseSession();
app.UseAuthorization();

// Route for landing page (root) - must be first
app.MapControllerRoute(
    name: "landing",
    pattern: "",
    defaults: new { controller = "Home", action = "Welcome" });

// Default route for other pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Welcome}/{id?}");

app.Run();
