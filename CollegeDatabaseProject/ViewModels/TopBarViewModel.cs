using System.Windows.Media;
using CollegeDatabaseProject.Models;

namespace CollegeDatabaseProject.ViewModels;

public class TopBarViewModel : ViewModelBase
{
    private readonly TopBar _topBar;
    
    public static string AppIconPath => TopBar.AppIconPath;

    public string AppName
    {
        get => _topBar.AppName;
        set
        {
            _topBar.AppName = value;
            OnPropertyChanged();
        }
    }

    public static string MinimizeIconPath => TopBar.MinimizeIconPath;

    public static string StateIconPath => TopBar.StateIconPath;

    public static string CloseIconPath => TopBar.CloseIconPath;

    public static double FontSize => TopBar.FontSize;

    public static string FontFamily => TopBar.FontFamily;

    public static string FontWeight => TopBar.FontWeight;

    public Brush BrushFillPolygon => _topBar.SolidColorBrushesList[0];
    
    public Brush BrushStrokePolygon => _topBar.SolidColorBrushesList[1];
    
    public Brush BrushMainBackground => _topBar.SolidColorBrushesList[2];

    public Brush BrushHoverBackground => _topBar.SolidColorBrushesList[2];

    private int _colorSwitch;

    public TopBarViewModel(int colorSwitch)
    {
        _colorSwitch = colorSwitch;
        _topBar = new TopBar(_colorSwitch);
    }
}