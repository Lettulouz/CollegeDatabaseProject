using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Input;
using CollegeDatabaseProject.Commands;

namespace CollegeDatabaseProject.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private SecureString _securePassword = new();
    private string _constlogin = "administrator";
    private string _constpass = "Admin123";
    private string _userLogin = "";

    public string ConstLogin
    {
        get => _constlogin;
    }

    public string ConstPass
    {
        get => _constpass;
    }

    public string UserLogin
    {
        get => _userLogin;
        set
        {
            _userLogin = value;
            OnPropertyChanged();
        }
    }
    public SecureString SecurePassword
    {
        get => _securePassword;
        set
        {
            _securePassword = value;
            OnPropertyChanged();
        }
        
    }

    public ICommand LoginCommand { get; }
    
    public LoginViewModel(HomePageViewModel homePageViewModel)
    {
        LoginCommand = new LoginCommand(this, homePageViewModel);
    }
}