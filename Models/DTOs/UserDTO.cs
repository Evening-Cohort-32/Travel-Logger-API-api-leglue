namespace Travel_Logger_API_api_leglue.Models.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<RecommendationDTO> Recommendations { get; set; }
    public List<LogDTO> Logs { get; set; }
}