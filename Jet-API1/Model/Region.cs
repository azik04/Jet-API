using Jet_API1.Model.Base;

namespace Jet_API1.Model
{
    public class Region : BaseModel
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; } 
        public IQueryable<Hotel> Hotel { get; set; }
    }
}
