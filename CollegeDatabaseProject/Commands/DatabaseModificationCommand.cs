using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CollegeDatabaseProject.ViewModels;
using MySqlConnector;

namespace CollegeDatabaseProject.Commands;

public class DatabaseModificationCommand :CommandBase
{
    private readonly AdminViewModel _adminViewModel;
    private readonly ObservableCollection<string?> _countriesAll = new ();
    private readonly ObservableCollection<string?> _currenciesAll = new ();
    private readonly ObservableCollection<string?> _continentsAll = new ();
    private readonly ObservableCollection<string?> _nationalityAll = new ();
    private readonly ObservableCollection<string?> _capitalAll = new ();
    private readonly ObservableCollection<string?> _langOfficialAll = new ();
    private readonly ObservableCollection<string?> _langForeignAll = new ();
    private readonly ObservableCollection<string?> _faithsAll = new ();
    private readonly ObservableCollection<string?> _nationalityTemp = new ();
    private readonly ObservableCollection<string?> _languageForeignTemp = new ();
    private readonly ObservableCollection<string?> _faithTemp = new ();
    public DatabaseModificationCommand(AdminViewModel adminViewModel)
    {
        _adminViewModel = adminViewModel;
    }

    public override void Execute(object? parameter)
    {
        MySqlConnection con = new MySqlConnection(DbConnection.getDbString());
        con.Open();
        MySqlTransaction tr = con.BeginTransaction();
        /*
         * Pobieranie wszystkich danych z tabeli państwa
         */
        var stm = "Select id from panstwo";
        var cmd = new MySqlCommand(stm, con, tr);
        
        var output = cmd.ExecuteReader();
        // Wczytywanie danych do listy
        _countriesAll.Clear();
        while (output.Read())
        {
            for (int i = 0; i < output.FieldCount; i++)
            {
                _countriesAll.Add(output.GetValue(i).ToString());
            }
        }
        output.Close();
        //==================================

        /*
         * Pobieranie danych z tabeli waluty
         */
        var stm1 = "Select w.skrot from walutypanstwa wp INNER JOIN waluty w ON w.id = wp.id_waluty " +
                   "WHERE wp.id_panstwa=@idPanstwa";
        var cmd1 = new MySqlCommand(stm1, con, tr);
        cmd1.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        var output1 = cmd1.ExecuteReader();
        _currenciesAll.Clear();
        while (output1.Read())
        {
            for (int i = 0; i < output1.FieldCount; i++)
            {
                 _currenciesAll.Add(output1.GetValue(i).ToString());
            }
        }
        output1.Close();
        //==================================
        
        /*
         * Pobieranie danych z tabeli kontynenty
         */
        var stm2 = "Select k.nazwaKontynentu from panstwokontynent AS pk " +
                   "INNER JOIN kontynenty AS k ON pk.id_kontynentu=k.id " +
                   "INNER JOIN panstwo AS p ON p.id=pk.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd2 = new MySqlCommand(stm2, con, tr);
        cmd2.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        var output2 = cmd2.ExecuteReader();
        _continentsAll.Clear();
        while (output2.Read())
        {
            for (int i = 0; i < output2.FieldCount; i++)
            {
                _continentsAll.Add(output2.GetValue(i).ToString());
            }
        }
        output2.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli narodowosci panstwa
        */
        var stm3 = "Select CONCAT(n.nazwa,' - ',ln.liczebnosc,'%') from ludnoscwgnarodowosci AS ln " +
                   "INNER JOIN narodowosc AS n ON ln.id_narodowosci=n.id " +
                   "INNER JOIN panstwo AS p ON p.id=ln.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY ln.liczebnosc DESC";
        var cmd3 = new MySqlCommand(stm3, con, tr);
        cmd3.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        var output3 = cmd3.ExecuteReader();
        _nationalityAll.Clear();
        while (output3.Read())
        {
            for (int i = 0; i < output3.FieldCount; i++)
            {
                _nationalityAll.Add(output3.GetValue(i).ToString());
            }
        }
        output3.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli stolice danego panstwa
        */
        var stm4 = "Select s.nazwa from stolicapanstwa AS sp " +
                   "INNER JOIN stolice AS s ON sp.id_stolicy=s.id " +
                   "INNER JOIN panstwo AS p ON p.id=sp.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd4 = new MySqlCommand(stm4, con, tr);
        cmd4.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        var output4 = cmd4.ExecuteReader();
        _capitalAll.Clear();
        while (output4.Read())
        {
            for (int i = 0; i < output4.FieldCount; i++)
            {
                _capitalAll.Add(output4.GetValue(i).ToString());
            }
        }
        output4.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli jezyk oficjalny panstwa
        */
        var stm5 = "Select j.nazwa from jezykoficjalny AS jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa";
        var cmd5 = new MySqlCommand(stm5, con, tr);
        cmd5.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        var output5 = cmd5.ExecuteReader();
        _langOfficialAll.Clear();
        while (output5.Read())
        {
            for (int i = 0; i < output5.FieldCount; i++)
            {
                _langOfficialAll.Add(output5.GetValue(i).ToString());
            }
        }
        output5.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli jezyk obcy panstwa
        */
        var stm6 = "Select CONCAT(j.nazwa, ' - ', jo.procent, '%') from jezykobcy jo " +
                   "INNER JOIN jezyki AS j ON jo.id_jezyka=j.id " +
                   "INNER JOIN panstwo AS p ON p.id=jo.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY jo.procent DESC";
        var cmd6 = new MySqlCommand(stm6, con, tr);
        cmd6.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        var output6 = cmd6.ExecuteReader();
        _langForeignAll.Clear();
        while (output6.Read())
        {
            for (int i = 0; i < output6.FieldCount; i++)
            {
                _langForeignAll.Add(output6.GetValue(i).ToString());
            }
        }
        output6.Close();
        //==================================
        
        /*
        * Pobieranie danych z tabeli wiara ludnosci
        */
        var stm7 = "Select CONCAT(w.nazwa, ' - ', lw.liczebnosc, '%') from ludnoscwgwierzen AS lw " +
                   "INNER JOIN wiary AS w ON lw.id_wiary=w.id " +
                   "INNER JOIN panstwo AS p ON p.id=lw.id_panstwa " +
                   "WHERE p.id=@idPanstwa ORDER BY lw.liczebnosc DESC";
        var cmd7 = new MySqlCommand(stm7, con, tr);
        cmd7.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
        var output7 = cmd7.ExecuteReader();
        _faithsAll.Clear();
        while (output7.Read())
        {
            for (int i = 0; i < output7.FieldCount; i++)
            {
                _faithsAll.Add(output7.GetValue(i).ToString());
            }
        }
        output7.Close();
        //==================================
        
        var stmu1 = "Select id from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmdu1 = new MySqlCommand(stmu1, con, tr);
        cmdu1.Parameters.AddWithValue("@nazwaPanstwa", _adminViewModel.ChosenCountry);
        var outputu1 = cmdu1.ExecuteScalar();
        if (outputu1 == null)
        {
            var stmu2 = "UPDATE panstwo SET nazwaPanstwa=@nazwaPanstwa WHERE nazwaPanstwa=@nazwaPanstwa2";
            var cmdu2 = new MySqlCommand(stmu2, con, tr);
            cmdu2.Parameters.AddWithValue("@nazwaPanstwa", _adminViewModel.ChosenCountry);
            cmdu2.Parameters.AddWithValue("@nazwaPanstwa2", _adminViewModel.SavedCountryName);
            cmdu2.ExecuteNonQuery();
        }
        
        var stmu3 = "Select id from panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
        var cmdu3 = new MySqlCommand(stmu3, con, tr);
        cmdu3.Parameters.AddWithValue("@nazwaPanstwa", _adminViewModel.ChosenCountry);
        var outputu3 = cmdu3.ExecuteScalar();
        
        
        var stmu11 = "Select id_tytulu from glowapanstwa WHERE id_panstwa=@idPanstwa";
        var cmdu11 = new MySqlCommand(stmu11, con, tr);
        cmdu11.Parameters.AddWithValue("@idPanstwa",(int) outputu3);
        var outputu11 = cmdu11.ExecuteScalar();
        

        if (outputu11 != null)
        {
            var stmu4 = "UPDATE glowapanstwa SET imie=@imie WHERE id_panstwa=@idPanstwa";
            var cmdu4 = new MySqlCommand(stmu4, con, tr);
            cmdu4.Parameters.AddWithValue("@idPanstwa", (int) outputu3);
            cmdu4.Parameters.AddWithValue("@imie", _adminViewModel.HeadOfCountry2);
            cmdu4.ExecuteNonQuery();
        }
        else
        {
            var stmu4 = "INSERT INTO glowapanstwa (imie, id_panstwa, id_tytulu) VALUES (@imie, @idPanstwa, 1)";
            var cmdu4 = new MySqlCommand(stmu4, con, tr);
            cmdu4.Parameters.AddWithValue("@idPanstwa", (int) outputu3);
            cmdu4.Parameters.AddWithValue("@imie", _adminViewModel.HeadOfCountry2);
            cmdu4.ExecuteNonQuery();
        }

        var stmu5 = "UPDATE glowapanstwa SET nazwisko=@nazwisko WHERE id_panstwa=@idPanstwa";
        var cmdu5 = new MySqlCommand(stmu5, con, tr);
        cmdu5.Parameters.AddWithValue("@idPanstwa",(int) outputu3);
        cmdu5.Parameters.AddWithValue("@nazwisko", _adminViewModel.HeadOfCountry3);
        cmdu5.ExecuteNonQuery();
        

        int addNewu = 0;
        var stmu6 = "Select tytul from tytulyglowpanstw WHERE tytul = @tytul";
        var cmdu6 = new MySqlCommand(stmu6, con, tr);
        cmdu6.Parameters.AddWithValue("@tytul", _adminViewModel.HeadOfCountry1);
        var outputu6 = cmdu6.ExecuteScalar();

        if (outputu6 == null)
        {
            addNewu = 1;
        }

        if (addNewu.Equals(1) || !_adminViewModel.HeadOfCountry1.Equals(outputu6.ToString()))
        {
            var stmu7 = "INSERT INTO tytulyglowpanstw(tytul) VALUES(@tytul)";
            var cmdu7 = new MySqlCommand(stmu7, con, tr);
            cmdu7.Parameters.AddWithValue("@tytul", _adminViewModel.HeadOfCountry1);
            cmdu7.ExecuteNonQuery();
            
            long idTytulu = cmdu7.LastInsertedId;
            var stmu8 = "UPDATE glowapanstwa SET id_tytulu=@idTytulu WHERE id_panstwa=@id_panstwa";
            var cmdu8 = new MySqlCommand(stmu8, con, tr);
            cmdu8.Parameters.AddWithValue("@idTytulu", idTytulu);
            cmdu8.Parameters.AddWithValue("@id_panstwa", (int)outputu3);
            cmdu8.ExecuteNonQuery();
            
        }
        else
        {
            var stmu9 = "Select id from tytulyglowpanstw WHERE tytul = @tytul";
            var cmdu9 = new MySqlCommand(stmu9, con, tr);
            cmdu9.Parameters.AddWithValue("@tytul", _adminViewModel.HeadOfCountry1);
            var outputu9 = cmdu9.ExecuteScalar();
            
            var stmu10 = "UPDATE glowapanstwa SET id_tytulu=@id_tytulu WHERE id_panstwa=@id_panstwa";
            var cmdu10 = new MySqlCommand(stmu10, con, tr);
            cmdu10.Parameters.AddWithValue("@id_tytulu", (int)outputu9);
            cmdu10.Parameters.AddWithValue("@id_panstwa", (int)outputu3);
            cmdu10.ExecuteNonQuery();
        }
        
            var stmu14 = "UPDATE panstwo SET ludnosc=@ludnosc, obszar=@obszar, hymn=@hymn WHERE id=@idPanstwa";
            var cmdu14 = new MySqlCommand(stmu14, con, tr);
            cmdu14.Parameters.AddWithValue("@idPanstwa", (int)outputu3);
            cmdu14.Parameters.AddWithValue("@ludnosc", _adminViewModel.Population);
            cmdu14.Parameters.AddWithValue("@obszar", _adminViewModel.Territory);
            cmdu14.Parameters.AddWithValue("@hymn", _adminViewModel.Anthem);
            cmdu14.ExecuteNonQuery();

            //==================================
        
        // Pobranie danych z widoku dla walut
        IEnumerable<string?> diffCurrencies = _adminViewModel.CurrenciesInCountry.Except(_currenciesAll);
        /*
        * Wstawianie danych walut do tabeli statycznej i łączącej
        */
        var result = diffCurrencies.ToList();
        int addNew = 0;
        foreach (var item in result)
        {
            var stm10 = "Select skrot from waluty WHERE skrot = @skrotWaluty";
            var cmd10 = new MySqlCommand(stm10, con, tr);
            cmd10.Parameters.AddWithValue("@skrotWaluty", item);
            var output10 = cmd10.ExecuteScalar();
            addNew = 0;
                if (output10 == null)
                {
                    addNew = 1;
                }

                if (addNew.Equals(1) || !item.Equals(output10.ToString()))
                {
                    var stm8 = "INSERT INTO waluty(skrot) VALUES(@skrotWaluty)";
                    var cmd8 = new MySqlCommand(stm8, con, tr);
                    cmd8.Parameters.AddWithValue("@skrotWaluty", item);
                    cmd8.ExecuteNonQuery();
                    
                    long currency = cmd8.LastInsertedId;
                    var stm9 = "INSERT INTO walutypanstwa(id_panstwa, id_waluty) VALUES(@idPanstwa, @idCurrency)";
                    var cmd9 = new MySqlCommand(stm9, con, tr);
                    cmd9.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd9.Parameters.AddWithValue("@idCurrency", currency);
                    cmd9.ExecuteNonQuery();
                }
                else
                {
                    var stm11 = "Select id from waluty WHERE skrot = @skrotWaluty";
                    var cmd11 = new MySqlCommand(stm11, con, tr);
                    cmd11.Parameters.AddWithValue("@skrotWaluty", item);
                    var output11 = cmd11.ExecuteScalar();
                    
                    var stm12 = "INSERT INTO walutypanstwa(id_panstwa, id_waluty) VALUES(@idPanstwa, @idCurrency)";
                    var cmd12 = new MySqlCommand(stm12, con, tr);
                    cmd12.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd12.Parameters.AddWithValue("@idCurrency", output11);
                    cmd12.ExecuteNonQuery();
                }
        }
        
        // Usuwanie
        
        var currenciesToDelete = _currenciesAll.Except(_adminViewModel.CurrenciesInCountry);
        foreach (var currencyToDelete in currenciesToDelete)
        {
            var stmd11 =
                "SELECT w.id FROM walutypanstwa wp INNER JOIN waluty w ON wp.id_waluty=w.id " +
                "INNER JOIN panstwo p ON wp.id_panstwa=p.id WHERE wp.id_panstwa=@idPanstwa AND w.skrot=@nazwaWaluty";
            var cmdd11 = new MySqlCommand(stmd11, con, tr);
            cmdd11.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd11.Parameters.AddWithValue("@nazwaWaluty", currencyToDelete);
            var idWaluty = cmdd11.ExecuteScalar();

            var stmd12 =
                "DELETE FROM walutypanstwa WHERE id_panstwa = @idPanstwa AND id_waluty = @idWaluty";
            var cmdd12 = new MySqlCommand(stmd12, con, tr);
            cmdd12.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd12.Parameters.AddWithValue("@idWaluty", idWaluty);
            cmdd12.ExecuteNonQuery();
        }
        
        //==================================
        
        // Pobranie danych z widoku dla kontynetów
        IEnumerable<string?> diffContinents = _adminViewModel.CountryOnContinents.Except(_continentsAll);
        /*
        * Wstawianie danych kontynetów do tabeli statycznej i łączącej
        */
        var result1 = diffContinents.ToList();
        int addNew1 = 0;
        foreach (var item in result1)
        {
            var stm13 = "Select nazwaKontynentu from kontynenty WHERE nazwaKontynentu = @nazwaKontynentu";
            var cmd13 = new MySqlCommand(stm13, con, tr);
            cmd13.Parameters.AddWithValue("@nazwaKontynentu", item);
            var output13 = cmd13.ExecuteScalar();
            addNew1 = 0;
                if (output13 == null)
                {
                    addNew1 = 1;
                }

                if (addNew1.Equals(1) || !item.Equals(output13.ToString()))
                {
                    var stm14 = "INSERT INTO kontynenty(nazwaKontynentu) VALUES(@nazwaKontynentu)";
                    var cmd14 = new MySqlCommand(stm14, con, tr);
                    cmd14.Parameters.AddWithValue("@nazwaKontynentu", item);
                    cmd14.ExecuteNonQuery();
                    
                    long continent = cmd14.LastInsertedId;
                    var stm15 = "INSERT INTO panstwokontynent(id_panstwa, id_kontynentu) VALUES(@idPanstwa, @idKontynentu)";
                    var cmd15 = new MySqlCommand(stm15, con, tr);
                    cmd15.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd15.Parameters.AddWithValue("@idKontynentu", continent);
                    cmd15.ExecuteNonQuery();
                }
                else
                {
                    var stm16 = "Select id from kontynenty WHERE nazwaKontynentu = @nazwaKontynentu";
                    var cmd16 = new MySqlCommand(stm16, con, tr);
                    cmd16.Parameters.AddWithValue("@nazwaKontynentu", item);
                    var output16 = cmd16.ExecuteScalar();
                    
                    var stm17 = "INSERT INTO panstwokontynent(id_panstwa, id_kontynentu) VALUES(@idPanstwa, @idKontynentu)";
                    var cmd17 = new MySqlCommand(stm17, con, tr);
                    cmd17.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd17.Parameters.AddWithValue("@idKontynentu", output16);
                    cmd17.ExecuteNonQuery();
                    
                }
        }
        
        // Usuwanie
        
        var continentsToDelete = _continentsAll.Except(_adminViewModel.CountryOnContinents);
        foreach (var continentToDelete in continentsToDelete)
        {
            var stmd21 =
                "SELECT k.id FROM panstwokontynent pk INNER JOIN kontynenty k ON pk.id_kontynentu=k.id " +
                "INNER JOIN panstwo p ON pk.id_panstwa=p.id WHERE pk.id_panstwa=@idPanstwa AND k.nazwaKontynentu=@nazwaKontynentu";
            var cmdd21 = new MySqlCommand(stmd21, con, tr);
            cmdd21.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd21.Parameters.AddWithValue("@nazwaKontynentu", continentToDelete);
            var idKontynentu = cmdd21.ExecuteScalar();
            
            var stmd22 =
                "DELETE FROM panstwokontynent WHERE id_panstwa = @idPanstwa AND id_kontynentu = @idKontynentu";
            var cmdd22 = new MySqlCommand(stmd22, con, tr);
            cmdd22.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd22.Parameters.AddWithValue("@idKontynentu", idKontynentu);
            cmdd22.ExecuteNonQuery();
            
        }
        
        //==================================
        
        // Pobranie danych z widoku dla populacji wg narodowości
        IEnumerable<string?> diffNationality = _adminViewModel.PopulationByNationality.Except(_nationalityAll);
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
            var cmd18 = new MySqlCommand(stm18, con, tr);
            cmd18.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
            var output18 = cmd18.ExecuteScalar();
            
            addNew2 = 0;
                if (output18 == null)
                {
                    addNew2 = 1;
                }
                if (addNew2.Equals(1) || !NationalityResult[i].Equals(output18.ToString()))
                {
                    var stm19 = "INSERT INTO narodowosc(nazwa) VALUES(@nazwaNarodowosci)";
                    var cmd19 = new MySqlCommand(stm19, con, tr);
                    cmd19.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
                    cmd19.ExecuteNonQuery();
                    
                    long nationality = cmd19.LastInsertedId;
                    var stm20 = "INSERT INTO ludnoscwgnarodowosci(id_panstwa, id_narodowosci, liczebnosc) " +
                                "VALUES(@idPanstwa, @idNarodowosci, @liczebnosc)";
                    var cmd20 = new MySqlCommand(stm20, con, tr);
                    cmd20.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd20.Parameters.AddWithValue("@idNarodowosci", nationality);
                    cmd20.Parameters.AddWithValue("@liczebnosc", NationalityResult[i+1]);
                    cmd20.ExecuteNonQuery();
                }
                else
                {
                    var stm50 = "Select ln.id_narodowosci from ludnoscwgnarodowosci ln " +
                                "INNER JOIN narodowosc n ON n.id = ln.id_narodowosci " +
                                "WHERE ln.id_panstwa=@idPanstwa AND n.nazwa = @nazwaNarodowosci";
                    var cmd50 = new MySqlCommand(stm50, con, tr);
                    cmd50.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
                    cmd50.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    var output50 = cmd50.ExecuteScalar();
                    
                    if (output50 == null)
                    {
                        var stm51 = "Select id from narodowosc WHERE nazwa = @nazwaNarodowosci";
                        var cmd51 = new MySqlCommand(stm51, con, tr);
                        cmd51.Parameters.AddWithValue("@nazwaNarodowosci", NationalityResult[i]);
                        var output51 = cmd51.ExecuteReader();
                        _nationalityTemp.Clear();
                        while (output51.Read())
                        {
                            for (int j = 0; j < output51.FieldCount; j++)
                            {
                                _nationalityTemp.Add(output51.GetValue(j).ToString());
                            }
                        }
                        output51.Close();
                        
                        var stm52 = "INSERT INTO ludnoscwgnarodowosci(id_panstwa, id_narodowosci, liczebnosc) " +
                                    "VALUES(@idPanstwa, @idNarodowosci, @liczebnosc)";
                        var cmd52 = new MySqlCommand(stm52, con, tr);
                        cmd52.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd52.Parameters.AddWithValue("@idNarodowosci", _nationalityTemp[0]);
                        cmd52.Parameters.AddWithValue("@liczebnosc", NationalityResult[i+1]);
                        cmd52.ExecuteNonQuery();
                    }
                    else
                    {
                        var stm53 = "UPDATE ludnoscwgnarodowosci SET liczebnosc = @liczebnosc " +
                                    "WHERE id_panstwa = @idPanstwa AND id_narodowosci = @idNarodowosci";
                        var cmd53 = new MySqlCommand(stm53, con, tr);
                        cmd53.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd53.Parameters.AddWithValue("@idNarodowosci", (int)output50);
                        cmd53.Parameters.AddWithValue("@liczebnosc",  NationalityResult[i+1]);
                        cmd53.ExecuteNonQuery();
                    }
                }
        }
        
