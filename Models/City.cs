using System.ComponentModel.DataAnnotations;

namespace Travel_Logger_API_api_leglue.Models;

public class City
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<User> Users { get; set; }
    public List<Log> Logs { get; set; }
    public List<Recommendation> Recommendations { get; set; }
}
