using fastwin.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace fastwin.Entities
{
    public class UserCode : BaseEntity
    {
        public int CodeId { get; set; }
        public string UserId { get; set; }
        public UserCodeStatus CodeStatus { get; set; }
        public Codes Code { get; set; }
        public User User { get; set; }
    }

    public enum UserCodeStatus
    {
        Scanned,
        Used
    }
}