        // Usuwanie
        
        var tempNationalitiesToDelete = _nationalityAll.Except(_adminViewModel.PopulationByNationality);
                List<string?> nationalitiesToDelete = new();
                foreach (var nationalityToDelete in tempNationalitiesToDelete)
                {
                    nationalitiesToDelete.Add(nationalityToDelete.Split(" - ")[0]);
                }
        	foreach (var nationalityToDelete in nationalitiesToDelete)
                {
                    var stmd31 =
                        "SELECT n.id FROM ludnoscwgnarodowosci lwn INNER JOIN narodowosc n ON lwn.id_narodowosci=n.id " +
                        "INNER JOIN panstwo p ON lwn.id_panstwa=p.id WHERE lwn.id_panstwa=@idPanstwa AND n.nazwa=@nazwaNarodowosci";
                    var cmdd31 = new MySqlCommand(stmd31, con, tr);
                    cmdd31.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmdd31.Parameters.AddWithValue("@nazwaNarodowosci", nationalityToDelete);
                    var idNarodowosci = cmdd31.ExecuteScalar();

                    var stmd32 =
                        "DELETE FROM ludnoscwgnarodowosci WHERE id_panstwa = @idPanstwa AND id_narodowosci = @idNarodowosci";
                    var cmdd32 = new MySqlCommand(stmd32, con, tr);
                    cmdd32.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmdd32.Parameters.AddWithValue("@idNarodowosci", idNarodowosci);
                    cmdd32.ExecuteNonQuery();
                }
        
