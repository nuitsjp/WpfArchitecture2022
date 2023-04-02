using System.Reflection;

namespace AdventureWorks.Hosting.Server;

public interface IApplicationBuilder : Hosting.IApplicationBuilder
{
    void Add(Assembly serviceAssembly);
}