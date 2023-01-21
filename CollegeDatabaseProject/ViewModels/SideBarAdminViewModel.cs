using System.Collections.ObjectModel;
using System.Windows.Input;
using CollegeDatabaseProject.Commands;
using MySqlConnector;

namespace CollegeDatabaseProject.ViewModels;

public class SideBarAdminViewModel : ViewModelBase
{
    private MainViewModel _mainViewModel;
    private HomePageViewModel _homePageViewModel;

    private AdminViewModel _adminViewModel;
    
    private ObservableCollection<string?> _dataList = new();

    private string _searchField = "";

    private object _selectedCountry = "Polska";

    private string _searchOutputField = "";

    public object SelectedCountry
    {
        get => _selectedCountry;
        set
        {
            _selectedCountry = value;
            OnPropertyChanged();
            if(_selectedCountry !=null || _selectedCountry !="") _searchOutputField = _selectedCountry.ToString();
            OnPropertyChanged(nameof(SearchOutputField));
            if (_homePageViewModel != null) _homePageViewModel.ChosenCountry = _selectedCountry.ToString();
            if (_adminViewModel != null) _adminViewModel.ChosenCountry = _selectedCountry.ToString();
        }
    }
    public string SearchOutputField
    {
        get => _searchOutputField;
        set
        {
            _selectedCountry = value;
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string?> DataList
    {
        get { return _dataList; }
        set
        {
            _dataList = value;
            OnPropertyChanged();
        }
    }

    public string SearchField
    {
        get => _searchField;
        set
        {
            _searchField = value;
            OnPropertyChanged();
        }
    }

    public ICommand ReloadButtonCommand { get; }
    public ICommand SearchButtonCommand { get; }
    public ICommand OpenAdminCommand { get; }
    
    public SideBarAdminViewModel(AdminViewModel adminViewModel)
    {
        _adminViewModel = adminViewModel;
        ReloadButtonCommand = new ReloadButtonCommand(this);
        SearchButtonCommand = new SearchButtonCommand(this);
        MySqlConnection con = new MySqlConnection(DbConnection.getDbString());

        var stm = "Select nazwaPanstwa from panstwo";
        var cmd = new MySqlCommand(stm, con);
        con.Open();
        var output = cmd.ExecuteReader();
        _dataList.Clear();
        while (output.Read())
        {
            for(int i=0; i<output.FieldCount; i++)
                _dataList.Add(output.GetValue(i).ToString());
        }
    }

    public void OnPropChange()
    {
        OnPropertyChanged();
    }
}