        //==================================
        
        // Pobranie danych z widoku dla stolic
        IEnumerable<string?> diffCapitals = _adminViewModel.CapitalsOfCountry.Except(_capitalAll);
        /*
        * Wstawianie danych stolic do tabeli statycznej i łączącej
        */
        var result3 = diffCapitals.ToList();
        int addNew3 = 0;
        foreach (var item in result3)
        {
            var stm23 = "Select nazwa from stolice WHERE nazwa = @nazwaStolicy";
            var cmd23 = new MySqlCommand(stm23, con, tr);
            cmd23.Parameters.AddWithValue("@nazwaStolicy", item);
            var output23 = cmd23.ExecuteScalar();
            
            addNew3 = 0;
                if (output23 == null)
                {
                    addNew3 = 1;
                }

                if (addNew3.Equals(1) || !item.Equals(output23.ToString()))
                {
                    var stm24 = "INSERT INTO stolice(nazwa) VALUES(@nazwaStolicy)";
                    var cmd24 = new MySqlCommand(stm24, con, tr);
                    cmd24.Parameters.AddWithValue("@nazwaStolicy", item);
                    cmd24.ExecuteNonQuery();
                    
                    long capital = cmd24.LastInsertedId;
                    var stm25 = "INSERT INTO stolicapanstwa(id_panstwa, id_stolicy) VALUES(@idPanstwa, @idStolicy)";
                    var cmd25 = new MySqlCommand(stm25, con, tr);
                    cmd25.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd25.Parameters.AddWithValue("@idStolicy", capital);
                    cmd25.ExecuteNonQuery();
                }
                else
                {
                    var stm26 = "Select id from stolice WHERE nazwa = @nazwaStolicy";
                    var cmd26 = new MySqlCommand(stm26, con, tr);
                    cmd26.Parameters.AddWithValue("@nazwaStolicy", item);
                    var output26 = cmd26.ExecuteScalar();
                    
                    var stm27 = "INSERT INTO stolicapanstwa(id_panstwa, id_stolicy) VALUES(@idPanstwa, @idStolicy)";
                    var cmd27 = new MySqlCommand(stm27, con, tr);
                    cmd27.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd27.Parameters.AddWithValue("@idStolicy", output26);
                    cmd27.ExecuteNonQuery();
                }
        }
        
