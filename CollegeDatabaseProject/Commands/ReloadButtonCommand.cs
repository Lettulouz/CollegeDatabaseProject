using CollegeDatabaseProject.ViewModels;
using HandyControl.Controls;
using MySqlConnector;

namespace CollegeDatabaseProject.Commands;

public class ReloadButtonCommand : CommandBase
{
    private SideBarViewModel _sideBarViewModel;
    
    public ReloadButtonCommand(SideBarViewModel sideBarViewModel)
    {
        _sideBarViewModel = sideBarViewModel;
    }
    
    public override void Execute(object? parameter)
    {
        MySqlConnection con = new MySqlConnection(DbConnection.getDbString());

        var stm = "Select nazwaPanstwa from panstwo";
        var cmd = new MySqlCommand(stm, con);
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