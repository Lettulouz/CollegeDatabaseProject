using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CollegeDatabaseProject.Commands;
using CollegeDatabaseProject.Models;
using HandyControl.Tools.Extension;
using MySqlConnector;

namespace CollegeDatabaseProject.ViewModels;

public class AdminViewModel : ViewModelBase
{
    private int LabelInputValue;

    private Country _country = new();

    public static double FontSize
    {
        get => 12;
    }
    
    public static double MinHeightLabelOrButton
    {
        get => 20;
    }
    
    public static double MinWidthLabelOrButton
    {
        get => 150;
    }
    
    private string? _chosenCountry = "";
    private int? _idChosenCountry;
    private string? _savedCountryName;
    private string _textInput1;
    private string _textInput2;
    private string _textInput31;
    private double _textInput32;
    private string _textInput4;
    private string _textInput5;
    private string _textInput61;
    private double _textInput62;
    private string _textInput71;
    private double _textInput72;

    public string? ChosenCountry
    {
        get => _chosenCountry;
        set
        {
            _chosenCountry = value;
            OnPropertyChanged();
        }
    }

    public string? SavedCountryName 
    { 
        get => _savedCountryName;
        set { 
            _savedCountryName = value; 
            OnPropertyChanged();
            
        }
    }

    public int? IdChosenCountry
    {
        get => _idChosenCountry;
        set
        {
            _idChosenCountry = value;
            OnPropertyChanged();
        }
    }
    
