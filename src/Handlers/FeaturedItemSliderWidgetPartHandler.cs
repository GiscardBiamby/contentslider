using AwkwardShowcaseItemSlider.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace AwkwardShowcaseItemSlider.Handlers {
    public class FeaturedItemSliderWidgetPartHandler : ContentHandler {
        public FeaturedItemSliderWidgetPartHandler(IRepository<FeaturedItemSliderWidgetPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}