using AdventureWorks.MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Authentication.MagicOnion.Client;

public class AuthenticationService : IAuthenticationService
{
    private readonly MagicOnionConfig _config;
    private Employee? _currentEmployee;


    public AuthenticationService(MagicOnionConfig config)
    {
        _config = config;
    }

    public Employee CurrentEmployee
    {
        get
        {
            if (_currentEmployee is null)
            {
                throw new InvalidOperationException("TryAuthenticateAsyncを正常終了後のみ利用可能です。");
            }

            return _currentEmployee;
        }
    }

    public async Task<bool> TryAuthenticateAsync()
    {
        // 単純にnet6-windowsにした場合、Server側もwindows縛りが発生してしまう。
        // それを避けようとした場合、IAuthenticationServiceServerを別アセンブリにする必要がある。
        // このコードはクライアントからしか呼ばれないため、そこまで厳密にする必要がないと判断し
        // ここでは警告を抑制する。
#pragma warning disable CA1416
        var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
#pragma warning restore CA1416

        var client = MagicOnionClient.Create<IAuthenticationServiceServer>(GrpcChannel.ForAddress(_config.Address));
        _currentEmployee = await client.GetEmployeeAsync(new LoginId(userName));

        return _currentEmployee is not null;
    }
}