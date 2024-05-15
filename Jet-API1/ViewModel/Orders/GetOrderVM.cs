using Jet_API1.Model;

namespace Jet_API1.ViewModel.Orders
{
    public class GetOrderVM
    {
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string UserName { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int HotelId { get; set; }
        public Hotell Hotels { get; set; }
        public int RegionId { get; set; }
        public Region Regions { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
