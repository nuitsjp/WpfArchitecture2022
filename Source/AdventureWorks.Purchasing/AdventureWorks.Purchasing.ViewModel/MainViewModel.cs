using AdventureWorks.Authentication;
using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel;

public class MainViewModel : INavigatedAsyncAware
{
    private readonly IAuthenticationContext _authenticationContext;
    private readonly Menu.ViewModel.IPresentationService _presentationService;

    public MainViewModel(
        [Inject] Menu.ViewModel.IPresentationService presentationService, 
        [Inject] IAuthenticationContext authenticationContext)
    {
        _presentationService = presentationService;
        _authenticationContext = authenticationContext;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        if (await _authenticationContext.TryAuthenticateAsync("AdventureWorks.Purchasing"))
        {
            await _presentationService.NavigateToMenuAsync();
        }
        else
        {
            throw new NotImplementedException("認証失敗時の処理は現時点で未実装です。");
        }
    }
}