    public string? HeadOfCountry1
    {
        get => _country.HeadOfCountry1;
        set
        {
            _country.HeadOfCountry1 = value;
            OnPropertyChanged();
        }
    } 
    public string? HeadOfCountry2
    {
        get => _country.HeadOfCountry2;
        set
        {
            _country.HeadOfCountry2 = value;
            OnPropertyChanged();
        }
    } 
    public string? HeadOfCountry3
    {
        get => _country.HeadOfCountry3;
        set
        {
            _country.HeadOfCountry3 = value;
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

    public string TextInput1
    {
        get => _textInput1;
        set
        {
            _textInput1 = value; 
            OnPropertyChanged();
        }
    }
    public string TextInput2
    {
        get => _textInput2;
        set
        {
            _textInput2 = value; 
            OnPropertyChanged();
        }
    }
    public string TextInput31
    {
        get => _textInput31;
        set
        {
            _textInput31 = value; 
            OnPropertyChanged();
        }
    }
    public double TextInput32
    {
        get => _textInput32;
        set
        {
            _textInput32 = value; 
            OnPropertyChanged();
        }
    }
    public string TextInput4
    {
        get => _textInput4;
        set
        {
            _textInput4 = value; 
            OnPropertyChanged();
        }
    }
    public string TextInput5
    {
        get => _textInput5;
        set
        {
            _textInput5 = value; 
            OnPropertyChanged();
        }
    }
    public string TextInput61
    {
        get => _textInput61;
        set
        {
            _textInput61 = value; 
            OnPropertyChanged();
        }
    }
    public double TextInput62
    {
        get => _textInput62;
        set
        {
            _textInput62 = value; 
            OnPropertyChanged();
        }
    }
    public string TextInput71
    {
        get => _textInput71;
        set
        {
            _textInput71 = value; 
            OnPropertyChanged();
        }
    }
    public double TextInput72
    {
        get => _textInput72;
        set
        {
            _textInput72 = value; 
            OnPropertyChanged();
        }
    }
    
    public ICommand AddToListCommand { get; }
    public ICommand DatabaseModificationCommand { get; }
    
    public AdminViewModel()
    {
        DatabaseModificationCommand = new DatabaseModificationCommand(this);
        AddToListCommand = new AddToListCommand(this);
    }
    
    public void FillFieldsWithDb(string? countryName)
    {
        MySqlConnection con = new MySqlConnection(DbConnection.getDbString());

       
        var stm = "Select id from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd = new MySqlCommand(stm, con);
        cmd.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        con.Open();
        var output = cmd.ExecuteScalar();
        con.Close();
        _idChosenCountry = (int?)output;
        OnPropertyChanged(nameof(IdChosenCountry));
        //---------------------------
        
        var stm2 = "Select w.skrot from walutypanstwa AS wp " +
                  "INNER JOIN waluty AS w ON wp.id_waluty=w.id " +
                  "INNER JOIN panstwo AS p ON p.id=wp.id_panstwa " +
                  "WHERE p.id=@idPanstwa";
        var cmd2 = new MySqlCommand(stm2, con);
        cmd2.Parameters.AddWithValue("@idPanstwa", output);
        con.Open();
        var output2 = cmd2.ExecuteReader();
        CurrenciesInCountry.Clear();
        while (output2.Read())
        {
            for (int i = 0; i < output2.FieldCount; i++)
            {
                CurrenciesInCountry.Add(output2.GetValue(i).ToString());
            }
        }
        con.Close();
        
        //---------------------------
        
        var stm3 = "Select k.nazwaKontynentu from panstwokontynent AS pk " +
                   "INNER JOIN kontynenty AS k ON pk.id_kontynentu=k.id " +
                   "INNER JOIN panstwo AS p ON p.id=pk.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd3 = new MySqlCommand(stm3, con);
        cmd3.Parameters.AddWithValue("@idPanstwa", output);
        con.Open();
        var output3 = cmd3.ExecuteReader();
        CountryOnContinents.Clear();
        while (output3.Read())
        {
            for(int i=0; i<output3.FieldCount; i++)
                CountryOnContinents.Add(output3.GetValue(i).ToString());
        }
        con.Close();
        
        //---------------------------
        
        var stm4 = "Select CONCAT(n.nazwa,' - ',ln.liczebnosc,'%') from ludnoscwgnarodowosci AS ln " +
                   "INNER JOIN narodowosc AS n ON ln.id_narodowosci=n.id " +
                   "INNER JOIN panstwo AS p ON p.id=ln.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY ln.liczebnosc DESC";
        var cmd4 = new MySqlCommand(stm4, con);
        cmd4.Parameters.AddWithValue("@idPanstwa", output);
        con.Open();
        var output4 = cmd4.ExecuteReader();
        PopulationByNationality.Clear();
        while (output4.Read())
        {
            for(int i=0; i<output4.FieldCount; i++)
                PopulationByNationality.Add(output4.GetValue(i).ToString());
        }
        con.Close();
        
        //---------------------------
        
        var stm5 = "Select s.nazwa from stolicapanstwa AS sp " +
                   "INNER JOIN stolice AS s ON sp.id_stolicy=s.id " +
                   "INNER JOIN panstwo AS p ON p.id=sp.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd5 = new MySqlCommand(stm5, con);
        cmd5.Parameters.AddWithValue("@idPanstwa", output);
        con.Open();
        var output5 = cmd5.ExecuteReader();
        CapitalsOfCountry.Clear();
        while (output5.Read())
        {
            for(int i=0; i<output5.FieldCount; i++)
                CapitalsOfCountry.Add(output5.GetValue(i).ToString());
        }
        con.Close();
        
        //---------------------------
        
        var stm6 = "Select j.nazwa from jezykoficjalny AS jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd6 = new MySqlCommand(stm6, con);
        cmd6.Parameters.AddWithValue("@idPanstwa", output);
        con.Open();
        var output6 = cmd6.ExecuteReader();
        OfficialLanguages.Clear();
        while (output6.Read())
        {
            for(int i=0; i<output6.FieldCount; i++)
                OfficialLanguages.Add(output6.GetValue(i).ToString());
        }
        con.Close();
        
        //---------------------------
        
        var stm7 = "Select CONCAT(j.nazwa, ' - ', jo.procent, '%') from jezykobcy AS jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY jo.procent DESC";
        var cmd7 = new MySqlCommand(stm7, con);
        cmd7.Parameters.AddWithValue("@idPanstwa", output);
        con.Open();
        var output7 = cmd7.ExecuteReader();
        ForeignLanguages.Clear();
        while (output7.Read())
        {
            for(int i=0; i<output7.FieldCount; i++)
                ForeignLanguages.Add(output7.GetValue(i).ToString());
        }
        con.Close();
        
        //---------------------------
        
        var stm8 = "Select CONCAT(w.nazwa, ' - ', lw.liczebnosc, '%') from ludnoscwgwierzen AS lw " +
                   "INNER JOIN wiary AS w ON lw.id_wiary=w.id " +
                   "INNER JOIN panstwo AS p ON p.id=lw.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY lw.liczebnosc DESC";
        var cmd8 = new MySqlCommand(stm8, con);
        cmd8.Parameters.AddWithValue("@idPanstwa", output);
        con.Open();
        var output8 = cmd8.ExecuteReader();
        PopulationByFaith.Clear();
        while (output8.Read())
        {
            for(int i=0; i<output8.FieldCount; i++)
                PopulationByFaith.Add(output8.GetValue(i).ToString());
        }
        con.Close();
        
        //---------------------------
        
        var stm9 = "Select obszar from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd9 = new MySqlCommand(stm9, con);
        cmd9.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        con.Open();
        var output9 = cmd9.ExecuteScalar();
        con.Close();
        Territory = 0;
        if (output9 != null) 
            Territory = (int)output9;

        //---------------------------
        
        var stm10 = "Select tgp.tytul, gp.imie, gp.nazwisko from glowapanstwa as gp " +
                    "INNER JOIN tytulyglowpanstw as tgp ON gp.id_tytulu=tgp.id " +
                    "INNER JOIN panstwo as p ON gp.id_panstwa=p.id " +
                    "WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd10 = new MySqlCommand(stm10, con);
        cmd10.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        con.Open();
        HeadOfCountry1 = "";
        HeadOfCountry2 = "";
        HeadOfCountry3 = "";
        var output10 = cmd10.ExecuteReader();
        while(output10.Read())
        {
            HeadOfCountry1 = output10.GetValue(0).ToString();
            HeadOfCountry2 = output10.GetValue(1).ToString();
            HeadOfCountry3 = output10.GetValue(2).ToString();
        }
        con.Close();

        
        //---------------------------
        
        var stm11 = "Select ludnosc from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd11 = new MySqlCommand(stm11, con);
        cmd11.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        con.Open();
        var output11 = cmd11.ExecuteScalar();
        con.Close();
        Population = 0;
        if(output11 != null)
            Population = (int) output11;
        
        //---------------------------
        
        var stm12 = "Select hymn from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmd12 = new MySqlCommand(stm12, con);
        cmd12.Parameters.AddWithValue("@nazwaPanstwa", countryName);
        con.Open();
        var output12 = cmd12.ExecuteScalar();
        con.Close();
        Anthem = "";
        if(output12 !=  null)
            Anthem = output12.ToString();
    }
}