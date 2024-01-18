using System.ComponentModel.DataAnnotations;

namespace fastwin.Requests
{
    public class RegistrationRequest
    {
        public string Email { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
