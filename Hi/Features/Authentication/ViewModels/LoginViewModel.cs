namespace ZarqaPortal.Web.Features.Authentication.ViewModels;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// View model for user login.
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// The username for login.
    /// </summary>
    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password for login.
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Whether to remember the user's login.
    /// </summary>
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}
