using Jet_API1.Enum;
using Jet_API1.Model.Base;

namespace Jet_API1.Model
{
    public class Order : BaseModel
    {
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public string UserName { get; set; }
        public Vehicle Vehicle { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotels { get; set; }
        public int RegionId { get; set; }
        public Region Regions { get; set; }

    }
}
