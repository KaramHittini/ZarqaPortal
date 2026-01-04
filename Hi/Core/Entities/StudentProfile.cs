namespace ZarqaPortal.Web.Core.Entities;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a student's academic profile.
/// </summary>
public class StudentProfile
{
    /// <summary>
    /// The unique identifier for this profile.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The student's user ID (foreign key).
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// The student's full name.
    /// </summary>
    [Required]
    [MaxLength(150)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// The student's university ID number.
    /// </summary>
    [Required]
    [MaxLength(20)]
    [Display(Name = "Student ID")]
    public string StudentId { get; set; } = string.Empty;

    /// <summary>
    /// The student's major field of study.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Major { get; set; } = string.Empty;

    /// <summary>
    /// The college the student belongs to.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string College { get; set; } = string.Empty;

    /// <summary>
    /// The student's grade point average.
    /// </summary>
    [Range(0.0, 4.0)]
    [Display(Name = "GPA")]
    public double Gpa { get; set; }

    /// <summary>
    /// Total credit hours completed by the student.
    /// </summary>
    [Range(0, 200)]
    [Display(Name = "Completed Hours")]
    public int CompletedHours { get; set; }

    /// <summary>
    /// Navigation property to the associated user.
    /// </summary>
    public User? User { get; set; }
}
