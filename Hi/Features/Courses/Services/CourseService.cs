namespace ZarqaPortal.Web.Features.Courses.Services;

using Microsoft.EntityFrameworkCore;
using ZarqaPortal.Web.Core.Entities;
using ZarqaPortal.Web.Infrastructure.Data;

/// <summary>
/// Implementation of course-related business logic.
/// </summary>
public class CourseService : ICourseService
{
    private readonly ZarqaPortalDbContext _dbContext;
    private readonly ILogger<CourseService> _logger;

    public CourseService(ZarqaPortalDbContext dbContext, ILogger<CourseService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Course>> GetAllCoursesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching all courses");
        return await _dbContext.Courses
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Course?> GetCourseByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching course with ID {CourseId}", id);
        return await _dbContext.Courses.FindAsync(new object[] { id }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Course> CreateCourseAsync(Course course, CancellationToken cancellationToken = default)
    {
        _dbContext.Courses.Add(course);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Created course {CourseName} with ID {CourseId}", course.Name, course.Id);
        return course;
    }

    /// <inheritdoc/>
    public async Task UpdateCourseAsync(Course course, CancellationToken cancellationToken = default)
    {
        _dbContext.Courses.Update(course);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Updated course with ID {CourseId}", course.Id);
    }

    /// <inheritdoc/>
    public async Task DeleteCourseAsync(int id, CancellationToken cancellationToken = default)
    {
        var course = await GetCourseByIdAsync(id, cancellationToken);
        if (course is not null)
        {
            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Deleted course with ID {CourseId}", id);
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent course with ID {CourseId}", id);
        }
    }

    /// <inheritdoc/>
    public async Task<bool> CourseExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Courses.AnyAsync(c => c.Id == id, cancellationToken);
    }
}
