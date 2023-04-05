using AdventureWorks.Business.Purchasing.MagicOnion.Server;

var builder = AdventureWorks.Hosting.MagicOnion.Server.MagicOnionServerApplicationBuilder.CreateBuilder(args);

// �w���T�[�r�X�̏�����
Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.MagicOnion.Server.Initializer.Initialize(builder);

// ���|�W�g���[�̏�����
AdventureWorks.Business.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Business.Purchasing.RePurchasing.SqlServer.Initializer.Initialize(builder);

var app = builder.Build("AdventureWorks.Purchasing.Service");
app.Run();
