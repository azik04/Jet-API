using Jet_API1.Model.Base;
using System.Text.Json.Serialization;

namespace Jet_API1.Model;

public class Region : BaseModel
{
    public string Name { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public ICollection<Hotell> Hotel { get; set; }
    public ICollection<Order> Order { get; set; }
}
