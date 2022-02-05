using System;
using Microsoft.VisualBasic;

namespace Fiorella.Models
{
    public class Blog
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
    }
}