using CollegeDatabaseProject.Stores;

namespace CollegeDatabaseProject.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;
    
    public ViewModelBase SideBarViewModel
    {
        get => _navigationStore.SideBarViewModel;
        set => _navigationStore.SideBarViewModel = value;
    }
    
    public ViewModelBase TopBarViewModel
    {
        get => _navigationStore.TopBarViewModel;
        set => _navigationStore.TopBarViewModel = value;
    }
    
    public ViewModelBase CurrentViewModel
    {
        get => _navigationStore.CurrentViewModel;
        set => _navigationStore.CurrentViewModel = value;
    }

    public MainViewModel(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}