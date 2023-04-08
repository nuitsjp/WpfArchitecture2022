using Microsoft.Data.SqlClient;

namespace AdventureWorks.Database;

public static class ConnectionStringProvider
{
    public static string Resolve(string userId, string password)
    {
        return new SqlConnectionStringBuilder
        {
            DataSource = Environments.GetEnvironmentVariable("AdventureWorks.Database.DataSource", "localhost"),
            InitialCatalog = "AdventureWorks",
            UserID = userId,
            Password = password,
            TrustServerCertificate = true
        }.ToString();
    }
}