        // Usuwanie
        
        var capitalsToDelete = _capitalAll.Except(_adminViewModel.CapitalsOfCountry);
        foreach (var capitalToDelete in capitalsToDelete)
        {
            var stmd41 =
                "SELECT s.id FROM stolicapanstwa sp INNER JOIN stolice s ON sp.id_stolicy=s.id " +
                "INNER JOIN panstwo p ON sp.id_panstwa=p.id WHERE sp.id_panstwa=@idPanstwa AND s.nazwa=@nazwaStolicy";
            var cmdd41 = new MySqlCommand(stmd41, con, tr);
            cmdd41.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd41.Parameters.AddWithValue("@nazwaStolicy", capitalToDelete);
            var idStolicy = cmdd41.ExecuteScalar();
            
            var stmd42 =
                "DELETE FROM stolicapanstwa WHERE id_panstwa = @idPanstwa AND id_stolicy = @idStolicy";
            var cmdd42 = new MySqlCommand(stmd42, con, tr);
            cmdd42.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd42.Parameters.AddWithValue("@idStolicy", idStolicy);
            cmdd42.ExecuteNonQuery();
        }
        
        //==================================
        
        // Pobranie danych z widoku dla języka oficialnego
        IEnumerable<string?> diffLanguageOff = _adminViewModel.OfficialLanguages.Except(_langOfficialAll);
        /*
        * Wstawianie danych języka oficialnego do tabeli statycznej i łączącej
        */
        var result4 = diffLanguageOff.ToList();
        int addNew4 = 0;
        foreach (var item in result4)
        {
            var stm28 = "Select nazwa from jezyki WHERE nazwa = @nazwaJezykaOff";
            var cmd28 = new MySqlCommand(stm28, con, tr);
            cmd28.Parameters.AddWithValue("@nazwaJezykaOff", item);
            var output28 = cmd28.ExecuteScalar();
            
            addNew4 = 0;
                if (output28 == null)
                {
                    addNew4 = 1;
                }

                if (addNew4.Equals(1) || !item.Equals(output28.ToString()))
                {
                    var stm29 = "INSERT INTO jezyki(nazwa) VALUES(@nazwaJezykaOff)";
                    var cmd29 = new MySqlCommand(stm29, con, tr);
                    cmd29.Parameters.AddWithValue("@nazwaJezykaOff", item);
                    cmd29.ExecuteNonQuery();
                    
                    long LanguageOfficial = cmd29.LastInsertedId;
                    var stm30 = "INSERT INTO jezykoficjalny(id_panstwa, id_jezyka) VALUES(@idPanstwa, @idJezykaOff)";
                    var cmd30 = new MySqlCommand(stm30, con, tr);
                    cmd30.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd30.Parameters.AddWithValue("@idJezykaOff", LanguageOfficial);
                    cmd30.ExecuteNonQuery();
                }
                else
                {
                    var stm31 = "Select id from jezyki WHERE nazwa = @nazwaJezykaOff";
                    var cmd31 = new MySqlCommand(stm31, con, tr);
                    cmd31.Parameters.AddWithValue("@nazwaJezykaOff", item);
                    var output31 = cmd31.ExecuteScalar();
                    
                    var stm32 = "INSERT INTO jezykoficjalny(id_panstwa, id_jezyka) VALUES(@idPanstwa, @idJezykaOff)";
                    var cmd32 = new MySqlCommand(stm32, con, tr);
                    cmd32.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd32.Parameters.AddWithValue("@idJezykaOff", output31);
                    cmd32.ExecuteNonQuery();
                }
        }
        
