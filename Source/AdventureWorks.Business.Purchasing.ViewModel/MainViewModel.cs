using AdventureWorks.Authentication;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.ViewModel;

public class MainViewModel : INavigatedAsyncAware
{
    private readonly IAuthenticationService _authenticationService;
    private readonly Menu.ViewModel.IPresentationService _presentationService;

    public MainViewModel(
        [Inject] IAuthenticationService authenticationService,
        [Inject] Menu.ViewModel.IPresentationService presentationService)
    {
        _authenticationService = authenticationService;
        _presentationService = presentationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        if (await _authenticationService.TryAuthenticateAsync())
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

