using Jet_API1.Model.Base;
using System.Text.Json.Serialization;

namespace Jet_API1.Model;

public class Flight : BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get ; set; }
    public ICollection<Order>? Orders { get; set; }
}
