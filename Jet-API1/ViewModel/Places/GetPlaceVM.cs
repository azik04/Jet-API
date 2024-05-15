using Jet_API1.Model;
using Jet_API1.ViewModel.Cityes;

namespace Jet_API1.ViewModel.Places
{
    public class GetPlaceVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
        public CityVM? City { get; set; }
    }
}
