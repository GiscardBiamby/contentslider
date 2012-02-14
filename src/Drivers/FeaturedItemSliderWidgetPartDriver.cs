using System.Linq;
using ContentSlider.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace ContentSlider.Drivers {
    public class FeaturedItemSliderWidgetPartDriver : ContentPartDriver<FeaturedItemSliderWidgetPart> {
        private readonly IContentManager _contentManager;


        public FeaturedItemSliderWidgetPartDriver(IContentManager contentManager) {
            _contentManager = contentManager;
        }


        protected override DriverResult Display(FeaturedItemSliderWidgetPart part, string displayType, dynamic shapeHelper) {
            int slideNumber = 0;

            var featuredItems = _contentManager.Query<FeaturedItemPart, FeaturedItemPartRecord>("FeaturedItem")
                .Where(fip => fip.GroupName == part.GroupName)
                .OrderBy(fi => fi.SlideOrder)
                .List()
                .Select(fi => new FeaturedItemViewModel {
                    Headline = fi.Headline,
                    SubHeadline = fi.SubHeadline,
                    LinkUrl = fi.LinkUrl,
                    SlideNumber = ++slideNumber, 
                    ContentHtml = fi.ContentHtml
                })
                .ToList();

            var group = _contentManager.Query<FeaturedItemGroupPart, FeaturedItemGroupPartRecord>()
                .Where(fig => fig.Name == part.GroupName)
                .List()
                .SingleOrDefault();

            if (group != null) {
                group.BackgroundColor = group.BackgroundColor.TrimStart('#');
                group.ForegroundColor = group.ForegroundColor.TrimStart('#');
            }

            return ContentShape(
                "Parts_FeaturedItems",
                () => shapeHelper.Parts_FeaturedItems(
                    FeaturedItems: featuredItems
                    , ContentPart: part
                    , Group: group
                )
            );
        }


        protected override DriverResult Editor(FeaturedItemSliderWidgetPart part, dynamic shapeHelper) {
            var groups = _contentManager.Query<FeaturedItemGroupPart, FeaturedItemGroupPartRecord>()
                .Where(s => s.Name != null && s.Name != "")
                .List()
                .Select(fig => fig.Name)
                .ToList();

            var viewModel = new FeaturedItemSliderWidgetEditViewModel { GroupNames = groups, SelectedGroup = part.GroupName};
            
            return ContentShape(
                "Parts_FeaturedItemSliderWidget_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.FeaturedItemSliderWidget.Edit"
                    , Model: viewModel
                )
            );
        }


        protected override DriverResult Editor(FeaturedItemSliderWidgetPart part, IUpdateModel updater, dynamic shapeHelper) 
        {
            updater.TryUpdateModel(part, "", null, null);
            return Editor(part, shapeHelper);
        }


        protected override void Importing(FeaturedItemSliderWidgetPart part, Orchard.ContentManagement.Handlers.ImportContentContext context)
        {
            var groupName = context.Attribute(part.PartDefinition.Name, "GroupName");
            if (groupName != null)
            {
                part.GroupName = groupName;
            }
        }


        protected override void Exporting(FeaturedItemSliderWidgetPart part, Orchard.ContentManagement.Handlers.ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("GroupName", part.GroupName);
        }
    }
}