using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CollegeDatabaseProject.Models;

public class Country
{
    public ObservableCollection<string?> Currencies { get; }
    public ObservableCollection<string?> Continents { get; }
    public ObservableCollection<string?> PopulationByNationality { get; }
    public ObservableCollection<string?> CapitalsOfCountry { get; }
    public ObservableCollection<string?> OfficialLanguages { get; }
    public ObservableCollection<string?> ForeignLanguages { get; }
    public ObservableCollection<string?> PopulationByFaith  { get; }
    public string? HeadOfCountry { get; set; }
    
    public string? HeadOfCountry1 { get; set; }
    public string? HeadOfCountry2 { get; set; }
    public string? HeadOfCountry3 { get; set; }
    
    public int? Population { get; set; }
    public int? Territory { get; set; }
    public string? Anthem { get; set; }

    public Country()
    {
        HeadOfCountry = "";
        HeadOfCountry1 = "";
        HeadOfCountry2 = "";
        HeadOfCountry3 = "";
        Population = 0;
        Territory = 0;
        Anthem = "";
        Currencies = new();
        Continents = new();
        PopulationByNationality = new();
        CapitalsOfCountry = new();
        OfficialLanguages = new();
        ForeignLanguages = new();
        PopulationByFaith = new();
    }

    public void AddCurrency(string currency)
    {
        Currencies.Add(currency);    
    }
    
    public void AddContinent(string continent)
    {
        Continents.Add(continent);    
    }
    public void AddPopulationByNationality(string populationByNationality)
    {
        PopulationByNationality.Add(populationByNationality);    
    }
    public void AddCapitalsOfCountry(string capitalsOfCountry)
    {
        CapitalsOfCountry.Add(capitalsOfCountry);    
    }
    public void AddOfficialLanguage(string officialLanguage)
    {
        OfficialLanguages.Add(officialLanguage);    
    }
    public void AddForeignLanguage(string foreignLanguage)
    {
        ForeignLanguages.Add(foreignLanguage);    
    }
    public void AddPopulationByFaith(string populationbyfaith)
    {
        PopulationByFaith.Add(populationbyfaith);    
    }
}