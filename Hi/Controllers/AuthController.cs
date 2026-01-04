namespace ZarqaPortal.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using ZarqaPortal.Web.Features.Authentication.Services;
using ZarqaPortal.Web.Features.Authentication.ViewModels;

/// <summary>
/// Controller for authentication (login/logout).
/// </summary>
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Displays the login page.
    /// </summary>
    [HttpGet]
    public IActionResult Login()
    {
        // If already logged in, redirect appropriately
        var username = HttpContext.Session.GetString("Username");
        if (!string.IsNullOrEmpty(username))
        {
            var role = HttpContext.Session.GetString("Role");
            return RedirectBasedOnRole(role);
        }

        return View(new LoginViewModel());
    }

    /// <summary>
    /// Handles login form submission.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = _authService.ValidateCredentials(model.Username, model.Password);

        if (result is null)
        {
            _logger.LogWarning("Failed login attempt for username: {Username}", model.Username);
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        // Store user info in session
        HttpContext.Session.SetString("Username", result.Value.Username);
        HttpContext.Session.SetString("Role", result.Value.Role);

        _logger.LogInformation("User {Username} logged in successfully with role {Role}", 
            result.Value.Username, result.Value.Role);

        return RedirectBasedOnRole(result.Value.Role);
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    public IActionResult Logout()
    {
        var username = HttpContext.Session.GetString("Username");
        HttpContext.Session.Clear();
        _logger.LogInformation("User {Username} logged out", username);
        return RedirectToAction("Welcome", "Home");
    }

    private IActionResult RedirectBasedOnRole(string? role)
    {
        if (role == "Admin")
        {
            return RedirectToAction("AdminProfile", "Home");
        }
        else
        {
            return RedirectToAction("Index", "StudentProfile");
        }
    }
}
