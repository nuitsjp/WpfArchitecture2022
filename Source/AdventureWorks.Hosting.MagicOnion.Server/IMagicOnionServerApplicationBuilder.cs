using System.Reflection;

namespace AdventureWorks.Hosting.MagicOnion.Server;

public interface IMagicOnionServerApplicationBuilder : IApplicationBuilder
{
    void AddServiceAssembly(Assembly serviceAssembly);
}