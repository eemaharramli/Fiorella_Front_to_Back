using System.Collections.Generic;

namespace Fiorella.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderImage> SliderImages { get; set; }
        public Slider Slider { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}