using Jet_API1.Model.Base;
using System.Text.Json.Serialization;

namespace Jet_API1.Model
{
    public class City : BaseModel
    {
        public string Name { get; set; }
        
        public List<Place> Places { get; set; }
        public List<Region> Regions { get; set; }
    }
}
