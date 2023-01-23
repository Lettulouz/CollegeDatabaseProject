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
    private ObservableCollection<string?> _LanguageForeignTemp = new ();
    private ObservableCollection<string?> _ReligionTemp = new ();
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
        var stm6 = "Select CONCAT(j.nazwa, ' - ', jo.procent, '%') from jezykobcy jo " +
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
        var stm7 = "Select CONCAT(w.nazwa, ' - ', lw.liczebnosc, '%') from ludnoscwgwierzen AS lw " +
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
                    var stm15 = "INSERT INTO panstwokontynent(id_panstwa, id_kontynentu) VALUES(@idPanstwa, @idKontynentu)";
                    var cmd15 = new MySqlCommand(stm15, con);
                    cmd15.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd15.Parameters.AddWithValue("@idKontynentu", continent);
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
                    var stm17 = "INSERT INTO panstwokontynent(id_panstwa, id_kontynentu) VALUES(@idPanstwa, @idKontynentu)";
                    var cmd17 = new MySqlCommand(stm17, con);
                    cmd17.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd17.Parameters.AddWithValue("@idKontynentu", output16);
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
                    var stm50 = "Select ln.id_narodowosci from ludnoscwgnarodowosci ln " +
                                "INNER JOIN narodowosc n ON n.id = ln.id_narodowosci " +
                                "WHERE ln.id_panstwa=@idPanstwa AND n.nazwa = @nazwaNarodowosci";
                    var cmd50 = new MySqlCommand(stm50, con);
                    cmd50.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
                    cmd50.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    con.Open();
                    var output50 = cmd50.ExecuteScalar();
                    con.Close();
                    if (output50 == null)
                    {
                        var stm51 = "Select id from narodowosc WHERE nazwa = @nazwaNarodowosci";
                        var cmd51 = new MySqlCommand(stm51, con);
                        cmd51.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
                        con.Open();
                        var output51 = cmd51.ExecuteReader();
                        _nationalityTemp.Clear();
                        while (output51.Read())
                        {
                            for (int j = 0; j < output51.FieldCount; j++)
                            {
                                _nationalityTemp.Add(output51.GetValue(j).ToString());
                            }
                        }
                        con.Close();
                        var stm52 = "INSERT INTO ludnoscwgnarodowosci(id_panstwa, id_narodowosci, liczebnosc) " +
                                    "VALUES(@idPanstwa, @idNarodowosci, @liczebnosc)";
                        var cmd52 = new MySqlCommand(stm52, con);
                        cmd52.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd52.Parameters.AddWithValue("@idNarodowosci", _nationalityTemp[0]);
                        cmd52.Parameters.AddWithValue("@liczebnosc", NationalityResult[i+1]);
                        con.Open();
                        cmd52.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        var stm53 = "UPDATE ludnoscwgnarodowosci SET liczebnosc = @liczebnosc " +
                                    "WHERE id_panstwa = @idPanstwa AND id_narodowosci = @idNarodowosci";
                        var cmd53 = new MySqlCommand(stm53, con);
                        cmd53.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd53.Parameters.AddWithValue("@idNarodowosci", (int)output50);
                        cmd53.Parameters.AddWithValue("@liczebnosc",  NationalityResult[i+1]);
                        con.Open();
                        cmd53.ExecuteNonQuery();
                        con.Close();
                    }
                }
        }
        //==================================
        
        // Pobranie danych z widoku dla stolic
        IEnumerable<string> diffCapitals = _adminViewModel.CapitalsOfCountry.Except(_capitalAll);
        /*
        * Wstawianie danych stolic do tabeli statycznej i łączącej
        */
        var result3 = diffCapitals.ToList();
        int addNew3 = 0;
        foreach (var item in result3)
        {
            var stm23 = "Select nazwa from stolice WHERE nazwa = @nazwaStolicy";
            var cmd23 = new MySqlCommand(stm23, con);
            cmd23.Parameters.AddWithValue("@nazwaStolicy", item);
            con.Open();
            var output23 = cmd23.ExecuteScalar();
            con.Close();
            addNew3 = 0;
                if (output23 == null)
                {
                    addNew3 = 1;
                }

                if (addNew3.Equals(1) || !item.Contains(output23.ToString()))
                {
                    var stm24 = "INSERT INTO stolice(nazwa) VALUES(@nazwaStolicy)";
                    var cmd24 = new MySqlCommand(stm24, con);
                    cmd24.Parameters.AddWithValue("@nazwaStolicy", item);
                    con.Open();
                    cmd24.ExecuteNonQuery();
                    con.Close();
                    long capital = cmd24.LastInsertedId;
                    var stm25 = "INSERT INTO stolicapanstwa(id_panstwa, id_stolicy) VALUES(@idPanstwa, @idStolicy)";
                    var cmd25 = new MySqlCommand(stm25, con);
                    cmd25.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd25.Parameters.AddWithValue("@idStolicy", capital);
                    con.Open();
                    cmd25.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    var stm26 = "Select id from stolice WHERE nazwa = @nazwaStolicy";
                    var cmd26 = new MySqlCommand(stm26, con);
                    cmd26.Parameters.AddWithValue("@nazwaStolicy", item);
                    con.Open();
                    var output26 = cmd26.ExecuteScalar();
                    con.Close();
                    var stm27 = "INSERT INTO stolicapanstwa(id_panstwa, id_stolicy) VALUES(@idPanstwa, @idStolicy)";
                    var cmd27 = new MySqlCommand(stm27, con);
                    cmd27.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd27.Parameters.AddWithValue("@idStolicy", output26);
                    con.Open();
                    cmd27.ExecuteNonQuery();
                    con.Close();
                }
        }
        //==================================
        
        // Pobranie danych z widoku dla języka oficialnego
        IEnumerable<string> diffLanguageOff = _adminViewModel.OfficialLanguages.Except(_langOfficiallAll);
        /*
        * Wstawianie danych języka oficialnego do tabeli statycznej i łączącej
        */
        var result4 = diffLanguageOff.ToList();
        int addNew4 = 0;
        foreach (var item in result4)
        {
            var stm28 = "Select nazwa from jezyki WHERE nazwa = @nazwaJezykaOff";
            var cmd28 = new MySqlCommand(stm28, con);
            cmd28.Parameters.AddWithValue("@nazwaJezykaOff", item);
            con.Open();
            var output28 = cmd28.ExecuteScalar();
            con.Close();
            addNew4 = 0;
                if (output28 == null)
                {
                    addNew4 = 1;
                }

                if (addNew4.Equals(1) || !item.Contains(output28.ToString()))
                {
                    var stm29 = "INSERT INTO jezyki(nazwa) VALUES(@nazwaJezykaOff)";
                    var cmd29 = new MySqlCommand(stm29, con);
                    cmd29.Parameters.AddWithValue("@nazwaJezykaOff", item);
                    con.Open();
                    cmd29.ExecuteNonQuery();
                    con.Close();
                    long LanguageOfficial = cmd29.LastInsertedId;
                    var stm30 = "INSERT INTO jezykoficjalny(id_panstwa, id_jezyka) VALUES(@idPanstwa, @idJezykaOff)";
                    var cmd30 = new MySqlCommand(stm30, con);
                    cmd30.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd30.Parameters.AddWithValue("@idJezykaOff", LanguageOfficial);
                    con.Open();
                    cmd30.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    var stm31 = "Select id from jezyki WHERE nazwa = @nazwaJezykaOff";
                    var cmd31 = new MySqlCommand(stm31, con);
                    cmd31.Parameters.AddWithValue("@nazwaJezykaOff", item);
                    con.Open();
                    var output31 = cmd31.ExecuteScalar();
                    con.Close();
                    var stm32 = "INSERT INTO jezykoficjalny(id_panstwa, id_jezyka) VALUES(@idPanstwa, @idJezykaOff)";
                    var cmd32 = new MySqlCommand(stm32, con);
                    cmd32.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd32.Parameters.AddWithValue("@idJezykaOff", output31);
                    con.Open();
                    cmd32.ExecuteNonQuery();
                    con.Close();
                }
        }
        //==================================
        
        // Pobranie danych z widoku dla języka obcego
        IEnumerable<string> diffLanguageFore = _adminViewModel.ForeignLanguages.Except(_langForeignllAll);
        /*
        * Wstawianie danych języka obcego do tabeli statycznej i łączącej
        */
        var result5 = diffLanguageFore.ToList();
        var LanguageForeResult = new List<string>();
        foreach (var item in result5)
        {
            var LanguageForeInput = item.TrimEnd('%');
            var LanguageForeLocal = LanguageForeInput.Split(" - ");
            LanguageForeResult.AddRange(LanguageForeLocal);
        }

        int addNew5 = 0;
        for (int i = 0; i < LanguageForeResult.Count-1; i = i + 2)
        {
            var stm33 = "Select nazwa from jezyki WHERE nazwa = @nazwaJezykaFore";
            var cmd33 = new MySqlCommand(stm33, con);
            cmd33.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
            con.Open();
            var output33 = cmd33.ExecuteScalar();
            con.Close();
            addNew5 = 0;
                if (output33 == null)
                {
                    addNew5 = 1;
                }
                if (addNew5.Equals(1) || !LanguageForeResult[i].Contains(output33.ToString()))
                {
                    var stm34 = "INSERT INTO jezyki(nazwa) VALUES(@nazwaJezykaFore)";
                    var cmd34 = new MySqlCommand(stm34, con);
                    cmd34.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
                    con.Open();
                    cmd34.ExecuteNonQuery();
                    con.Close();
                    long nationality = cmd34.LastInsertedId;
                    var stm35 = "INSERT INTO jezykobcy(id_panstwa, id_jezyka, procent) " +
                                "VALUES(@idPanstwa, @idJezykaFore, @procent)";
                    var cmd35 = new MySqlCommand(stm35, con);
                    cmd35.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd35.Parameters.AddWithValue("@idJezykaFore", nationality);
                    cmd35.Parameters.AddWithValue("@procent", LanguageForeResult[i+1]);
                    con.Open();
                    cmd35.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    var stm36 = "Select jb.id_jezyka from jezykobcy jb " +
                                "INNER JOIN jezyki j ON j.id = jb.id_jezyka " +
                                "WHERE jb.id_panstwa=@idPanstwa AND j.nazwa = @nazwaJezykaFore";
                    var cmd36 = new MySqlCommand(stm36, con);
                    cmd36.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
                    cmd36.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    con.Open();
                    var output36 = cmd36.ExecuteScalar();
                    con.Close();
                    if (output36 == null)
                    {
                        var stm38 = "Select id from jezyki WHERE nazwa = @nazwaJezykaFore";
                        var cmd38 = new MySqlCommand(stm38, con);
                        cmd38.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
                        con.Open();
                        var output38 = cmd38.ExecuteReader();
                        _LanguageForeignTemp.Clear();
                        while (output38.Read())
                        {
                            for (int j = 0; j < output38.FieldCount; j++)
                            {
                                _LanguageForeignTemp.Add(output38.GetValue(j).ToString());
                            }
                        }
                        con.Close();
                        var stm39 = "INSERT INTO jezykobcy(id_panstwa, id_jezyka, procent) " +
                                    "VALUES(@idPanstwa, @idJezykaFore, @procent)";
                        var cmd39 = new MySqlCommand(stm39, con);
                        cmd39.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd39.Parameters.AddWithValue("@idJezykaFore", _LanguageForeignTemp[0]);
                        cmd39.Parameters.AddWithValue("@procent", LanguageForeResult[i+1]);
                        con.Open();
                        cmd39.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        var stm37 = "UPDATE jezykobcy SET procent = @procent " +
                                    "WHERE id_panstwa = @idPanstwa AND id_jezyka = @idJezykaFore";
                        var cmd37 = new MySqlCommand(stm37, con);
                        cmd37.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd37.Parameters.AddWithValue("@idJezykaFore", (int)output36);
                        cmd37.Parameters.AddWithValue("@procent",  LanguageForeResult[i+1]);
                        con.Open();
                        cmd37.ExecuteNonQuery();
                        con.Close();
                    }
                }
        }
        //==================================
        
        // Pobranie danych z widoku dla ludnosci wg wiary
        IEnumerable<string> diffReligion = _adminViewModel.PopulationByFaith.Except(_religionAll);
        /*
        * Wstawianie danych ludnosci wg wiary do tabeli statycznej i łączącej
        */
        var result6 = diffReligion.ToList();
        var ReligionResult = new List<string>();
        foreach (var item in result6)
        {
            var ReligionInput = item.TrimEnd('%');
            var ReligionLocal = ReligionInput.Split(" - ");
            ReligionResult.AddRange(ReligionLocal);
        }

        int addNew6 = 0;
        for (int i = 0; i < ReligionResult.Count-1; i = i + 2)
        {
            var stm40 = "Select nazwa from wiary WHERE nazwa = @nazwaWiary";
            var cmd40 = new MySqlCommand(stm40, con);
            cmd40.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
            con.Open();
            var output40 = cmd40.ExecuteScalar();
            con.Close();
            addNew6 = 0;
                if (output40 == null)
                {
                    addNew6 = 1;
                }
                if (addNew6.Equals(1) || !ReligionResult[i].Contains(output40.ToString()))
                {
                    var stm41 = "INSERT INTO wiary(nazwa) VALUES(@nazwaWiary)";
                    var cmd41 = new MySqlCommand(stm41, con);
                    cmd41.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
                    con.Open();
                    cmd41.ExecuteNonQuery();
                    con.Close();
                    long religion = cmd41.LastInsertedId;
                    var stm42 = "INSERT INTO ludnoscwgwierzen(id_panstwa, id_wiary, liczebnosc) " +
                                "VALUES(@idPanstwa, @idWiary, @liczebnosc)";
                    var cmd42 = new MySqlCommand(stm42, con);
                    cmd42.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd42.Parameters.AddWithValue("@idWiary", religion);
                    cmd42.Parameters.AddWithValue("@liczebnosc", ReligionResult[i+1]);
                    con.Open();
                    cmd42.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    var stm43 = "Select lw.id_wiary from ludnoscwgwierzen lw " +
                                "INNER JOIN wiary w ON w.id = lw.id_wiary " +
                                "WHERE lw.id_panstwa=@idPanstwa AND w.nazwa = @nazwaWiary";
                    var cmd43 = new MySqlCommand(stm43, con);
                    cmd43.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
                    cmd43.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    con.Open();
                    var output43 = cmd43.ExecuteScalar();
                    con.Close();
                    if (output43 == null)
                    {
                        var stm44 = "Select id from wiary WHERE nazwa = @nazwaWiary";
                        var cmd44 = new MySqlCommand(stm44, con);
                        cmd44.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
                        con.Open();
                        var output44 = cmd44.ExecuteReader();
                        _ReligionTemp.Clear();
                        while (output44.Read())
                        {
                            for (int j = 0; j < output44.FieldCount; j++)
                            {
                                _ReligionTemp.Add(output44.GetValue(j).ToString());
                            }
                        }
                        con.Close();
                        var stm45 = "INSERT INTO ludnoscwgwierzen(id_panstwa, id_wiary, liczebnosc) " +
                                    "VALUES(@idPanstwa, @idWiary, @liczebnosc)";
                        var cmd45 = new MySqlCommand(stm45, con);
                        cmd45.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd45.Parameters.AddWithValue("@idWiary", _ReligionTemp[0]);
                        cmd45.Parameters.AddWithValue("@liczebnosc", ReligionResult[i+1]);
                        con.Open();
                        cmd45.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        var stm46 = "UPDATE ludnoscwgwierzen SET liczebnosc = @liczebnosc " +
                                    "WHERE id_panstwa = @idPanstwa AND id_wiary = @idWiary";
                        var cmd46 = new MySqlCommand(stm46, con);
                        cmd46.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd46.Parameters.AddWithValue("@idWiary", (int)output43);
                        cmd46.Parameters.AddWithValue("@liczebnosc",  ReligionResult[i+1]);
                        con.Open();
                        cmd46.ExecuteNonQuery();
                        con.Close();
                    }
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