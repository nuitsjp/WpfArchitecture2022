using AdventureWorks.Hosting.MagicOnion.Server;

var builder = MagicOnionServerApplicationBuilder.CreateBuilder(args);

// �w���T�[�r�X�̏�����
AdventureWorks.Business.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server.Initializer.Initialize(builder);

// ���|�W�g���[�̏�����
AdventureWorks.Business.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.SqlServer.Initializer.Initialize(builder);

var app = await builder.BuildAsync(typeof(Program).Assembly.GetName().Name!);
app.Run();
