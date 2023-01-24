using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CollegeDatabaseProject.Commands;
using CollegeDatabaseProject.Models;
using HandyControl.Tools.Extension;
using MySqlConnector;

namespace CollegeDatabaseProject.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    private int LabelInputValue;

    private Country _country = new();

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
    
    private string? _chosenCountry = "";

    public string? ChosenCountry
    {
        get => _chosenCountry;
        set
        {
            _chosenCountry = value;
            FillFieldsWithDb(_chosenCountry);
            OnPropertyChanged();
        }
    }
    
    public string? HeadOfCountry
    {
        get => _country.HeadOfCountry;
        set
        {
            _country.HeadOfCountry = value;
            OnPropertyChanged();
        }
    }
    
    public int? Population
    {
        get => _country.Population;
        set
        {
            _country.Population = value;
            OnPropertyChanged();
        }
    }
    
    public int? Territory
    {
        get => _country.Territory;
        set
        {
            _country.Territory = value;
            OnPropertyChanged();
        }
    }
    
    public string? Anthem
    {
        get => _country.Anthem;
        set
        {
            _country.Anthem = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string?> CurrenciesInCountry
    {
        get { return _country.Currencies; }
        set
        {
            _country.Currencies.Clear();
            _country.Currencies.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string?> CountryOnContinents
    {
        get { return _country.Continents; }
        set
        {
            _country.Currencies.Clear();
            _country.Currencies.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string?> PopulationByNationality
    {
        get { return _country.PopulationByNationality; }
        set
        {
            _country.PopulationByNationality.Clear();
            _country.PopulationByNationality.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string?> CapitalsOfCountry
    {
        get { return _country.CapitalsOfCountry; }
        set
        {
            _country.CapitalsOfCountry.Clear();
            _country.CapitalsOfCountry.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string?> ForeignLanguages
    {
        get { return _country.ForeignLanguages; }
        set
        {
            _country.ForeignLanguages.Clear();
            _country.ForeignLanguages.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string?> OfficialLanguages
    {
        get { return _country.OfficialLanguages; }
        set
        {
            _country.OfficialLanguages.Clear();
            _country.OfficialLanguages.AddRange(value);
            OnPropertyChanged();
        }
    }
    
    public ObservableCollection<string?> PopulationByFaith
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
            OnPropertyChanged();
        }
    }

    public HomePageViewModel()
    {
    }

    public void FillFieldsWithDb(string? countryName)
    {
        MySqlConnection con = new MySqlConnection(DbConnection.getDbString());
        
        
        con.Open();
        MySqlTransaction tr = con.BeginTransaction();
        var stm = "Select id from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd = new MySqlCommand(stm, con, tr);
        cmd.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        var output = cmd.ExecuteScalar();
        

        
        //---------------------------
        
        var stm2 = "Select w.skrot from walutypanstwa AS wp " +
                  "INNER JOIN waluty AS w ON wp.id_waluty=w.id " +
                  "INNER JOIN panstwo AS p ON p.id=wp.id_panstwa " +
                  "WHERE p.id=@idPanstwa";
        var cmd2 = new MySqlCommand(stm2, con, tr);
        cmd2.Parameters.AddWithValue("@idPanstwa", output);
        var output2 = cmd2.ExecuteReader();
        CurrenciesInCountry.Clear();
        while (output2.Read())
        {
            for (int i = 0; i < output2.FieldCount; i++)
            {
                CurrenciesInCountry.Add(output2.GetValue(i).ToString());
            }
        }
        output2.Close();
        
        //---------------------------
        
        var stm3 = "Select k.nazwaKontynentu from panstwokontynent AS pk " +
                   "INNER JOIN kontynenty AS k ON pk.id_kontynentu=k.id " +
                   "INNER JOIN panstwo AS p ON p.id=pk.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd3 = new MySqlCommand(stm3, con, tr);
        cmd3.Parameters.AddWithValue("@idPanstwa", output);
        var output3 = cmd3.ExecuteReader();
        CountryOnContinents.Clear();
        while (output3.Read())
        {
            for(int i=0; i<output3.FieldCount; i++)
                CountryOnContinents.Add(output3.GetValue(i).ToString());
        }
        output3.Close();
        
        //---------------------------
        
        var stm4 = "Select CONCAT(n.nazwa,' - ',ln.liczebnosc,'%') from ludnoscwgnarodowosci AS ln " +
                   "INNER JOIN narodowosc AS n ON ln.id_narodowosci=n.id " +
                   "INNER JOIN panstwo AS p ON p.id=ln.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY ln.liczebnosc DESC";
        var cmd4 = new MySqlCommand(stm4, con, tr);
        cmd4.Parameters.AddWithValue("@idPanstwa", output);
        var output4 = cmd4.ExecuteReader();
        PopulationByNationality.Clear();
        while (output4.Read())
        {
            for(int i=0; i<output4.FieldCount; i++)
                PopulationByNationality.Add(output4.GetValue(i).ToString());
        }
        output4.Close();
        
        //---------------------------
        
        var stm5 = "Select s.nazwa from stolicapanstwa AS sp " +
                   "INNER JOIN stolice AS s ON sp.id_stolicy=s.id " +
                   "INNER JOIN panstwo AS p ON p.id=sp.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd5 = new MySqlCommand(stm5, con, tr);
        cmd5.Parameters.AddWithValue("@idPanstwa", output);
        var output5 = cmd5.ExecuteReader();
        CapitalsOfCountry.Clear();
        while (output5.Read())
        {
            for(int i=0; i<output5.FieldCount; i++)
                CapitalsOfCountry.Add(output5.GetValue(i).ToString());
        }
        output5.Close();
        
        //---------------------------
        
        var stm6 = "Select j.nazwa from jezykoficjalny AS jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd6 = new MySqlCommand(stm6, con, tr);
        cmd6.Parameters.AddWithValue("@idPanstwa", output);
        var output6 = cmd6.ExecuteReader();
        OfficialLanguages.Clear();
        while (output6.Read())
        {
            for(int i=0; i<output6.FieldCount; i++)
                OfficialLanguages.Add(output6.GetValue(i).ToString());
        }
        output6.Close();
        
        //---------------------------
        
        var stm7 = "Select CONCAT(j.nazwa, ' - ', jo.procent, '%') from jezykobcy AS jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY jo.procent DESC";
        var cmd7 = new MySqlCommand(stm7, con, tr);
        cmd7.Parameters.AddWithValue("@idPanstwa", output);
        var output7 = cmd7.ExecuteReader();
        ForeignLanguages.Clear();
        while (output7.Read())
        {
            for(int i=0; i<output7.FieldCount; i++)
                ForeignLanguages.Add(output7.GetValue(i).ToString());
        }
        output7.Close();
        
        //---------------------------
        
        var stm8 = "Select CONCAT(w.nazwa, ' - ', lw.liczebnosc, '%') from ludnoscwgwierzen AS lw " +
                   "INNER JOIN wiary AS w ON lw.id_wiary=w.id " +
                   "INNER JOIN panstwo AS p ON p.id=lw.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY lw.liczebnosc DESC";
        var cmd8 = new MySqlCommand(stm8, con, tr);
        cmd8.Parameters.AddWithValue("@idPanstwa", output);
        var output8 = cmd8.ExecuteReader();
        PopulationByFaith.Clear();
        while (output8.Read())
        {
            for(int i=0; i<output8.FieldCount; i++)
                PopulationByFaith.Add(output8.GetValue(i).ToString());
        }
        output8.Close();
        
        //---------------------------
        
        var stm9 = "Select obszar from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd9 = new MySqlCommand(stm9, con, tr);
        cmd9.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        var output9 = cmd9.ExecuteScalar();
        Territory = 0;
        if (output9 != null) Territory = (int) output9;

        //---------------------------
        
        var stm10 = "Select CONCAT(tgp.tytul, ' ', gp.imie, ' ', gp.nazwisko) from glowapanstwa as gp " +
                    "INNER JOIN tytulyglowpanstw as tgp ON gp.id_tytulu=tgp.id " +
                    "INNER JOIN panstwo as p ON gp.id_panstwa=p.id " +
                    "WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd10 = new MySqlCommand(stm10, con, tr);
        cmd10.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        var output10 = cmd10.ExecuteScalar();
        HeadOfCountry = "Nie podano";
        if(output10 != null)
            HeadOfCountry = output10.ToString();
        
        //---------------------------
        
        var stm11 = "Select ludnosc from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd11 = new MySqlCommand(stm11, con, tr);
        cmd11.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        var output11 = cmd11.ExecuteScalar();
        Population = 0;
        if(output11 != null)
            Population = (int)output11;
        
        //---------------------------
        
        var stm12 = "Select hymn from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd12 = new MySqlCommand(stm12, con, tr);
        cmd12.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        var output12 = cmd12.ExecuteScalar();
        tr.Commit();
        con.Close();
        Anthem = "Nie podano";
        if(output12 !=  null)
            Anthem = output12.ToString();
    }
}