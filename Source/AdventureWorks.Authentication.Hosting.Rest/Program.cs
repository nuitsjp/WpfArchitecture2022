using AdventureWorks.Authentication.Jwt.Rest.Server;
using AdventureWorks.Business.SqlServer;
using AdventureWorks.Hosting.Rest;
using AdventureWorks.Logging.Serilog;

var builder = RestApplicationBuilder.CreateBuilder(args);

// 認証時に利用するUserRepositoryを有効化する。
builder.UseBusinessSqlServer();

// 他のアセンブリに存在するASP.NETのコントローラーをロードする。
builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);

// アプリケーションをビルドし実行する。
var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
await app.RunAsync();