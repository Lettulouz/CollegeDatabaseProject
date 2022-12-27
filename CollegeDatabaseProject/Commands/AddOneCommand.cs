using System;
using CollegeDatabaseProject.Models;
using CollegeDatabaseProject.ViewModels;
using HandyControl.Tools.Extension;
using Microsoft.VisualBasic.CompilerServices;

namespace CollegeDatabaseProject.Commands;

public class AddOneCommand : CommandBase
{
    private HomePageViewModel _homePageViewModel;
    
    public AddOneCommand(HomePageViewModel homePageViewModel)
    {
        _homePageViewModel = homePageViewModel;
    }
    
    public override void Execute(object? parameter)
    {
        _homePageViewModel.DataList.Add(new Country("PLN"));
        _homePageViewModel.DataList.Add(new Country("EUR"));
        _homePageViewModel.OnPropChan();
    }
}