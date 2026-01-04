namespace ZarqaPortal.Web.Features.Courses.Services;

using ZarqaPortal.Web.Core.Entities;

/// <summary>
/// Service interface for course-related operations.
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Gets all courses.
    /// </summary>
    Task<IEnumerable<Course>> GetAllCoursesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific course by ID.
    /// </summary>
    Task<Course?> GetCourseByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new course.
    /// </summary>
    Task<Course> CreateCourseAsync(Course course, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing course.
    /// </summary>
    Task UpdateCourseAsync(Course course, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a course by ID.
    /// </summary>
    Task DeleteCourseAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a course exists.
    /// </summary>
    Task<bool> CourseExistsAsync(int id, CancellationToken cancellationToken = default);
}
