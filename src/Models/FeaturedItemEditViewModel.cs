using System.Collections.Generic;

namespace AwkwardShowcaseItemSlider.Models {
    public class FeaturedItemEditViewModel {
        public FeaturedItemPart FeaturedItem { get; set; }
        public List<FeaturedItemGroupPart> Groups { get; set; }
    }
}