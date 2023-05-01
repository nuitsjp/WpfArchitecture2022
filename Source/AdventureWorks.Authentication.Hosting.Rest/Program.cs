using AdventureWorks.Authentication.Jwt.Rest.Server;
using AdventureWorks.Business.SqlServer;
using AdventureWorks.Hosting.Rest;
using AdventureWorks.Logging.Serilog;

var builder = RestApplicationBuilder.CreateBuilder(args);

// �F�؎��ɗ��p����UserRepository��L��������B
builder.UseBusinessSqlServer();

// ���̃A�Z���u���ɑ��݂���ASP.NET�̃R���g���[���[�����[�h����B
builder
    .Services
    .AddControllers()
    .AddApplicationPart(typeof(AuthenticationController).Assembly);

// �A�v���P�[�V�������r���h�����s����B
var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
await app.RunAsync();