using AdventureWorks.Authentication;
using AdventureWorks.Wpf.ViewModel;
using Kamishibai;

namespace AdventureWorks.Business.Purchasing.ViewModel;

public class MainViewModel : INavigatedAsyncAware
{
    private readonly Menu.ViewModel.IPresentationService _presentationService;

    public MainViewModel(
        [Inject] Menu.ViewModel.IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        await _presentationService.NavigateToMenuAsync();
    }
}

