using Kamishibai;

namespace AdventureWorks.Purchasing.ViewModel;

public class MainViewModel : INavigatedAsyncAware
{
    private readonly IPresentationService _presentationService;

    public MainViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    public Task OnNavigatedAsync(PostForwardEventArgs args) => _presentationService.NavigateToMenuAsync();
}
