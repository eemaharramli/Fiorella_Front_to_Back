using System.Collections.Generic;

namespace Fiorella.Models
{
    public class ExpertJob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Expert> Experts { get; set; }
    }
}