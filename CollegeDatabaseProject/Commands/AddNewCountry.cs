using System;
using CollegeDatabaseProject.ViewModels;
using HandyControl.Controls;
using MySqlConnector;

namespace CollegeDatabaseProject.Commands;

public class AddNewCountry : CommandBase
{
    private SideBarAdminViewModel _sideBarAdminViewModel;
    public AddNewCountry(SideBarAdminViewModel sideBarAdminViewModel)
    {
        _sideBarAdminViewModel = sideBarAdminViewModel;
    }
    
    public override void Execute(object? parameter)
    {
        _sideBarAdminViewModel.ReadFromDatabase();
        if (!_sideBarAdminViewModel.DataList.Contains("Nowe"))
        {
            MySqlConnection con = new MySqlConnection(DbConnection.getDbString());
            con.Open();
            MySqlTransaction tr = con.BeginTransaction();
            try
            {
                var stmu1 = "INSERT INTO panstwo (nazwaPanstwa) VALUES ('Nowe')";
                var cmdu1 = new MySqlCommand(stmu1, con, tr);
                cmdu1.ExecuteNonQuery();
                tr.Commit();
                _sideBarAdminViewModel.DataList.Add("Nowe");
                _sideBarAdminViewModel.SelectedCountry = "Nowe";
            }
            catch (Exception ex)
            {
                tr.Rollback();
                MessageBox.Show("Błąd bazy danych");
            }
        }
    }
}