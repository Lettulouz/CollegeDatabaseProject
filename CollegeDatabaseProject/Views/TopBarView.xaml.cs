using System.Windows;
using System.Windows.Input;
using CollegeDatabaseProject.Models;

namespace CollegeDatabaseProject.Views;

public partial class TopBarView
{ 
    public TopBarView()
    {
        InitializeComponent();
    }

    private void ExitButtonOn_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }

    private void ChangeStateOn_Click(object sender, RoutedEventArgs e)
    {
        if(Window.GetWindow(this).WindowState == WindowState.Maximized)
        {
            Window.GetWindow(this).WindowState = WindowState.Normal;
        }
        else if (Window.GetWindow(this).WindowState != null)
            Window.GetWindow(this).WindowState = WindowState.Maximized;
    }

    private void MinimizeButtonOn_Click(object sender, RoutedEventArgs e)
    {
        if (Window.GetWindow(this).WindowState != null) Window.GetWindow(this).WindowState = WindowState.Minimized;
    }
}