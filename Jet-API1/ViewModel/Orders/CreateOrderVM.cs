using System.ComponentModel.DataAnnotations;

namespace Jet_API1.ViewModel.Orders;

public class CreateOrderVM
{
    [Required]
    public string CheckIn { get; set; }
    [Required]
    public string CheckOut { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public int HotelId { get; set; }
    [Required]
    public int FlightId { get; set; }
    [Required]
    public int VehicleId { get; set; }
    [Required]
    public int RegionId { get; set; }
}
