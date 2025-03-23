using System.Reflection;

namespace Catalog.API;

public class CarterDepedencyContextAssemblyCatalog : DependencyContextAssemblyCatalog
{
    public override IReadOnlyCollection<Assembly> GetAssemblies()
    {
        return new List<Assembly> { typeof(Program).Assembly };
    }
}
