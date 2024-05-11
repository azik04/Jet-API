using Jet_API1.Model.Base;
using System.Text.Json.Serialization;

namespace Jet_API1.Model
{
    public class Hotel :BaseModel
    {
        public string Name { get; set; }
        public int RegionId { get; set; }
        [JsonIgnore]
        public Region Region { get; set; }
        [JsonIgnore]
        public ICollection<Order> Order { get; set; }
    }
}
