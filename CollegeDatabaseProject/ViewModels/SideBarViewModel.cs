using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using CollegeDatabaseProject.Commands;
using CollegeDatabaseProject.Models;
using MySqlConnector;

namespace CollegeDatabaseProject.ViewModels;

public class SideBarViewModel : ViewModelBase
{
    private ObservableCollection<string> _dataList = new();

    private string _searchField = "";
    
    public string SearchOutputField
    {
        get => "Polska";
    }
    
    public ObservableCollection<string> DataList
    {
        get { return _dataList; }
        set
        {
            _dataList = value;
            OnPropertyChanged("DataList");
        }
    }

    public string SearchField
    {
        get => _searchField;
        set
        {
            _searchField = value;
            OnPropertyChanged("SearchField");
        }
    }

    public ICommand ReloadButtonCommand { get; }
    public ICommand SearchButtonCommand { get; }

    public SideBarViewModel()
    {
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
        OnPropertyChanged("DataList");

    }

    public void OnPropChange()
    {
        OnPropertyChanged("DataList");
    }
}