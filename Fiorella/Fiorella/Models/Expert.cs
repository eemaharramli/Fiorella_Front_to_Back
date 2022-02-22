using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Fiorella.Models
{
    public class Expert
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public int JobId { get; set; }
        public ExpertJob ExpertJob { get; set; }
        
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
    }
}