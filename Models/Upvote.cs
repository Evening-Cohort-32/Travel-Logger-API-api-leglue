using System.ComponentModel.DataAnnotations;

namespace Travel_Logger_API_api_tvrdna.Models;

public class Upvote
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int RecommendationId { get; set; }
}