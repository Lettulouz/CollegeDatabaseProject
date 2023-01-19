using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using CollegeDatabaseProject.Services;
using CollegeDatabaseProject.Stores;
using CollegeDatabaseProject.ViewModels;

namespace CollegeDatabaseProject.Commands;

public class LoginCommand : CommandBase
{
    private LoginViewModel _loginViewModel;
    private readonly HomePageViewModel _homePageViewModel;
    private NavigationStore _navigationStore = new();
    public LoginCommand(LoginViewModel loginViewModel, HomePageViewModel homePageViewModel)
    {
        _loginViewModel = loginViewModel;
        _homePageViewModel = homePageViewModel;
    }
    public override void Execute(object? parameter)
    {
        IntPtr valuePtr = Marshal.SecureStringToGlobalAllocUnicode(_loginViewModel.SecurePassword);
        string plainTextPassword = Marshal.PtrToStringUni(valuePtr);
        if (plainTextPassword.Equals(_loginViewModel.ConstPass) 
            && _loginViewModel.UserLogin.Equals(_loginViewModel.ConstLogin))
        {
            var currentShowDialog = Application.Current.Windows[1];
            currentShowDialog.Close();
            TopBarViewModel topBarViewModel = new(1);
            topBarViewModel.AppName = "Administrator";
            _navigationStore.CurrentViewModel = CreateAdminViewModel();
            _navigationStore.TopBarViewModel = topBarViewModel;
            _navigationStore.SideBarViewModel = new SideBarViewModel(_homePageViewModel);

            Window test = new AdminWindow()
            {
                DataContext = new MainViewModel(_navigationStore),
            };
            test.ShowDialog();
        }
        else
        {
            MessageBox.Show("Błędne dane");
        }
    }
    
    private AdminViewModel CreateAdminViewModel()
    {
        AdminViewModel adminViewModel = new();
        return adminViewModel;
    }
}