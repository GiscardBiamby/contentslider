using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;

namespace ContentSlider.Models {
    public class FeaturedItemSliderWidgetPart : ContentPart<FeaturedItemSliderWidgetPartRecord> {
        
        [Required(ErrorMessage = "You must have a Feature Group to associate with this widget")]
        public string GroupName {
            get { return Record.GroupName; }
            set { Record.GroupName = value; }
        }
    }
}