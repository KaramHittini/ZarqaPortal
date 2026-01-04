namespace ZarqaPortal.Web.Shared.ViewModels;

/// <summary>
/// View model for error pages.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// The request ID for tracking the error.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Indicates whether the request ID should be displayed.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
