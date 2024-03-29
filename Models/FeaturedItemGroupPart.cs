using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace ContentSlider.Models {
    public class FeaturedItemGroupPart : ContentPart<FeaturedItemGroupPartRecord> {
        private const int MinFeatureDimension = 1;
        private const int MaxFeatureDimension = 99999;
        private const int DefaultSlideSpeed = 300;
        private const int DefaultSlidePause = 5000;


        [Required]
        public string Name {
            get { return Record.Name; }
            set { Record.Name = value; }
        }

        [Range(MinFeatureDimension, MaxFeatureDimension, ErrorMessage = "Group Width must be greater than zero.")]
        public int GroupWidth {
            get { return Record.GroupWidth; }
            set { Record.GroupWidth = value; }
        }

        [Range(MinFeatureDimension, MaxFeatureDimension, ErrorMessage = "Group Height must be greater than zero.")]
        public int GroupHeight {
            get { return Record.GroupHeight; }
            set { Record.GroupHeight = value; }
        }

        [Required]
        public string BackgroundColor {
            get { return Record.BackgroundColor; }
            set { Record.BackgroundColor = value; }
        }

        [Required]
        public string ForegroundColor {
            get { return Record.ForegroundColor; }
            set { Record.ForegroundColor = value; }
        }

        [Required]
        public string TransitionEffect {
            get { return Record.TransitionEffect; }
            set { Record.TransitionEffect = value; }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Slide Speed must at least one millisecond.")]
        public int SlideSpeed {
            get { return Record.SlideSpeed==0 ? DefaultSlideSpeed : Record.SlideSpeed; }
            set { Record.SlideSpeed = value; }
        }

        [Range(15, int.MaxValue, ErrorMessage = "Slide Pause must be at least fifteen milliseconds.")]
        public int SlidePause {
            get { return Record.SlidePause==0 ? DefaultSlidePause : Record.SlidePause; }
            set { Record.SlidePause = value; }
        }

        public bool ShowPager {
            get { return Record.ShowPager; }
            set { Record.ShowPager = value; }
        }

        public bool ShowSlideNumbers {
            get { return Record.ShowSlideNumbers; }
            set { Record.ShowSlideNumbers = value; }
        }
    }
}