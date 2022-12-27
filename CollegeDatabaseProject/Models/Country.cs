namespace CollegeDatabaseProject.Models;

public class Country
{
    public string Currencies { get; set; }

    public Country(string currency)
    {
        Currencies = currency;
    }
}