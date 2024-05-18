using System.ComponentModel.DataAnnotations;

namespace Jet_API1.ViewModel.Account;

public class AccountVM
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}
