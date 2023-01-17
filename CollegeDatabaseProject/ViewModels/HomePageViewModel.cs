using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CollegeDatabaseProject.Commands;
using CollegeDatabaseProject.Models;
using HandyControl.Tools.Extension;

namespace CollegeDatabaseProject.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    private int LabelInputValue = 0;

    private HomePage _homePage;
    
    private Country _country = new("Prezydent Anrzej Duda");
    
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
    
    private string _chosenCountry = "";

    public string ChosenCountry
    {
        get => _chosenCountry;
        set
        {
            _chosenCountry = value;
            OnPropertyChanged();
        }
    }
    
    public string HeadOfCountry
    {
        get => _country.HeadOfCountry;
    }
    
    
    public ObservableCollection<string> CurrenciesInCountry
    {
        get { return _country.Currencies; }
        set
        {
            _country.Currencies.Clear();
            _country.Currencies.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string> CountryOnContinents
    {
        get { return _country.Continents; }
        set
        {
            _country.Currencies.Clear();
            _country.Currencies.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string> PopulationByNationality
    {
        get { return _country.PopulationByNationality; }
        set
        {
            _country.PopulationByNationality.Clear();
            _country.PopulationByNationality.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string> CapitalsOfCountry
    {
        get { return _country.CapitalsOfCountry; }
        set
        {
            _country.CapitalsOfCountry.Clear();
            _country.CapitalsOfCountry.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string> ForeignLanguages
    {
        get { return _country.ForeignLanguages; }
        set
        {
            _country.ForeignLanguages.Clear();
            _country.ForeignLanguages.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string> OfficialLanguages
    {
        get { return _country.OfficialLanguages; }
        set
        {
            _country.OfficialLanguages.Clear();
            _country.OfficialLanguages.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string> PopulationByFaith
    {
        get { return _country.PopulationByFaith; }
        set
        {
            _country.PopulationByFaith.Clear();
            _country.PopulationByFaith.AddRange(value);
            OnPropertyChanged();
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