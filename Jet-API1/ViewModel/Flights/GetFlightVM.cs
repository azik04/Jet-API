using Jet_API1.Model;

namespace Jet_API1.ViewModel.Flights;

public class GetFlightVM
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
}
