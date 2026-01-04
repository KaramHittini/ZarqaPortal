namespace ZarqaPortal.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using ZarqaPortal.Web.Core.Entities;
using ZarqaPortal.Web.Features.Authentication.Services;
using ZarqaPortal.Web.Features.Students.Services;

/// <summary>
/// Controller for managing student profiles.
/// </summary>
public class StudentProfileController : Controller
{
    private readonly IStudentProfileService _profileService;
    private readonly IAuthService _authService;
    private readonly ILogger<StudentProfileController> _logger;

    public StudentProfileController(
        IStudentProfileService profileService, 
        IAuthService authService,
        ILogger<StudentProfileController> logger)
    {
        _profileService = profileService;
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Displays the student profile for the logged-in user.
    /// </summary>
    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("Username");
        var role = HttpContext.Session.GetString("Role");

        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Login", "Auth");
        }

        // Admin shouldn't access student profile
        if (role == "Admin")
        {
            return RedirectToAction("Index", "Courses");
        }

        // Get the student profile from AuthService
        var profileData = _authService.GetStudentProfile(username);

        if (profileData is null)
        {
            _logger.LogWarning("No profile found for user {Username}", username);
            return NotFound();
        }

        var profile = new StudentProfile
        {
            FullName = profileData.Value.FullName,
            StudentId = profileData.Value.StudentId,
            Major = profileData.Value.Major,
            College = profileData.Value.College,
            Gpa = profileData.Value.Gpa,
            CompletedHours = profileData.Value.CompletedHours
        };

        return View(profile);
    }

    /// <summary>
    /// Displays the edit profile form.
    /// </summary>
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var profile = await _profileService.GetProfileByUserIdAsync(id, cancellationToken);
        if (profile is null)
        {
            return NotFound();
        }
        return View(profile);
    }

    /// <summary>
    /// Handles profile updates.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(StudentProfile profile, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(profile);
        }

        await _profileService.SaveProfileAsync(profile, cancellationToken);
        _logger.LogInformation("Updated profile for student {StudentId}", profile.StudentId);
        return RedirectToAction(nameof(Index));
    }
}
