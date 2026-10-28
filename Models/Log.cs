using System.ComponentModel.DataAnnotations;

namespace Travel_Logger_API_api_leglue.Models;

public class Log
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int CityId { get; set; }
    public string LogBody { get; set; }

}