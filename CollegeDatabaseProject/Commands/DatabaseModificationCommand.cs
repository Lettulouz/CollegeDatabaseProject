using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
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
    private ObservableCollection<string?> _nationalityTemp = new ();
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
        var stm3 = "Select CONCAT(n.nazwa,' - ',ln.liczebnosc,'%') from ludnoscwgnarodowosci AS ln " +
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
        int addNew = 0;
        foreach (var item in result)
        {
            var stm10 = "Select skrot from waluty WHERE skrot = @skrotWaluty";
            var cmd10 = new MySqlCommand(stm10, con);
            cmd10.Parameters.AddWithValue("@skrotWaluty", item);
            con.Open();
            var output10 = cmd10.ExecuteScalar();
            con.Close();
            addNew = 0;
                if (output10 == null)
                {
                    addNew = 1;
                }

                if (addNew.Equals(1) || !item.Contains(output10.ToString()))
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
        
        // Pobranie danych z widoku dla kontynetów
        IEnumerable<string> diffContinents = _adminViewModel.CountryOnContinents.Except(_continentsAll);
        /*
        * Wstawianie danych kontynetów do tabeli statycznej i łączącej
        */
        var result1 = diffContinents.ToList();
        int addNew1 = 0;
        foreach (var item in result1)
        {
            var stm13 = "Select nazwaKontynentu from kontynenty WHERE nazwaKontynentu = @nazwaKontynentu";
            var cmd13 = new MySqlCommand(stm13, con);
            cmd13.Parameters.AddWithValue("@nazwaKontynentu", item);
            con.Open();
            var output13 = cmd13.ExecuteScalar();
            con.Close();
            addNew1 = 0;
                if (output13 == null)
                {
                    addNew1 = 1;
                }

                if (addNew1.Equals(1) || !item.Contains(output13.ToString()))
                {
                    var stm14 = "INSERT INTO kontynenty(nazwaKontynentu) VALUES(@nazwaKontynentu)";
                    var cmd14 = new MySqlCommand(stm14, con);
                    cmd14.Parameters.AddWithValue("@nazwaKontynentu", item);
                    con.Open();
                    cmd14.ExecuteNonQuery();
                    con.Close();
                    long continent = cmd14.LastInsertedId;
                    var stm15 = "INSERT INTO panstwokontynent(id_panstwa, id_kontynentu) VALUES(@idPanstwa, @nazwaKontynentu)";
                    var cmd15 = new MySqlCommand(stm15, con);
                    cmd15.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd15.Parameters.AddWithValue("@nazwaKontynentu", continent);
                    con.Open();
                    cmd15.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    var stm16 = "Select id from kontynenty WHERE nazwaKontynentu = @nazwaKontynentu";
                    var cmd16 = new MySqlCommand(stm16, con);
                    cmd16.Parameters.AddWithValue("@nazwaKontynentu", item);
                    con.Open();
                    var output16 = cmd16.ExecuteScalar();
                    con.Close();
                    var stm17 = "INSERT INTO panstwokontynent(id_panstwa, id_kontynentu) VALUES(@idPanstwa, @nazwaKontynentu)";
                    var cmd17 = new MySqlCommand(stm17, con);
                    cmd17.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd17.Parameters.AddWithValue("@nazwaKontynentu", output16);
                    con.Open();
                    cmd17.ExecuteNonQuery();
                    con.Close();
                }
        }
        //==================================
        
        // Pobranie danych z widoku dla populacji wg narodowości

        
        IEnumerable<string> diffNationality = _adminViewModel.PopulationByNationality.Except(_nationalityAll);
        /*
        * Wstawianie danych populacji wg narodowości do tabeli statycznej i łączącej
        */
        var result2 = diffNationality.ToList();
        var NationalityResult = new List<string>();
        foreach (var item in result2)
        {
            var NationalityInput = item.TrimEnd('%');
            var NationalityLocal = NationalityInput.Split(" - ");
            NationalityResult.AddRange(NationalityLocal);
        }

        int addNew2 = 0;
        for (int i = 0; i < NationalityResult.Count-1; i = i + 2)
        {
            var stm18 = "Select nazwa from narodowosc WHERE nazwa = @nazwaNarodowosci";
            var cmd18 = new MySqlCommand(stm18, con);
            cmd18.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
            con.Open();
            var output18 = cmd18.ExecuteScalar();
            con.Close();
            addNew2 = 0;
                if (output18 == null)
                {
                    addNew2 = 1;
                }
                if (addNew2.Equals(1) || !NationalityResult[i].Contains(output18.ToString()))
                {
                    var stm19 = "INSERT INTO narodowosc(nazwa) VALUES(@nazwaNarodowosci)";
                    var cmd19 = new MySqlCommand(stm19, con);
                    cmd19.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
                    con.Open();
                    cmd19.ExecuteNonQuery();
                    con.Close();
                    long nationality = cmd19.LastInsertedId;
                    var stm20 = "INSERT INTO ludnoscwgnarodowosci(id_panstwa, id_narodowosci, liczebnosc) " +
                                "VALUES(@idPanstwa, @idNarodowosci, @liczebnosc)";
                    var cmd20 = new MySqlCommand(stm20, con);
                    cmd20.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd20.Parameters.AddWithValue("@idNarodowosci", nationality);
                    cmd20.Parameters.AddWithValue("@liczebnosc", NationalityResult[i+1]);
                    con.Open();
                    cmd20.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    var stm21 = "Select ln.id_narodowosci from ludnoscwgnarodowosci ln " +
                                "INNER JOIN narodowosc n ON n.id = ln.id_narodowosci " +
                                "WHERE ln.id_panstwa=@idPanstwa AND n.nazwa = @nazwaNarodowosci";
                    var cmd21 = new MySqlCommand(stm21, con);
                    cmd21.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
                    cmd21.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    con.Open();
                    var output21 = cmd21.ExecuteReader();
                    _nationalityTemp.Clear();
                    while (output21.Read())
                    {
                        for (int j = 0; j < output21.FieldCount; j++)
                        {
                            _nationalityTemp.Add(output21.GetValue(j).ToString());
                        }
                    }
                    con.Close();
                    var stm22 = "UPDATE ludnoscwgnarodowosci SET liczebnosc = @liczebnosc " +
                                "WHERE id_panstwa = @idPanstwa AND id_narodowosci = @idNarodowosci";
                    var cmd22 = new MySqlCommand(stm22, con);
                    cmd22.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd22.Parameters.AddWithValue("@idNarodowosci", _nationalityTemp[0]);
                    cmd22.Parameters.AddWithValue("@liczebnosc",  NationalityResult[i+1]);
                    con.Open();
                    cmd22.ExecuteNonQuery();
                    con.Close();
                }
        }
        //==================================
        
        
        
        
        


            _adminViewModel.FillFieldsWithDb(_adminViewModel.ChosenCountry);
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