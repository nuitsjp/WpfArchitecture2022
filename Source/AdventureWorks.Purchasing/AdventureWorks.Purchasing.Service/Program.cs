var builder = AdventureWorks.AspNetCore.ApplicationBuilder.CreateBuilder(args);

// �F�؃T�[�r�X�̏�����
AdventureWorks.Authentication.MagicOnion.Server.Initializer.Initialize(builder);

// �w���T�[�r�X�̏�����
AdventureWorks.Purchasing.MagicOnion.Server.Initializer.Initialize(builder);

// ���|�W�g���[�̏�����
AdventureWorks.Purchasing.Database.Initializer.Initialize(builder);
AdventureWorks.Purchasing.UseCase.Database.Initializer.Initialize(builder);

var app = builder.Build();
app.Run();
