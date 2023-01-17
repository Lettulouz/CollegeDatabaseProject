using System;
using System.Collections.Generic;
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

    private List<string> test = new();
    
    public override void Execute(object? parameter)
    {
        //test.Add("Chuj");
       // test.Add("Jd");
       // _homePageViewModel.DataList.Add(new Country("PLN", test));
        //_homePageViewModel.DataList.Add(new Country("EUR", test));
       // _homePageViewModel.OnPropChan();
    }
}