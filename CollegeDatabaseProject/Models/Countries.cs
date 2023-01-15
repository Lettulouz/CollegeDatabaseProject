using System.Collections.Generic;

namespace CollegeDatabaseProject.Models;

public class Countries
{
    private readonly List<Country> _countries;

    public Countries()
    {
        _countries = new List<Country>();
    }
}