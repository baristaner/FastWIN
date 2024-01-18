using fastwin.Entities;
using System.ComponentModel.DataAnnotations;

namespace fastwin.Models
{
    public class Codes : BaseEntity
    {

        [StringLength(10)]
        public string Code { get; set; }

        public DateTime ExpirationDate { get; set; }

        public StatusCode Status { get; set; }

    }

    public enum StatusCode
    {
        Active,
        Passive,
        Locked
    }
}