        // Usuwanie
        
        var officialLanguagesToDelete = _langOfficialAll.Except(_adminViewModel.OfficialLanguages);
                foreach (var officialLanguageToDelete in officialLanguagesToDelete)
                {
                    var stmd51 =
                        "SELECT j.id FROM jezykoficjalny jo INNER JOIN jezyki j ON jo.id_jezyka=j.id " +
                        "INNER JOIN panstwo p ON jo.id_panstwa=p.id WHERE jo.id_panstwa=@idPanstwa AND j.nazwa=@nazwaJezyka";
                    var cmdd51 = new MySqlCommand(stmd51, con, tr);
                    cmdd51.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmdd51.Parameters.AddWithValue("@nazwaJezyka", officialLanguageToDelete);
                    var idJezyka = cmdd51.ExecuteScalar();

                    var stmd52 =
                        "DELETE FROM jezykoficjalny WHERE id_panstwa = @idPanstwa AND id_jezyka = @idJezyka";
                    var cmdd52 = new MySqlCommand(stmd52, con, tr);
                    cmdd52.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmdd52.Parameters.AddWithValue("@idJezyka", idJezyka);
                    cmdd52.ExecuteNonQuery();
                    
                }
        
        //==================================
        
        // Pobranie danych z widoku dla języka obcego
        IEnumerable<string?> diffLanguageFore = _adminViewModel.ForeignLanguages.Except(_langForeignAll);
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
            var cmd33 = new MySqlCommand(stm33, con, tr);
            cmd33.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
            var output33 = cmd33.ExecuteScalar();
            
