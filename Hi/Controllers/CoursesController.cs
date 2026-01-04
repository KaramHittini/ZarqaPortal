namespace ZarqaPortal.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using ZarqaPortal.Web.Core.Entities;
using ZarqaPortal.Web.Features.Courses.Services;

/// <summary>
/// Controller for managing courses in the university portal.
/// </summary>
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(ICourseService courseService, ILogger<CoursesController> logger)
    {
        _courseService = courseService;
        _logger = logger;
    }

    /// <summary>
    /// Checks if current user is an admin.
    /// </summary>
    private bool IsAdmin => HttpContext.Session.GetString("Role") == "Admin";

    /// <summary>
    /// Checks if current user is logged in.
    /// </summary>
    private bool IsLoggedIn => !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));

    /// <summary>
    /// Displays the list of all courses.
    /// </summary>
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var courses = await _courseService.GetAllCoursesAsync(cancellationToken);
        ViewBag.IsAdmin = IsAdmin;
        ViewBag.IsLoggedIn = IsLoggedIn;
        ViewBag.Username = HttpContext.Session.GetString("Username");
        return View(courses);
    }

    /// <summary>
    /// Displays course details.
    /// </summary>
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var course = await _courseService.GetCourseByIdAsync(id, cancellationToken);
        if (course is null)
        {
            _logger.LogWarning("Course with ID {CourseId} not found", id);
            return NotFound();
        }
        ViewBag.IsAdmin = IsAdmin;
        return View(course);
    }

    /// <summary>
    /// Displays the create course form (Admin only).
    /// </summary>
    public IActionResult Create()
    {
        if (!IsAdmin)
        {
            _logger.LogWarning("Non-admin user attempted to access Create course");
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    /// <summary>
    /// Handles course creation (Admin only).
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course, CancellationToken cancellationToken)
    {
        if (!IsAdmin)
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            return View(course);
        }

        await _courseService.CreateCourseAsync(course, cancellationToken);
        _logger.LogInformation("Course '{CourseName}' created successfully", course.Name);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the edit course form (Admin only).
    /// </summary>
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        if (!IsAdmin)
        {
            _logger.LogWarning("Non-admin user attempted to access Edit course");
            return RedirectToAction(nameof(Index));
        }

        var course = await _courseService.GetCourseByIdAsync(id, cancellationToken);
        if (course is null)
        {
            _logger.LogWarning("Course with ID {CourseId} not found for edit", id);
            return NotFound();
        }
        return View(course);
    }

    /// <summary>
    /// Handles course updates (Admin only).
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course, CancellationToken cancellationToken)
    {
        if (!IsAdmin)
        {
            return Forbid();
        }

        if (id != course.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(course);
        }

        if (!await _courseService.CourseExistsAsync(id, cancellationToken))
        {
            return NotFound();
        }

        await _courseService.UpdateCourseAsync(course, cancellationToken);
        _logger.LogInformation("Course with ID {CourseId} updated", course.Id);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the delete confirmation page (Admin only).
    /// </summary>
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        if (!IsAdmin)
        {
            _logger.LogWarning("Non-admin user attempted to access Delete course");
            return RedirectToAction(nameof(Index));
        }

        var course = await _courseService.GetCourseByIdAsync(id, cancellationToken);
        if (course is null)
        {
            _logger.LogWarning("Course with ID {CourseId} not found for delete", id);
            return NotFound();
        }
        return View(course);
    }

    /// <summary>
    /// Handles course deletion (Admin only).
    /// </summary>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        if (!IsAdmin)
        {
            return Forbid();
        }

        await _courseService.DeleteCourseAsync(id, cancellationToken);
        _logger.LogInformation("Course with ID {CourseId} deleted", id);
        return RedirectToAction(nameof(Index));
    }
}
