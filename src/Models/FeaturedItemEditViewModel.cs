using System.Collections.Generic;

namespace ContentSlider.Models {
    public class FeaturedItemEditViewModel {
        public FeaturedItemPart FeaturedItem { get; set; }
        public List<FeaturedItemGroupPart> Groups { get; set; }
    }
}