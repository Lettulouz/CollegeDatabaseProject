using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CollegeDatabaseProject.ViewModels;
using HandyControl.Controls;
using MySqlConnector;

namespace CollegeDatabaseProject.Commands;

public class DatabaseModificationCommand :CommandBase
{
    private AdminViewModel _adminViewModel;
    private ObservableCollection<string?> _countrysAll = new ();
    private ObservableCollection<string?> _currenciesAll = new ();
    private ObservableCollection<string?> _continentsAll = new ();
    private ObservableCollection<string?> _nationalityAll = new ();
    private ObservableCollection<string?> _capitalAll = new ();
    private ObservableCollection<string?> _langOfficiallAll = new ();
    private ObservableCollection<string?> _langForeignllAll = new ();
    private ObservableCollection<string?> _religionAll = new ();
    public DatabaseModificationCommand(AdminViewModel adminViewModel)
    {
        _adminViewModel = adminViewModel;
    }

    public override void Execute(object? parameter)
    {
        MySqlConnection con = new MySqlConnection(DbConnection.getDbString());

        /*
         * Pobieranie wszystkich danych z tabeli państwa
         */
        var stm = "Select id from panstwo";
        var cmd = new MySqlCommand(stm, con);
        con.Open();
        var output = cmd.ExecuteReader();
        // Wczytywanie danych do listy
        _countrysAll.Clear();
        while (output.Read())
        {
            for (int i = 0; i < output.FieldCount; i++)
            {
                _countrysAll.Add(output.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================

        /*
         * Pobieranie danych z tabeli waluty
         */
        var stm1 = "Select w.skrot from walutypanstwa wp INNER JOIN waluty w ON w.id = wp.id_waluty " +
                   "WHERE wp.id_panstwa=@idPanstwa";
        var cmd1 = new MySqlCommand(stm1, con);
        cmd1.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        con.Open();
        var output1 = cmd1.ExecuteReader();
        _currenciesAll.Clear();
        while (output1.Read())
        {
            for (int i = 0; i < output1.FieldCount; i++)
            {
                 _currenciesAll.Add(output1.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================
        
        /*
         * Pobieranie danych z tabeli kontynenty
         */
        var stm2 = "Select k.nazwaKontynentu from panstwokontynent AS pk " +
                   "INNER JOIN kontynenty AS k ON pk.id_kontynentu=k.id " +
                   "INNER JOIN panstwo AS p ON p.id=pk.id_panstwa " +
                   "WHERE p.id=@idPanstwa";;
        var cmd2 = new MySqlCommand(stm2, con);
        cmd2.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        con.Open();
        var output2 = cmd2.ExecuteReader();
        _continentsAll.Clear();
        while (output2.Read())
        {
            for (int i = 0; i < output2.FieldCount; i++)
            {
                _continentsAll.Add(output2.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli narodowosci panstwa
        */
        var stm3 = "Select n.nazwa from ludnoscwgnarodowosci AS ln " +
                   "INNER JOIN narodowosc AS n ON ln.id_narodowosci=n.id " +
                   "INNER JOIN panstwo AS p ON p.id=ln.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY ln.liczebnosc DESC";
        var cmd3 = new MySqlCommand(stm3, con);
        cmd3.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        con.Open();
        var output3 = cmd3.ExecuteReader();
        _nationalityAll.Clear();
        while (output3.Read())
        {
            for (int i = 0; i < output3.FieldCount; i++)
            {
                _nationalityAll.Add(output3.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli stolice danego panstwa
        */
        var stm4 = "Select s.nazwa from stolicapanstwa AS sp " +
                   "INNER JOIN stolice AS s ON sp.id_stolicy=s.id " +
                   "INNER JOIN panstwo AS p ON p.id=sp.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd4 = new MySqlCommand(stm4, con);
        cmd4.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        con.Open();
        var output4 = cmd4.ExecuteReader();
        _capitalAll.Clear();
        while (output4.Read())
        {
            for (int i = 0; i < output4.FieldCount; i++)
            {
                _capitalAll.Add(output4.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli jezyk oficjalny panstwa
        */
        var stm5 = "Select j.nazwa from jezykoficjalny AS jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd5 = new MySqlCommand(stm5, con);
        cmd5.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        con.Open();
        var output5 = cmd5.ExecuteReader();
        _langOfficiallAll.Clear();
        while (output5.Read())
        {
            for (int i = 0; i < output5.FieldCount; i++)
            {
                _langOfficiallAll.Add(output5.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli jezyk obcy panstwa
        */
        var stm6 = "Select j.nazwa from jezykobcy AS jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY jo.procent DESC";
        var cmd6 = new MySqlCommand(stm6, con);
        cmd6.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        con.Open();
        var output6 = cmd6.ExecuteReader();
        _langForeignllAll.Clear();
        while (output6.Read())
        {
            for (int i = 0; i < output6.FieldCount; i++)
            {
                _langForeignllAll.Add(output6.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli wiara ludnosci
        */
        var stm7 = "Select w.nazwa from ludnoscwgwierzen AS lw " +
                   "INNER JOIN wiary AS w ON lw.id_wiary=w.id " +
                   "INNER JOIN panstwo AS p ON p.id=lw.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY lw.liczebnosc DESC";
        var cmd7 = new MySqlCommand(stm7, con);
        cmd7.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        con.Open();
        var output7 = cmd7.ExecuteReader();
        _religionAll.Clear();
        while (output7.Read())
        {
            for (int i = 0; i < output7.FieldCount; i++)
            {
                _religionAll.Add(output7.GetValue(i).ToString());
            }
        }
        con.Close();
        //==================================
        
        // Pobranie danych z widoku dla walut
        IEnumerable<string> diffCurrencies = _adminViewModel.CurrenciesInCountry.Except(_currenciesAll);
        /*
        * Wstawianie danych walut do tabeli statycznej i łączącej
        */
        var result = diffCurrencies.ToList();
        foreach (var item in result)
        {
            var stm10 = "Select skrot from waluty WHERE skrot = @skrotWaluty";
            var cmd10 = new MySqlCommand(stm10, con);
            cmd10.Parameters.AddWithValue("@skrotWaluty", item);
            con.Open();
            var output10 = cmd10.ExecuteScalar();
            con.Close();
                if (output10 == null)
                {
                    output10 = "brak";
                }

                if (!item.Contains(output10.ToString()))
                {
                    var stm8 = "INSERT INTO waluty(skrot) VALUES(@skrotWaluty)";
                    var cmd8 = new MySqlCommand(stm8, con);
                    cmd8.Parameters.AddWithValue("@skrotWaluty", item);
                    con.Open();
                    cmd8.ExecuteNonQuery();
                    con.Close();
                    long currency = cmd8.LastInsertedId;
                    var stm9 = "INSERT INTO walutypanstwa(id_panstwa, id_waluty) VALUES(@idPanstwa, @idCurrency)";
                    var cmd9 = new MySqlCommand(stm9, con);
                    cmd9.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd9.Parameters.AddWithValue("@idCurrency", currency);
                    con.Open();
                    cmd9.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    var stm11 = "Select id from waluty WHERE skrot = @skrotWaluty";
                    var cmd11 = new MySqlCommand(stm11, con);
                    cmd11.Parameters.AddWithValue("@skrotWaluty", item);
                    con.Open();
                    var output11 = cmd11.ExecuteScalar();
                    con.Close();
                    var stm12 = "INSERT INTO walutypanstwa(id_panstwa, id_waluty) VALUES(@idPanstwa, @idCurrency)";
                    var cmd12 = new MySqlCommand(stm12, con);
                    cmd12.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd12.Parameters.AddWithValue("@idCurrency", output11);
                    con.Open();
                    cmd12.ExecuteNonQuery();
                    con.Close();
                }
        }
        //==================================
        
 



            CleanInputs();
    }

    private void CleanInputs()
    {
        _adminViewModel.TextInput1 = "";
        _adminViewModel.TextInput2 = "";
        _adminViewModel.TextInput31 = "";
        _adminViewModel.TextInput32 = 0;
        _adminViewModel.TextInput4 = "";
        _adminViewModel.TextInput5 = "";
        _adminViewModel.TextInput61 = "";
        _adminViewModel.TextInput62 = 0;
        _adminViewModel.TextInput71 = "";
        _adminViewModel.TextInput72 = 0;
    }
}