            addNew5 = 0;
                if (output33 == null)
                {
                    addNew5 = 1;
                }
                if (addNew5.Equals(1) || !LanguageForeResult[i].Equals(output33.ToString()))
                {
                    var stm34 = "INSERT INTO jezyki(nazwa) VALUES(@nazwaJezykaFore)";
                    var cmd34 = new MySqlCommand(stm34, con, tr);
                    cmd34.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
                    cmd34.ExecuteNonQuery();
                    
                    long nationality = cmd34.LastInsertedId;
                    var stm35 = "INSERT INTO jezykobcy(id_panstwa, id_jezyka, procent) " +
                                "VALUES(@idPanstwa, @idJezykaFore, @procent)";
                    var cmd35 = new MySqlCommand(stm35, con, tr);
                    cmd35.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd35.Parameters.AddWithValue("@idJezykaFore", nationality);
                    cmd35.Parameters.AddWithValue("@procent", LanguageForeResult[i+1]);
                    cmd35.ExecuteNonQuery();
                }
                else
                {
                    var stm36 = "Select jb.id_jezyka from jezykobcy jb " +
                                "INNER JOIN jezyki j ON j.id = jb.id_jezyka " +
                                "WHERE jb.id_panstwa=@idPanstwa AND j.nazwa = @nazwaJezykaFore";
                    var cmd36 = new MySqlCommand(stm36, con, tr);
                    cmd36.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
                    cmd36.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    var output36 = cmd36.ExecuteScalar();
                    
                    if (output36 == null)
                    {
                        var stm38 = "Select id from jezyki WHERE nazwa = @nazwaJezykaFore";
                        var cmd38 = new MySqlCommand(stm38, con, tr);
                        cmd38.Parameters.AddWithValue("@nazwaJezykaFore", LanguageForeResult[i]);
                        
                        var output38 = cmd38.ExecuteReader();
                        _languageForeignTemp.Clear();
                        while (output38.Read())
                        {
                            for (int j = 0; j < output38.FieldCount; j++)
                            {
                                _languageForeignTemp.Add(output38.GetValue(j).ToString());
                            }
                        }
                        output38.Close();
                        
                        var stm39 = "INSERT INTO jezykobcy(id_panstwa, id_jezyka, procent) " +
                                    "VALUES(@idPanstwa, @idJezykaFore, @procent)";
                        var cmd39 = new MySqlCommand(stm39, con, tr);
                        cmd39.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd39.Parameters.AddWithValue("@idJezykaFore", _languageForeignTemp[0]);
                        cmd39.Parameters.AddWithValue("@procent", LanguageForeResult[i+1]);
                        cmd39.ExecuteNonQuery();
                    }
                    else
                    {
                        var stm37 = "UPDATE jezykobcy SET procent = @procent " +
                                    "WHERE id_panstwa = @idPanstwa AND id_jezyka = @idJezykaFore";
                        var cmd37 = new MySqlCommand(stm37, con, tr);
                        cmd37.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd37.Parameters.AddWithValue("@idJezykaFore", (int)output36);
                        cmd37.Parameters.AddWithValue("@procent",  LanguageForeResult[i+1]);
                        cmd37.ExecuteNonQuery();
                    }
                }
        }
        
        // Usuwanie
        
        var tempForeignLanguagesToDelete = _langForeignAll.Except(_adminViewModel.ForeignLanguages);
        List<string?> foreignLanguagesToDelete = new();
        foreach (var foreignLanguageToDelete in tempForeignLanguagesToDelete)
        {
            foreignLanguagesToDelete.Add(foreignLanguageToDelete.Split(" - ")[0]);
        }
        foreach (var foreignLanguageToDelete in foreignLanguagesToDelete)
        {
            var stmd61 =
                "SELECT j.id FROM jezykobcy jo INNER JOIN jezyki j ON jo.id_jezyka=j.id " +
                "INNER JOIN panstwo p ON jo.id_panstwa=p.id WHERE jo.id_panstwa=@idPanstwa AND j.nazwa=@nazwaJezyka";
            var cmdd61 = new MySqlCommand(stmd61, con, tr);
            cmdd61.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd61.Parameters.AddWithValue("@nazwaJezyka", foreignLanguageToDelete);
            var idJezyka = cmdd61.ExecuteScalar();

            var stmd62 =
                "DELETE FROM jezykobcy WHERE id_panstwa = @idPanstwa AND id_jezyka = @idJezyka";
            var cmdd62 = new MySqlCommand(stmd62, con, tr);
            cmdd62.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd62.Parameters.AddWithValue("@idJezyka", idJezyka);
            cmdd62.ExecuteNonQuery();
            
        }
        
        //==================================
        
        // Pobranie danych z widoku dla ludnosci wg wiary
        IEnumerable<string?> diffReligion = _adminViewModel.PopulationByFaith.Except(_faithsAll);
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
            var cmd40 = new MySqlCommand(stm40, con, tr);
            cmd40.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
            var output40 = cmd40.ExecuteScalar();
            
            addNew6 = 0;
                if (output40 == null)
                {
                    addNew6 = 1;
                }
                if (addNew6.Equals(1) || !ReligionResult[i].Equals(output40.ToString()))
                {
                    var stm41 = "INSERT INTO wiary(nazwa) VALUES(@nazwaWiary)";
                    var cmd41 = new MySqlCommand(stm41, con, tr);
                    cmd41.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
                    cmd41.ExecuteNonQuery();
                    
                    long religion = cmd41.LastInsertedId;
                    var stm42 = "INSERT INTO ludnoscwgwierzen(id_panstwa, id_wiary, liczebnosc) " +
                                "VALUES(@idPanstwa, @idWiary, @liczebnosc)";
                    var cmd42 = new MySqlCommand(stm42, con, tr);
                    cmd42.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    cmd42.Parameters.AddWithValue("@idWiary", religion);
                    cmd42.Parameters.AddWithValue("@liczebnosc", ReligionResult[i+1]);
                    cmd42.ExecuteNonQuery();
                }
                else
                {
                    var stm43 = "Select lw.id_wiary from ludnoscwgwierzen lw " +
                                "INNER JOIN wiary w ON w.id = lw.id_wiary " +
                                "WHERE lw.id_panstwa=@idPanstwa AND w.nazwa = @nazwaWiary";
                    var cmd43 = new MySqlCommand(stm43, con, tr);
                    cmd43.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
                    cmd43.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                    var output43 = cmd43.ExecuteScalar();
                    
                    if (output43 == null)
                    {
                        var stm44 = "Select id from wiary WHERE nazwa = @nazwaWiary";
                        var cmd44 = new MySqlCommand(stm44, con, tr);
                        cmd44.Parameters.AddWithValue("@nazwaWiary", ReligionResult[i]);
                        var output44 = cmd44.ExecuteReader();
                        _faithTemp.Clear();
                        while (output44.Read())
                        {
                            for (int j = 0; j < output44.FieldCount; j++)
                            {
                                _faithTemp.Add(output44.GetValue(j).ToString());
                            }
                        }
                        output44.Close();
                        
                        var stm45 = "INSERT INTO ludnoscwgwierzen(id_panstwa, id_wiary, liczebnosc) " +
                                    "VALUES(@idPanstwa, @idWiary, @liczebnosc)";
                        var cmd45 = new MySqlCommand(stm45, con, tr);
                        cmd45.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd45.Parameters.AddWithValue("@idWiary", _faithTemp[0]);
                        cmd45.Parameters.AddWithValue("@liczebnosc", ReligionResult[i+1]);
                        cmd45.ExecuteNonQuery();
                    }
                    else
                    {
                        var stm46 = "UPDATE ludnoscwgwierzen SET liczebnosc = @liczebnosc " +
                                    "WHERE id_panstwa = @idPanstwa AND id_wiary = @idWiary";
                        var cmd46 = new MySqlCommand(stm46, con, tr);
                        cmd46.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
                        cmd46.Parameters.AddWithValue("@idWiary", (int)output43);
                        cmd46.Parameters.AddWithValue("@liczebnosc",  ReligionResult[i+1]);
                        cmd46.ExecuteNonQuery();
                    }
                }
        }
        
        // Usuwanie
        
        var tempFaithsToDelete = _faithsAll.Except(_adminViewModel.PopulationByFaith);
        List<string?> faithsToDelete = new();
        foreach (var faithToDelete in tempFaithsToDelete)
        {
            faithsToDelete.Add(faithToDelete.Split(" - ")[0]);
        }
        foreach (var faithToDelete in faithsToDelete)
        {
            var stmd71 =
                "SELECT w.id FROM ludnoscwgwierzen lww INNER JOIN wiary w ON lww.id_wiary=w.id " +
                "INNER JOIN panstwo p ON lww.id_panstwa=p.id WHERE lww.id_panstwa=@idPanstwa AND w.nazwa=@nazwaWiary";
            var cmdd71 = new MySqlCommand(stmd71, con, tr);
            cmdd71.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd71.Parameters.AddWithValue("@nazwaWiary", faithToDelete);
            var idWiary = cmdd71.ExecuteScalar();

            var stmd72 =
                "DELETE FROM ludnoscwgwierzen WHERE id_panstwa = @idPanstwa AND id_wiary = @idWiary";
            var cmdd72 = new MySqlCommand(stmd72, con, tr);
            cmdd72.Parameters.AddWithValue("@idPanstwa", _adminViewModel.IdChosenCountry);
            cmdd72.Parameters.AddWithValue("@idWiary", idWiary);
            cmdd72.ExecuteNonQuery();
        }
        tr.Commit();
        con.Close();
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