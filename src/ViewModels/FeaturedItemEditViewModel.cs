using System.Collections.Generic;

namespace ContentSlider.Models {
    public class FeaturedItemEditViewModel {
        public string Headline { get; set; }
        public string SubHeadline { get; set; }
        public string LinkUrl { get; set; }
        public string GroupName { get; set; }
        public int SlideOrder { get; set; }
        public string ContentHtml { get; set; }

        public List<FeaturedItemGroupPart> Groups { get; set; }
        public string AddMediaPath { get; set; }
    }
}