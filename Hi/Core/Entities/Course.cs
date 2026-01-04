namespace ZarqaPortal.Web.Core.Entities;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Represents a course offered by the university.
/// </summary>
public class Course
{
    /// <summary>
    /// The unique identifier for the course.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the course.
    /// </summary>
    [Required(ErrorMessage = "Course name is required")]
    [MaxLength(200)]
    [Display(Name = "Course Name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the course content.
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// The name of the instructor teaching this course.
    /// </summary>
    [Required(ErrorMessage = "Instructor name is required")]
    [MaxLength(150)]
    [Display(Name = "Instructor")]
    public string InstructorName { get; set; } = string.Empty;

    /// <summary>
    /// The number of credit hours for this course.
    /// </summary>
    [Range(1, 6)]
    [Display(Name = "Credit Hours")]
    public int CreditHours { get; set; } = 3;
}
