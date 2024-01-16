using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace fastwin.Entities
{
    public class User : IdentityUser
    {
        public List<UserCode> UserCode { get; set; }
    }
}
