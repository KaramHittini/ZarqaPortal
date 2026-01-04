namespace ZarqaPortal.Web.Features.Authentication.Services;

/// <summary>
/// Service interface for authentication operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Validates user credentials and returns user info if valid.
    /// </summary>
    /// <param name="username">The username to validate.</param>
    /// <param name="password">The password to validate.</param>
    /// <returns>User info tuple (IsValid, Role, Username) or null if invalid.</returns>
    (bool IsValid, string Role, string Username)? ValidateCredentials(string username, string password);

    /// <summary>
    /// Gets the profile data for a student by username.
    /// </summary>
    (string FullName, string StudentId, string Major, string College, double Gpa, int CompletedHours)? GetStudentProfile(string username);
}
