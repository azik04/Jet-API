using System.ComponentModel.DataAnnotations;

namespace Jet_API1.ViewModel.Places;

public class CreatePalaceVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int CityId { get; set; }
}
