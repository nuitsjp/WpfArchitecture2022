using ApplicationBuilder = AdventureWorks.AspNetCore.Hosting.ApplicationBuilder;

var builder = ApplicationBuilder.CreateBuilder(args);

// �F�؃T�[�r�X�̏�����
AdventureWorks.Authentication.MagicOnion.Server.Initializer.Initialize(builder);

// �w���T�[�r�X�̏�����
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);

// ���|�W�g���[�̏�����
AdventureWorks.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Purchasing.SqlServer.Initializer.Initialize(builder);
AdventureWorks.Purchasing.UseCase.SqlServer.Initializer.Initialize(builder);

var app = builder.Build("AdventureWorks.Purchasing.Service");
app.Run();
