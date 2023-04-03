
using System.ComponentModel.DataAnnotations;

namespace back.Model.Request
{
    public class UserRegistrationRequest
    {
        [Required] public string? Username { get; set; }
        [Required] public string? Password { get; set; }
        [Required] public string? Firstname {get; set;}
        [Required] public string? Lastname {get; set;}
        [Required]public string? Email {get; set;}
        [Required] public string? Birthday {get; set;}
    }
}