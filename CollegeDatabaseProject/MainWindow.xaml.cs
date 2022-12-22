using System.Windows;
using System.Windows.Input;

namespace CollegeDatabaseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private new void MouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void ContentControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
    }
}