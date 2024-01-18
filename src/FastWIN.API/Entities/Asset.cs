using fastwin.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace fastwin.Entities
{
    public class Asset : BaseEntity
    {
        public int ProductId { get; set; }

        public int CodeId { get; set; }

        public string UserId { get; set; }

        public Product Product { get; set; }
        public Codes Codes { get; set; }
        public User User { get; set; }
    }
}
