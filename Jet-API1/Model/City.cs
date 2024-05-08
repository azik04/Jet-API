using Jet_API1.Model.Base;

namespace Jet_API1.Model
{
    public class City : BaseModel
    {
        public string Name { get; set; }
        public List<Place> Places { get; set; }
    }
}
