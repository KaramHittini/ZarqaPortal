namespace ZarqaPortal.Web.Features.Students.Services;

using ZarqaPortal.Web.Core.Entities;

/// <summary>
/// Service interface for student profile operations.
/// </summary>
public interface IStudentProfileService
{
    /// <summary>
    /// Gets a student profile by user ID.
    /// </summary>
    Task<StudentProfile?> GetProfileByUserIdAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a student profile by student ID.
    /// </summary>
    Task<StudentProfile?> GetProfileByStudentIdAsync(string studentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates or updates a student profile.
    /// </summary>
    Task<StudentProfile> SaveProfileAsync(StudentProfile profile, CancellationToken cancellationToken = default);
}
