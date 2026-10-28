namespace Travel_Logger_API_api_leglue.Models.DTOs;

public class RecommendationDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public int CityId { get; set; }
    public string RecommendationBody { get; set; }
}