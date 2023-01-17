using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CollegeDatabaseProject.Models;

public class Country
{
    public ObservableCollection<string> Currencies { get; }
    public ObservableCollection<string> Continents { get; }
    public ObservableCollection<string> PopulationByNationality { get; }
    public ObservableCollection<string> CapitalsOfCountry { get; }
    public ObservableCollection<string> OfficialLanguages { get; }
    public ObservableCollection<string> ForeignLanguages { get; }
    public ObservableCollection<string> PopulationByFaith  { get; }
    public string HeadOfCountry { get; }

    public Country(string headofcountry)
    {
        HeadOfCountry = headofcountry;
        Currencies = new();
        Continents = new();
        PopulationByNationality = new();
        CapitalsOfCountry = new();
        OfficialLanguages = new();
        ForeignLanguages = new();
        PopulationByFaith = new();
        AddCurrency("PLN");
        AddCurrency("EUR");
        AddContinent("Europa");
        AddPopulationByNationality("Polacy: 35000000");
        AddPopulationByNationality("Ukraińcy: 2000000");
        AddCapitalsOfCountry("Warszawa");
        AddOfficialLanguage("polski");
        AddForeignLanguage("angielski - 58%");
        AddForeignLanguage("niemiecki - 25%");
        AddForeignLanguage("francuski - 13%");
        AddForeignLanguage("hiszpański - 4%");
        AddPopulationByFaith("chrześcijaństwo - 50%");
        AddPopulationByFaith("muzułmanizm - 10%");
        AddPopulationByFaith("buddyzm - 5%");
        AddPopulationByFaith("niewierzący - 35%");
        
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