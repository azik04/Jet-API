using Jet_API1.Model.Base;

namespace Jet_API1.Model
{
    public class Hotel :BaseModel
    {
        public string Name { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
