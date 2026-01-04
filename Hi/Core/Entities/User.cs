namespace ZarqaPortal.Web.Core.Entities;

using System.ComponentModel.DataAnnotations;
using ZarqaPortal.Web.Core.Enums;

/// <summary>
/// Represents a user in the university portal system.
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's login username.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The hashed password for the user.
    /// </summary>
    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// The role assigned to this user.
    /// </summary>
    public UserRole Role { get; set; } = UserRole.Student;

    /// <summary>
    /// When the user account was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
