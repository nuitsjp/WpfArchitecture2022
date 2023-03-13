using AdventureWorks.Authentication;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel;

public class MainViewModel : INavigatedAsyncAware
{
    private readonly IAuthenticationService _authenticationService;
    private readonly Menu.ViewModel.IPresentationService _presentationService;

    public MainViewModel(
        [Inject] Menu.ViewModel.IPresentationService presentationService, 
        [Inject] IAuthenticationService authenticationService)
    {
        _presentationService = presentationService;
        _authenticationService = authenticationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        if (await _authenticationService.TryAuthenticateAsync())
        {
            await _presentationService.NavigateToMenuAsync();
        }
        else
        {
            throw new NotImplementedException("認証失敗時の処理は現時点で未実装です。");
        }
    }
}

