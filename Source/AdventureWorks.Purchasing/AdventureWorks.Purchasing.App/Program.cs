using AdventureWorks.Purchasing.View;

namespace AdventureWorks.Purchasing.App;

public class Program
{
    [STAThread]
    public static void Main()
    {
        View.App app = new();
        app.Run(new MainWindow());
    }
}
