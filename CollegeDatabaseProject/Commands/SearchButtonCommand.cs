using CollegeDatabaseProject.ViewModels;
using HandyControl.Controls;
using MySqlConnector;

namespace CollegeDatabaseProject.Commands;

public class SearchButtonCommand : CommandBase
{
    private SideBarViewModel _sideBarViewModel;
    private SideBarAdminViewModel _sideBarAdminViewModel;
    
    public SearchButtonCommand(SideBarViewModel sideBarViewModel)
    {
        _sideBarViewModel = sideBarViewModel;
    }
    public SearchButtonCommand(SideBarAdminViewModel sideBarAdminViewModel)
    {
        _sideBarAdminViewModel = sideBarAdminViewModel;
    }
    
    public override void Execute(object? parameter)
    {
        if (_sideBarViewModel != null)
        {
            MySqlConnection con = new MySqlConnection(DbConnection.getDbString());

            var stm = "Select nazwaPanstwa from panstwo WHERE nazwaPanstwa LIKE @CONTENT";
            var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@CONTENT", "%" + _sideBarViewModel.SearchField + "%");
            con.Open();
            var output = cmd.ExecuteReader();
            _sideBarViewModel.DataList.Clear();
            while (output.Read())
            {
                for (int i = 0; i < output.FieldCount; i++)
                    _sideBarViewModel.DataList.Add(output.GetValue(i).ToString());
            }

            _sideBarViewModel.OnPropChange();
        } else if (_sideBarAdminViewModel != null) {
            MySqlConnection con = new MySqlConnection(DbConnection.getDbString());

            var stm = "Select nazwaPanstwa from panstwo WHERE nazwaPanstwa LIKE @CONTENT";
            var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@CONTENT", "%" + _sideBarAdminViewModel.SearchField + "%");
            con.Open();
            var output = cmd.ExecuteReader();
            _sideBarAdminViewModel.DataList.Clear();
            while (output.Read())
            {
                for (int i = 0; i < output.FieldCount; i++)
                    _sideBarAdminViewModel.DataList.Add(output.GetValue(i).ToString());
            }

            _sideBarAdminViewModel.OnPropChange();
        }
    }
}