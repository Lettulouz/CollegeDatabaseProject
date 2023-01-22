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
                    if(_adminViewModel.TextInput31 != "" && _adminViewModel.TextInput31 != null &&  _adminViewModel.TextInput32 != null)
                        _adminViewModel.PopulationByNationality.Add(_adminViewModel.TextInput31 + " - " + _adminViewModel.TextInput32 + "%");
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
                    if(_adminViewModel.TextInput61 != "" && _adminViewModel.TextInput61 != null && _adminViewModel.TextInput62 != null)
                        _adminViewModel.ForeignLanguages.Add(_adminViewModel.TextInput61 + " - " + _adminViewModel.TextInput62 + "%");
                    break;
                case "7": 
                    if(_adminViewModel.TextInput71 != "" && _adminViewModel.TextInput71 != null && _adminViewModel.TextInput72 != null)
                        _adminViewModel.PopulationByFaith.Add(_adminViewModel.TextInput71 + " - " + _adminViewModel.TextInput72 + "%");
                    break;
            }
        }
    }
}