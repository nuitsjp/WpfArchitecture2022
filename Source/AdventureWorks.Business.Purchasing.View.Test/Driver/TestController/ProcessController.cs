using Codeer.Friendly.Windows;
using NUnit.Framework;
using System.Diagnostics;
using System.IO;

namespace Driver.TestController
{
    public static class ProcessController
    {
        public static WindowsAppFriend Start(TestContext context)
        {
            //target path
            var targetPath = @"..\..\..\..\AdventureWorks.Purchasing.App.Driver\bin\Debug\net6.0-windows\AdventureWorks.Purchasing.App.Driver.exe";
            var info = new ProcessStartInfo(targetPath) { WorkingDirectory = Path.GetDirectoryName(targetPath)! };
            info.Environment["TestName"] = context.Test.FullName;
            var app = new WindowsAppFriend(Process.Start(info));
            app.ResetTimeout();
            return app;
        }

        public static void Kill(this WindowsAppFriend app)
        {
            if (app == null) return;

            app.ClearTimeout();
            try
            {
                Process.GetProcessById(app.ProcessId).Kill();
            }
            catch
            {
                // ignored
            }
        }
    }
}
