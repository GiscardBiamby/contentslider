using ContentSlider.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace ContentSlider.Handlers {
    public class FeaturedItemSliderWidgetPartHandler : ContentHandler {
        public FeaturedItemSliderWidgetPartHandler(IRepository<FeaturedItemSliderWidgetPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}