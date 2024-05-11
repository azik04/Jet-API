using Jet_API1.Model.Base;
using System.Text.Json.Serialization;

namespace Jet_API1.Model
{
    public class Region : BaseModel
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        [JsonIgnore]
        public City City { get; set; }
        public ICollection<Hotel> Hotel { get; set; }
        [JsonIgnore]
        public ICollection<Order> Order { get; set; }
    }
}
