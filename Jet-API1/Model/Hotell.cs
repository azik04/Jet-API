using Jet_API1.Model.Base;
using System.Text.Json.Serialization;

namespace Jet_API1.Model;

public class Hotell :BaseModel
{
    public string Name { get; set; }
    public int RegionId { get; set; }
    public Region Region { get; set; }
    public ICollection<Order> Order { get; set; }
}
