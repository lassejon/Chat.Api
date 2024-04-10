using Microsoft.Extensions.DependencyInjection;

namespace Chat.Application.Settings;

public class Settings<T>
{
    private static readonly List<string> Prefixes = new ();
    
    private const string Suffix = "Settings";
    
    protected string SectionName { get; private set; } = default!;
    
    public virtual IServiceCollection OnConfigure(IServiceCollection serviceCollection)
    {
        var sectionName = typeof(T).Name;
        
        if (sectionName.EndsWith(Suffix))
        {
            sectionName = sectionName[..^Suffix.Length];
        }
        
        foreach (var prefix in Prefixes.Where(prefix => sectionName.StartsWith(prefix)))
        {
            sectionName = sectionName[prefix.Length..];
            break;
        }
        
        SectionName = sectionName;

        return serviceCollection;
    }
}