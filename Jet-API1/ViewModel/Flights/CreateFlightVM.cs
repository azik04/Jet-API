using System.ComponentModel.DataAnnotations;

namespace Jet_API1.ViewModel.Flights;

public class CreateFlightVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int VehicleId { get; set; }
}
