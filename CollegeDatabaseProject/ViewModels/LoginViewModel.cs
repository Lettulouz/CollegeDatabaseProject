using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CollegeDatabaseProject.Commands;

namespace CollegeDatabaseProject.ViewModels;

public class PasswordBoxMonitor : DependencyObject {
    public static bool GetIsMonitoring(DependencyObject obj) {
        return (bool)obj.GetValue(IsMonitoringProperty);
    }

    public static void SetIsMonitoring(DependencyObject obj, bool value) {
        obj.SetValue(IsMonitoringProperty, value);
    }

    public static readonly DependencyProperty IsMonitoringProperty =
        DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));

    public static int GetPasswordLength(DependencyObject obj) {
        return (int)obj.GetValue(PasswordLengthProperty);
    }

    public static void SetPasswordLength(DependencyObject obj, int value) {
        obj.SetValue(PasswordLengthProperty, value);
    }

    public static readonly DependencyProperty PasswordLengthProperty =
        DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

    private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        var pb = d as PasswordBox;
        if (pb == null) {
            return;
        }
        if ((bool) e.NewValue) {
            pb.PasswordChanged += PasswordChanged;
        } else {
            pb.PasswordChanged -= PasswordChanged;
        }
    }

    static void PasswordChanged(object sender, RoutedEventArgs e) {
        var pb = sender as PasswordBox;
        if (pb == null) {
            return;
        }
        SetPasswordLength(pb, pb.Password.Length);
    }
}

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
    
    public LoginViewModel()
    {
        LoginCommand = new LoginCommand(this);
    }
}