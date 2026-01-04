namespace ZarqaPortal.Web.Features.Schedule.Services;

using Microsoft.EntityFrameworkCore;
using ZarqaPortal.Web.Core.Entities;
using ZarqaPortal.Web.Infrastructure.Data;

/// <summary>
/// Implementation of schedule-related business logic.
/// </summary>
public class ScheduleService : IScheduleService
{
    private readonly ZarqaPortalDbContext _dbContext;
    private readonly ILogger<ScheduleService> _logger;

    public ScheduleService(ZarqaPortalDbContext dbContext, ILogger<ScheduleService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<StudentSchedule>> GetScheduleForUserAsync(string username, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching schedule for user {Username}", username);
        return await _dbContext.StudentSchedules
            .Include(s => s.Course)
            .Where(s => s.Username == username)
            .OrderBy(s => s.Course!.Name)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> AddCourseToScheduleAsync(string username, int courseId, CancellationToken cancellationToken = default)
    {
        // Check if already in schedule
        if (await IsCourseInScheduleAsync(username, courseId, cancellationToken))
        {
            _logger.LogWarning("Course {CourseId} is already in {Username}'s schedule", courseId, username);
            return false;
        }

        // Check if course exists
        var courseExists = await _dbContext.Courses.AnyAsync(c => c.Id == courseId, cancellationToken);
        if (!courseExists)
        {
            _logger.LogWarning("Course {CourseId} does not exist", courseId);
            return false;
        }

        var scheduleEntry = new StudentSchedule
        {
            Username = username,
            CourseId = courseId,
            AddedAt = DateTime.UtcNow
        };

        _dbContext.StudentSchedules.Add(scheduleEntry);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Added course {CourseId} to {Username}'s schedule", courseId, username);
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> IsCourseInScheduleAsync(string username, int courseId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.StudentSchedules
            .AnyAsync(s => s.Username == username && s.CourseId == courseId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveCourseFromScheduleAsync(string username, int courseId, CancellationToken cancellationToken = default)
    {
        var entry = await _dbContext.StudentSchedules
            .FirstOrDefaultAsync(s => s.Username == username && s.CourseId == courseId, cancellationToken);

        if (entry is null)
        {
            return false;
        }

        _dbContext.StudentSchedules.Remove(entry);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Removed course {CourseId} from {Username}'s schedule", courseId, username);
        return true;
    }
}
