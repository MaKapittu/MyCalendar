using System.ComponentModel.DataAnnotations;

namespace back.Model.Request
{
    public class UserLoginRequest
    {
        [Required] public string? Username { get; set; }
        [Required] public string? Password { get; set; }
    }
}