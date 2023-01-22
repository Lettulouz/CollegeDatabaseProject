using CollegeDatabaseProject.ViewModels;

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
        if(!_sideBarAdminViewModel.DataList.Contains("Nowe"))
            _sideBarAdminViewModel.DataList.Add("Nowe");
    }
}