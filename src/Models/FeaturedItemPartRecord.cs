using Orchard.ContentManagement.Records;

namespace AwkwardShowcaseItemSlider.Models {
    public class FeaturedItemPartRecord : ContentPartRecord {
        public virtual string Headline { get; set; }
        public virtual string SubHeadline { get; set; }
        public virtual string LinkUrl { get; set; }
        public virtual string GroupName { get; set; }
        public virtual int SlideOrder { get; set; }
    }
}