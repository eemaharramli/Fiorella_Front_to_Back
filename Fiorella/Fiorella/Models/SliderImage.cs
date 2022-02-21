using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Fiorella.Models
{
    public class SliderImage
    {
        public int Id { get; set; }
        public string SliderImageName { get; set; }
        
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}