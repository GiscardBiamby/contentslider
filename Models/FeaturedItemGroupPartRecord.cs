using Orchard.ContentManagement.Records;

namespace ContentSlider.Models {
    public class FeaturedItemGroupPartRecord : ContentPartRecord {
        public virtual string Name { get; set; }
        public virtual int GroupWidth { get; set; }
        public virtual int GroupHeight { get; set; }
        public virtual string BackgroundColor { get; set; }
        public virtual string ForegroundColor { get; set; }
        public virtual string TransitionEffect { get; set; }
        public virtual int SlideSpeed { get; set; }
        public virtual int SlidePause { get; set; }
        public virtual bool ShowPager { get; set; }
        public virtual bool ShowSlideNumbers { get; set; }
    }
}