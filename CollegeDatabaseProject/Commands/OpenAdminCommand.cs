using System;
using System.Windows;
using CollegeDatabaseProject.Stores;
using CollegeDatabaseProject.ViewModels;

namespace CollegeDatabaseProject.Commands;

public class OpenAdminCommand : CommandBase
{
    private readonly NavigationStore _navigationStore = new();
    private readonly HomePageViewModel _homePageViewModel;
    
    public OpenAdminCommand(HomePageViewModel homePageViewModel)
    {
        _homePageViewModel = homePageViewModel;
    }
    
    public override void Execute(object? parameter)
    {
        TopBarViewModel topBarViewModel = new(0);
        topBarViewModel.AppName = "Administrator";
        _navigationStore.CurrentViewModel = CreateLoginViewModel();
        _navigationStore.TopBarViewModel = topBarViewModel;
        _navigationStore.SideBarViewModel = new SideBarViewModel(_homePageViewModel);
        Window test = new LoginWindow()
        {
            DataContext = new MainViewModel(_navigationStore),
        };
        test.ShowDialog();
    }
    
    
    private LoginViewModel CreateLoginViewModel()
    {
        LoginViewModel loginViewModel = new();
        return loginViewModel;
    }

}