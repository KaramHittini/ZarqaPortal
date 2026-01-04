namespace ZarqaPortal.Web.Core.Enums;

/// <summary>
/// Defines the roles available in the system.
/// </summary>
public enum UserRole
{
    /// <summary>
    /// A student user with limited access.
    /// </summary>
    Student,

    /// <summary>
    /// An instructor who can manage their courses.
    /// </summary>
    Instructor,

    /// <summary>
    /// An administrator with full system access.
    /// </summary>
    Admin
}
