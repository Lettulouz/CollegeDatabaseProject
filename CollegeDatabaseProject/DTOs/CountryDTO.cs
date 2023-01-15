using System;
using System.ComponentModel.DataAnnotations;

namespace CollegeDatabaseProject.DTOs;

public class CountryDTO
{
    [Key]
    public Guid id { get; set; }
    public string countryName { get; set; }
    public string countrySize { get; set; }
    public string countryPopulation { get; set; }
    public string countryAnthem { get; set; }
}