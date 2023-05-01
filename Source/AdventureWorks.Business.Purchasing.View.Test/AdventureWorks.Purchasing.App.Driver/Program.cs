using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using AdventureWorks.Business.Purchasing.View;
using AdventureWorks.Purchasing.App.Driver;
using Scenario = AdventureWorks.Purchasing.App.Driver.Scenario;

string? testName = Environment.GetEnvironmentVariable("TestName");
File.AppendAllText("log.txt", $"TestName: {testName}\r\n");

var builder = ApplicationBuilder<App, MainWindow>.CreateBuilder();

try
{
    var builderName = $"AdventureWorks.Purchasing.App.Driver.{testName}.ContainerBuilder";
    var builderTye = Type.GetType(builderName)!;
    var builderInstance = Activator.CreateInstance(builderTye) as IContainerBuilder;
    builderInstance!.Build(builder.Services);

    // View & ViewModelを初期化する。
    Initializer.Initialize(builder);

    // アプリケーションをビルドし実行する。
    var app = builder.Build("AdventureWorks.Purchasing.App");
    await app.RunAsync();
}
catch (Exception exception)
{
    File.AppendAllLines(
        "log.txt", 
        new []
        {
            exception.Message,
            exception.StackTrace ?? string.Empty
        });
    throw;
}


public interface IContainerBuilder
{
    void Build(IServiceCollection services);
}
