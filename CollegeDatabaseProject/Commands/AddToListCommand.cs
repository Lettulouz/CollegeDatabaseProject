using CollegeDatabaseProject.ViewModels;
using HandyControl.Controls;

namespace CollegeDatabaseProject.Commands;

public class AddToListCommand : CommandBase
{
    private AdminViewModel _adminViewModel;
    public AddToListCommand(AdminViewModel adminViewModel)
    {
        _adminViewModel = adminViewModel;
    }
    public override void Execute(object? parameter)
    {
        //MessageBox.Show(parameter.ToString());
        // code
        if (_adminViewModel.ChosenCountry != "")
        {
            string paramVal = parameter.ToString();
            switch (paramVal)
            {
                case "1": 
                    if(_adminViewModel.TextInput1 != "")
                        _adminViewModel.CurrenciesInCountry.Add(_adminViewModel.TextInput1);
                    break;
                case "2": 
                    if(_adminViewModel.TextInput2 != "")
                        _adminViewModel.CountryOnContinents.Add(_adminViewModel.TextInput2);
                    break;
                case "3": 
                    if(_adminViewModel.TextInput3 != "")
                        _adminViewModel.PopulationByNationality.Add(_adminViewModel.TextInput3);
                    break;
                case "4": 
                    if(_adminViewModel.TextInput4 != "")
                        _adminViewModel.CapitalsOfCountry.Add(_adminViewModel.TextInput4);
                    break;
                case "5": 
                    if(_adminViewModel.TextInput5 != "")
                        _adminViewModel.OfficialLanguages.Add(_adminViewModel.TextInput5);
                    break;
                case "6": 
                    if(_adminViewModel.TextInput6 != "")
                        _adminViewModel.ForeignLanguages.Add(_adminViewModel.TextInput6);
                    break;
                case "7": 
                    if(_adminViewModel.TextInput7!= "")
                        _adminViewModel.PopulationByFaith.Add(_adminViewModel.TextInput7);
                    break;
            }
        }
    }
}