using Jet_API1.Model;
using Jet_API1.ViewModel.Regions;

namespace Jet_API1.ViewModel.Hotel
{
    public class GetHotelVM
    {
        public string Name { get; set; }
        public int RegionId { get; set; }
        public GetRegionVM Region { get; set; }
    }
}
