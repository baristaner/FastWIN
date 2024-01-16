using fastwin.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace fastwin.Entities
{
    public class UserCode : BaseEntity
    {
        public int CodeId { get; set; }
        public Codes Code { get; set; }
    }
}

