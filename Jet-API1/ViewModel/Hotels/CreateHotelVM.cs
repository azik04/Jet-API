using System.ComponentModel.DataAnnotations;

namespace Jet_API1.ViewModel.Hotel;

public class CreateHotelVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int RegionId { get; set; }
}
