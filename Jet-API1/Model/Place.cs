using Jet_API1.Model.Base;

namespace Jet_API1.Model
{
    public class Place : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
