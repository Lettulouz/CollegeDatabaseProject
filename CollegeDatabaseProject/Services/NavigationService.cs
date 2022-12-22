using System;
using WarehousePLUS.Stores;
using WarehousePLUS.ViewModels;

namespace WarehousePLUS.Services;

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
