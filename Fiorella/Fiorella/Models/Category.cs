using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fiorella.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required"), MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "Max symbol count 200")]
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}