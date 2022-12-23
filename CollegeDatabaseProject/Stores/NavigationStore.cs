using System;
using CollegeDatabaseProject.ViewModels;

namespace CollegeDatabaseProject.Stores;

public class NavigationStore
{
    private ViewModelBase _currentViewModel = null!;
    private ViewModelBase _topBarStoreViewModel = null!;

    public ViewModelBase TopBarViewModel
    {
        get => _topBarStoreViewModel;
        set
        {
            _topBarStoreViewModel = value;
            OnCurrentViewModelChanged();
        }
    }
    
    public ViewModelBase CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }

    public event Action? CurrentViewModelChanged;
}