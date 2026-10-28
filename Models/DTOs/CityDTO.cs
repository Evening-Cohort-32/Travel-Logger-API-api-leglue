namespace Travel_Logger_API_api_leglue.Models.DTOs;

public class CityDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UserDTO> Users { get; set; }
    public List<LogDTO> Logs { get; set; }
    public List<RecommendationDTO> Recommendations { get; set; }
}