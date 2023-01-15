namespace CollegeDatabaseProject.Models;

public class Country
{
    public string Currencies { get; }

    public Country(string currency)
    {
        Currencies = currency;
    }
}