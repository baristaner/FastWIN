using System.ComponentModel.DataAnnotations;

namespace fastwin.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public DateTime LastUsageDate { get; set; }
    }

    public enum Category
    {
        Beverage, 
        Snacks
    }

}
