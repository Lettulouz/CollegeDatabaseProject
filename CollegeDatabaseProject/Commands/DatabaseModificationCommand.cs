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
         * Pobieranie wszystkich danych z tabeli waluty
         */
        var stm1 = "Select id_waluty from walutypanstwa " +
                   "WHERE id_panstwa=@idPanstwa";
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
         * Pobieranie wszystkich danych z tabeli kontynenty
         */
        var stm2 = "Select id from kontynenty";
        var cmd2 = new MySqlCommand(stm2, con);
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
        * Pobieranie wszystkich danych z tabeli kontynenty
        */
        var stm3 = "Select id_narodowosci, liczebnosc from ludnoscwgnarodowosci " +
                   "WHERE id_panstwa=@idPanstwa";
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
        * Pobieranie wszystkich danych z tabeli stolice danego panstwa
        */
        var stm4 = "Select id_stolicy from stolicapanstwa WHERE id_panstwa=@idPanstwa";
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
        * Pobieranie wszystkich danych z tabeli jezyk oficjalny panstwa
        */
        var stm5 = "Select id_jezyka from jezykoficjalny WHERE id_panstwa=@idPanstwa";
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
        * Pobieranie wszystkich danych z tabeli jezyk oficjalny panstwa
        */
        var stm6 = "Select id_jezyka from jezykobcy WHERE id_panstwa=@idPanstwa";
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
        * Pobieranie wszystkich danych z tabeli wiara ludnosci
        */
        var stm7 = "Select id_wiary from ludnoscwgwierzen WHERE id_panstwa=@idPanstwa";
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
        
        IEnumerable<string> diffCurrencies = _adminViewModel.CurrenciesInCountry.Except(_currenciesAll);
        if(diffCurrencies.Count()>0)
        {
            var message = string.Join(Environment.NewLine,diffCurrencies);
            MessageBox.Show(message);
        }
        

    }
}