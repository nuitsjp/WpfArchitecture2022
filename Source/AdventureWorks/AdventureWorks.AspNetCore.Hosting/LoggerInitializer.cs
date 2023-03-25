using System.Text;
using AdventureWorks.Database;
using Microsoft.Extensions.Configuration;
using Serilog;
#if !DEBUG
using Dapper;
using Microsoft.Data.SqlClient;
#endif

namespace AdventureWorks.AspNetCore.Hosting
{
    public static class LoggerInitializer
    {
    }
}