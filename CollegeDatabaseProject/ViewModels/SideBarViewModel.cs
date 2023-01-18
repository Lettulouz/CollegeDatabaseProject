using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CollegeDatabaseProject.Commands;
using CollegeDatabaseProject.Models;
using MySqlConnector;

namespace CollegeDatabaseProject.ViewModels;

public class SideBarViewModel : ViewModelBase
{
    private HomePageViewModel _homePageViewModel;
    
    private ObservableCollection<string> _dataList = new();

    private string _searchField = "";

    private object _selectedCountry;

    private string _searchOutputField = "";

    public object SelectedCountry
    {
        get => _selectedCountry;
        set
        {
            _selectedCountry = value;
            OnPropertyChanged("SelectedCountry");
            _searchOutputField = _selectedCountry.ToString() ?? throw new InvalidOperationException();
            OnPropertyChanged(nameof(SearchOutputField));
            _homePageViewModel.ChosenCountry = _selectedCountry.ToString();
            
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
    public ICommand OpenAdminCommand { get; }

    public SideBarViewModel(HomePageViewModel homePageViewModel)
    {
        _homePageViewModel = homePageViewModel;
        ReloadButtonCommand = new ReloadButtonCommand(this);
        SearchButtonCommand = new SearchButtonCommand(this);
        OpenAdminCommand = new OpenAdminCommand();
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