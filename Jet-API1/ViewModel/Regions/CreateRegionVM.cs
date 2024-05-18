using System.ComponentModel.DataAnnotations;

namespace Jet_API1.ViewModel.Regions;

public class CreateRegionVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int CityId { get; set; }
}
