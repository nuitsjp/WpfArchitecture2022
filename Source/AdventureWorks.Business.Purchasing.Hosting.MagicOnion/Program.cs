using AdventureWorks.Business.Purchasing.MagicOnion.Server;
using AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server;
using AdventureWorks.Business.Purchasing.RePurchasing.SqlServer;
using AdventureWorks.Business.Purchasing.SqlServer;
using AdventureWorks.Business.SqlServer;
using AdventureWorks.Hosting.MagicOnion.Server;
using AdventureWorks.Logging.Serilog;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

// �w���T�[�r�X�̏�����
builder.UsePurchasingMagicOnionServer();
builder.UseRePurchasingMagicOnionServer();

// ���|�W�g���[�̏�����
builder.UseBusinessSqlServer();
builder.UsePurchasingSqlServer();
builder.UseRePurchasingSqlServer();

// �A�v���P�[�V�������r���h�����s����B
var app = await builder.BuildAsync(new ApplicationName(typeof(Program).Assembly.GetName().Name!));
app.Run();
