namespace ZarqaPortal.Web.Infrastructure.Razor;

using Microsoft.AspNetCore.Mvc.Razor;

/// <summary>
/// Configures Razor to look for views in feature folders.
/// </summary>
public class FeatureViewLocationExpander : IViewLocationExpander
{
    /// <inheritdoc/>
    public void PopulateValues(ViewLocationExpanderContext context)
    {
        // No need to populate values for this expander
    }

    /// <inheritdoc/>
    public IEnumerable<string> ExpandViewLocations(
        ViewLocationExpanderContext context,
        IEnumerable<string> viewLocations)
    {
        // Add feature-based view locations
        var featureLocations = new[]
        {
            // Feature-based views
            "/Features/{1}/Views/{0}.cshtml",
            "/Features/{1}/Views/{1}/{0}.cshtml",
            
            // Shared views
            "/Shared/Views/{0}.cshtml",
            "/Shared/Views/Shared/{0}.cshtml",
            
            // Default views (fallback)
            "/Views/{1}/{0}.cshtml",
            "/Views/Shared/{0}.cshtml"
        };

        return featureLocations.Concat(viewLocations);
    }
}
