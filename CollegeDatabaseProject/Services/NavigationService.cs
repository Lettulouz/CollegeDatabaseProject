using System;
using CollegeDatabaseProject.Stores;
using CollegeDatabaseProject.ViewModels;

namespace CollegeDatabaseProject.Services;

public class NavigationService
{
    private readonly NavigationStore _navigationStore;

    public NavigationService(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
    public void Navigate(ViewModelBase viewModelBase)
    {
        _navigationStore.CurrentViewModel = viewModelBase;
    }
}
