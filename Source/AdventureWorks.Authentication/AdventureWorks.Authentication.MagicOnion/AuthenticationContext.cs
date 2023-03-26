using AdventureWorks.MagicOnion;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace AdventureWorks.Authentication.MagicOnion;

public class AuthenticationContext : IAuthenticationContext
{
    private readonly MagicOnionConfig _config;
    private Employee? _currentEmployee;


    public AuthenticationContext(MagicOnionConfig config)
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
        // 本当はAzure Active Directory認証などを用いたいが、サンプルとして動作させるさいに
        // なんらかの認証サーバーを用意するのは困難なので、ここではOSのユーザーを取得して利用する。
        // 実際、とくに公開APIではセキュリティ的に問題となるため、下記を参照していずれかの放棄を選択する。
        // https://learn.microsoft.com/ja-jp/aspnet/core/grpc/authn-and-authz?view=aspnetcore-7.0
        var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
#pragma warning restore CA1416
        var client = MagicOnionClient.Create<IAuthenticationService>(GrpcChannel.ForAddress(_config.Address));
        _currentEmployee = await client.GetEmployeeAsync(new LoginId(userName));

        return _currentEmployee is not null;
    }
}