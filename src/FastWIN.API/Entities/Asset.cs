using fastwin.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace fastwin.Entities
{
    public class Asset : BaseEntity
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Codes")]
        public int CodeId { get; set; }

        public Product Product { get; set; }
        public Codes Codes { get; set; }
    }
}
