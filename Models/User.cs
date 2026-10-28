using System.ComponentModel.DataAnnotations;

namespace Travel_Logger_API_api_leglue.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<Recommendation> Recommendations { get; set; }
    public List<Log> Logs { get; set; }
}