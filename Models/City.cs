using System.ComponentModel.DataAnnotations;

namespace Travel_Logger_API_api_tvrdna.Models;

public class City
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}
