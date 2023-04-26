using AdventureWorks.Authentication;
using AdventureWorks.Logging;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.ViewModel;

public class MainViewModel : INavigatedAsyncAware
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ILoggingInitializer _loggingInitializer;
    private readonly Menu.ViewModel.IPresentationService _presentationService;

    public MainViewModel(
        [Inject] IAuthenticationService authenticationService,
        [Inject] ILoggingInitializer loggingInitializer,
        [Inject] Menu.ViewModel.IPresentationService presentationService)
    {
        _authenticationService = authenticationService;
        _loggingInitializer = loggingInitializer;
        _presentationService = presentationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        var authenticationResult = await _authenticationService.TryAuthenticateAsync();
        if (authenticationResult.IsAuthenticated
            && await _loggingInitializer.TryInitializeAsync())
        {
            await _presentationService.NavigateToMenuAsync();
        }
        else
        {
            _presentationService.ShowMessage(
                "ユーザー認証に失敗しました。",
                "認証エラー",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            // アプリケーションを終了する。
            Environment.Exit(1);
        }
    }
}

