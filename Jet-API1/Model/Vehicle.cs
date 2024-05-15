using Jet_API1.Model.Base;
using System.Text.Json.Serialization;

namespace Jet_API1.Model
{
    public class Vehicle : BaseModel
    {
        public string Name { get; set; }
        public ICollection<Flight>? Flights { get; set; }
    }
}
