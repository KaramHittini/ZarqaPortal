namespace ZarqaPortal.Web.Features.Students.Services;

using Microsoft.EntityFrameworkCore;
using ZarqaPortal.Web.Core.Entities;
using ZarqaPortal.Web.Infrastructure.Data;

/// <summary>
/// Implementation of student profile business logic.
/// </summary>
public class StudentProfileService : IStudentProfileService
{
    private readonly ZarqaPortalDbContext _dbContext;
    private readonly ILogger<StudentProfileService> _logger;

    public StudentProfileService(ZarqaPortalDbContext dbContext, ILogger<StudentProfileService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<StudentProfile?> GetProfileByUserIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching student profile for user ID {UserId}", userId);
        return await _dbContext.StudentProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<StudentProfile?> GetProfileByStudentIdAsync(string studentId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching student profile for student ID {StudentId}", studentId);
        return await _dbContext.StudentProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.StudentId == studentId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<StudentProfile> SaveProfileAsync(StudentProfile profile, CancellationToken cancellationToken = default)
    {
        if (profile.Id == 0)
        {
            _dbContext.StudentProfiles.Add(profile);
            _logger.LogInformation("Creating new student profile for student ID {StudentId}", profile.StudentId);
        }
        else
        {
            _dbContext.StudentProfiles.Update(profile);
            _logger.LogInformation("Updating student profile with ID {ProfileId}", profile.Id);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return profile;
    }
}
