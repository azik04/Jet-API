namespace Jet_API1.ViewModel.Orders
{
    public class CreateOrderVM
    {
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public string UserName { get; set; }
        public int HotelId { get; set; }
        public int RegionId { get; set; }
    }
}
