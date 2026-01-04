namespace ZarqaPortal.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using ZarqaPortal.Web.Core.Entities;
using ZarqaPortal.Web.Features.Students.Services;

/// <summary>
/// Controller for managing student profiles.
/// </summary>
public class StudentProfileController : Controller
{
    private readonly IStudentProfileService _profileService;
    private readonly ILogger<StudentProfileController> _logger;

    public StudentProfileController(IStudentProfileService profileService, ILogger<StudentProfileController> logger)
    {
        _profileService = profileService;
        _logger = logger;
    }

    /// <summary>
    /// Displays the student profile.
    /// </summary>
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        // TODO: Get user ID from authentication claims
        // For now, using a demo profile
        var profile = new StudentProfile
        {
            FullName = "Mustafa Alhamad",
            StudentId = "202301969",
            Major = "Cyber Security",
            College = "IT",
            Gpa = 3.35,
            CompletedHours = 95
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
