using Jet_API1.Model;
using Jet_API1.ViewModel.Flights;
using Jet_API1.ViewModel.Hotel;
using Jet_API1.ViewModel.Regions;
using Jet_API1.ViewModel.Vehicles;

namespace Jet_API1.ViewModel.Orders
{
    public class GetOrderVM
    {
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string UserName { get; set; }
        public int VehicleId { get; set; }
        public VehicleVM Vehicle { get; set; }
        public int HotelId { get; set; }
        public HotelVM Hotels { get; set; }
        public int RegionId { get; set; }
        public RegionVM Regions { get; set; }
        public int FlightId { get; set; }
        public FlightVM Flight { get; set; }
    }
}
