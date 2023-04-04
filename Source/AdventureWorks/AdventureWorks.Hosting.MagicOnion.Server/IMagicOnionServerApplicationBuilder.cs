using System.Reflection;

namespace AdventureWorks.Hosting.MagicOnion.Server;

public interface IMagicOnionServerApplicationBuilder : IMagicOnionApplicationBuilder
{
    void AddServiceAssembly(Assembly serviceAssembly);

}