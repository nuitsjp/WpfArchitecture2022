using AdventureWorks.Authentication;
using AdventureWorks.Logging;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.Menu.ViewModel;

/// <summary>
/// MainWindowのViewModel
/// </summary>
public class MainViewModel : INavigatedAsyncAware
{
    /// <summary>
    /// 認証サービス
    /// </summary>
    private readonly IAuthenticationService _authenticationService;
    /// <summary>
    /// ロギング初期化サービス
    /// </summary>
    private readonly ILoggingInitializer _loggingInitializer;
    /// <summary>
    /// プレゼンテーションサービス
    /// </summary>
    private readonly IPresentationService _presentationService;

    /// <summary>
    /// インスタンスを生成する。
    /// </summary>
    /// <param name="authenticationService"></param>
    /// <param name="loggingInitializer"></param>
    /// <param name="presentationService"></param>
    public MainViewModel(
        [Inject] IAuthenticationService authenticationService,
        [Inject] ILoggingInitializer loggingInitializer,
        [Inject] IPresentationService presentationService)
    {
        _authenticationService = authenticationService;
        _loggingInitializer = loggingInitializer;
        _presentationService = presentationService;
    }

    /// <summary>
    /// ナビゲーション完了時の処理
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        // 認証を試行する。
        var authenticationResult = await _authenticationService.TryAuthenticateAsync();
        if (authenticationResult.IsAuthenticated
            && await _loggingInitializer.TryInitializeAsync())
        {
            // メニュー画面に遷移する。
            await _presentationService.NavigateToMenuAsync();
        }
        else
        {
            // 認証に失敗した場合はエラーメッセージを表示する。
            _presentationService.ShowMessage(
                Purchasing.ViewModel.Properties.Resources.AuthenticationFailed,
                Purchasing.ViewModel.Properties.Resources.AuthenticationFailedCaption,
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            // アプリケーションを終了する。
            Environment.Exit(1);
        }
    }
}

