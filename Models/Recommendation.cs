using System.ComponentModel.DataAnnotations;

namespace Travel_Logger_API_api_tvrdna.Models;

public class Recommendation
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int CityId { get; set; }
    public string RecommendationBody { get; set; }
}