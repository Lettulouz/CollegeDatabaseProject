using CollegeDatabaseProject.ViewModels;
using HandyControl.Controls;
using MySqlConnector;

namespace CollegeDatabaseProject.Commands;

public class SearchButtonCommand : CommandBase
{
    private SideBarViewModel _sideBarViewModel;
    
    public SearchButtonCommand(SideBarViewModel sideBarViewModel)
    {
        _sideBarViewModel = sideBarViewModel;
    }
    
    public override void Execute(object? parameter)
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
            for(int i=0; i<output.FieldCount; i++)
                _sideBarViewModel.DataList.Add(output.GetValue(i).ToString());
        }

        _sideBarViewModel.OnPropChange();
    }
}