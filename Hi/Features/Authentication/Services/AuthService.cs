namespace ZarqaPortal.Web.Features.Authentication.Services;

/// <summary>
/// Implementation of authentication service with hardcoded users for demo.
/// </summary>
public class AuthService : IAuthService
{
    // Hardcoded users for demonstration purposes
    private static readonly Dictionary<string, (string Password, string Role)> _users = new()
    {
        { "Admin", ("123", "Admin") },
        { "Karam Hittini", ("2569", "Student") },
        { "Mustafa Alhamad", ("1969", "Student") }
    };

    // Hardcoded student profiles
    private static readonly Dictionary<string, (string FullName, string StudentId, string Major, string College, double Gpa, int CompletedHours)> _studentProfiles = new()
    {
        { "Karam Hittini", ("Karam Hittini", "20230569", "Computer Science", "IT", 3.50, 95) },
        { "Mustafa Alhamad", ("Mustafa Alhamad", "202301969", "Cyber Security", "IT", 3.35, 95) }
    };

    /// <inheritdoc/>
    public (bool IsValid, string Role, string Username)? ValidateCredentials(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        if (_users.TryGetValue(username, out var userData) && userData.Password == password)
        {
            return (true, userData.Role, username);
        }

        return null;
    }

    /// <inheritdoc/>
    public (string FullName, string StudentId, string Major, string College, double Gpa, int CompletedHours)? GetStudentProfile(string username)
    {
        if (_studentProfiles.TryGetValue(username, out var profile))
        {
            return profile;
        }

        return null;
    }
}
