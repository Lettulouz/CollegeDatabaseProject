using System.Windows;
using System.Windows.Controls;

namespace CollegeDatabaseProject.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        
    }
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext != null)
        { ((dynamic)this.DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword; }
    }
}