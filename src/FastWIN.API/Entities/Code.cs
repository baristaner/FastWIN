using fastwin.Entities;
using System.ComponentModel.DataAnnotations;

namespace fastwin.Models
{
    public class Codes : BaseEntity
    {

        [StringLength(10)]
        public string Code { get; set; }

        public DateTime ExpirationDate { get; set; } 

        public bool IsActive { get; set; }
    }
}
