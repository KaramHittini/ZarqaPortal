namespace ZarqaPortal.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using ZarqaPortal.Web.Features.Schedule.Services;

/// <summary>
/// Controller for managing student course schedules.
/// </summary>
public class ScheduleController : Controller
{
    private readonly IScheduleService _scheduleService;
    private readonly ILogger<ScheduleController> _logger;

    public ScheduleController(IScheduleService scheduleService, ILogger<ScheduleController> logger)
    {
        _scheduleService = scheduleService;
        _logger = logger;
    }

    /// <summary>
    /// Displays the student's schedule.
    /// </summary>
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var username = HttpContext.Session.GetString("Username");
        var role = HttpContext.Session.GetString("Role");

        if (string.IsNullOrEmpty(username) || role == "Admin")
        {
            return RedirectToAction("Login", "Auth");
        }

        var schedule = await _scheduleService.GetScheduleForUserAsync(username, cancellationToken);
        ViewBag.StudentName = username;
        return View(schedule);
    }

    /// <summary>
    /// Adds a course to the student's schedule.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCourse(int courseId, CancellationToken cancellationToken)
    {
        var username = HttpContext.Session.GetString("Username");
        var role = HttpContext.Session.GetString("Role");

        if (string.IsNullOrEmpty(username) || role == "Admin")
        {
            return RedirectToAction("Login", "Auth");
        }

        var success = await _scheduleService.AddCourseToScheduleAsync(username, courseId, cancellationToken);

        if (success)
        {
            TempData["SuccessMessage"] = "Course added to your schedule!";
        }
        else
        {
            TempData["ErrorMessage"] = "Course is already in your schedule or does not exist.";
        }

        return RedirectToAction("Index", "Courses");
    }

    /// <summary>
    /// Removes a course from the student's schedule.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveCourse(int courseId, CancellationToken cancellationToken)
    {
        var username = HttpContext.Session.GetString("Username");

        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Auth");
        }

        await _scheduleService.RemoveCourseFromScheduleAsync(username, courseId, cancellationToken);
        TempData["SuccessMessage"] = "Course removed from your schedule.";

        return RedirectToAction(nameof(Index));
    }
}
