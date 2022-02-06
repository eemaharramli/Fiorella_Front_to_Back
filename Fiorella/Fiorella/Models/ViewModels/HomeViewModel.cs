using System.Collections.Generic;

namespace Fiorella.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; }
        public Slider Slider { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<About> About { get; set; }
        public List<Reason> Reasons { get; set; }
        public List<Expert> Experts { get; set; }
        public List<ExpertJob> ExpertJobs { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Thing> Things { get; set; }
        public List<InstagramImage> InstagramImages { get; set; }
        
        public List<Subscribe> Subscribes { get; set; }
    }
}