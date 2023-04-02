using System.Reflection;

namespace AdventureWorks.Hosting;

public interface IMagicOnionServerApplicationBuilder : IMagicOnionApplicationBuilder
{
    void AddServiceAssembly(Assembly serviceAssembly);

}