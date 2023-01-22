using System.Windows.Controls;


namespace CollegeDatabaseProject.Views;

public partial class AdminView : UserControl
{
    
    public AdminView()
    {
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        InitializeComponent();
    }
}