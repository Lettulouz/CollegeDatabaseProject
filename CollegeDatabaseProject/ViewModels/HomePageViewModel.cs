using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CollegeDatabaseProject.Commands;
using CollegeDatabaseProject.Models;

namespace CollegeDatabaseProject.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    private int LabelInputValue = 0;

    private HomePage _homePage;
    
    public static double FontSize
    {
        get => 14;
    }
    
    public static double MinHeightLabelOrButton
    {
        get => 50;
    }
    
    public static double MinWidthLabelOrButton
    {
        get => 100;
    }
    
    
    private ObservableCollection<Country> _dataList = new();
    public ObservableCollection<Country> DataList
    {
        get { return _dataList; }
        set
        {
            _dataList = value;
            OnPropertyChanged("DataList");
        }
    }
    public string LabelInput
    {
        get => LabelInputValue.ToString();
        set
        {
            LabelInputValue = Int32.Parse(value); 
            OnPropertyChanged("LabelInput");
        }
    }
    
    public ICommand AddOneCommand { get; }

    public HomePageViewModel()
    {
        _homePage = new();
        AddOneCommand = new AddOneCommand(this);
    }


    public void OnPropChan()
    {
        OnPropertyChanged("DataList");
    }
    
}