using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CollegeDatabaseProject.ViewModels;
using HandyControl.Controls;
using MySqlConnector;

namespace CollegeDatabaseProject.Commands;

public class DeleteCountryCommand : CommandBase
{
    private readonly SideBarAdminViewModel _sideBarAdminViewModel;
    private readonly ObservableCollection<string> _countriesAll = new();

    public DeleteCountryCommand(SideBarAdminViewModel sideBarAdminViewModel)
    {
        _sideBarAdminViewModel = sideBarAdminViewModel;
    }
    
    public override void Execute(object? parameter)
    {
        MySqlConnection con = new MySqlConnection(DbConnection.getDbString());
        con.Open();
        MySqlTransaction tr = con.BeginTransaction();
        
        try
        {
            var stm = "Select nazwaPanstwa from panstwo";
            var cmd = new MySqlCommand(stm, con, tr);
            var output = cmd.ExecuteReader();
            while (output.Read())
            {
                for(int i=0; i<output.FieldCount; i++)
                    _countriesAll.Add(output.GetValue(i).ToString());
            }
            output.Close();
            IEnumerable<string?> diffCountries = _countriesAll.Except(_sideBarAdminViewModel.DataList);
            foreach (var diffCountry in diffCountries)
            {
                var stmd1 =
                    "DELETE FROM panstwo WHERE nazwaPanstwa=@nazwaPanstwa";
                var cmdd1 = new MySqlCommand(stmd1, con, tr);
                cmdd1.Parameters.AddWithValue("@nazwaPanstwa", diffCountry);
                cmdd1.ExecuteNonQuery();
            }
            tr.Commit();
            MessageBox.Show("Poprawnie usunięto kraj/e z bazy.");
        }
        catch (Exception ex)
        {
            tr.Rollback();
            MessageBox.Show("Błąd bazy danych");
        }
    }
}