namespace Travel_Logger_API_api_leglue.Models.DTOs;

public class LogDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CityId { get; set; }
    public string LogBody { get; set; }
}