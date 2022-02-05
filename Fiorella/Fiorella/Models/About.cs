namespace Fiorella.Models
{
    public class About
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int ReasonId { get; set; }
        public Reason Reason { get; set; }
    }
}