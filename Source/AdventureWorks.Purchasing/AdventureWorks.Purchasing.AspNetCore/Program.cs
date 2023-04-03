var builder = AdventureWorks.AspNetCore.MagicOnion.MagicOnionApplicationBuilder.CreateBuilder(args);

// �w���T�[�r�X�̏�����
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);
AdventureWorks.Purchasing.RePurchasing.MagicOnion.Server.Initializer.Initialize(builder);

// ���|�W�g���[�̏�����
AdventureWorks.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Purchasing.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Purchasing.RePurchasing.SqlServer.Initializer.Initialize(builder);

var app = builder.Build("AdventureWorks.Purchasing.Service");
app.Run();
