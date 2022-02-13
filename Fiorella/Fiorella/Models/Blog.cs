using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace Fiorella.Models
{
    public class Blog
    {
        public int id { get; set; }
        [Required(ErrorMessage = "The Title for blog is required"), MaxLength(40)]
        public string Title { get; set; }
        [Required(ErrorMessage = "The Info about new blog is required"), MaxLength(100)]
        public string Info { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
    }
}