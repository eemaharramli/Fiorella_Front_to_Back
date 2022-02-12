using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Product name required"), MaxLength(30)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Product must contain a Price")]
        public double Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}