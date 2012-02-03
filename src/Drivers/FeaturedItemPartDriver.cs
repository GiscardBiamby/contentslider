using System.Linq;
using ContentSlider.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;

namespace ContentSlider.Drivers {
    public class FeaturedItemPartDriver : ContentPartDriver<FeaturedItemPart> {
        private readonly IContentManager _contentManager;

        public FeaturedItemPartDriver(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        protected override DriverResult Display(FeaturedItemPart part, string displayType, dynamic shapeHelper) {
            var group = _contentManager.Query<FeaturedItemGroupPart, FeaturedItemGroupPartRecord>("FeaturedItemGroup")
                .Where(g => g.Name == part.GroupName).List().SingleOrDefault();

            return ContentShape("Parts_FeaturedItem_SummaryAdmin",
                () => shapeHelper.Parts_FeaturedItem_SummaryAdmin(
                    ContentPart: part
                    , ContentItem: part.ContentItem
                    , Group: group
                )
            );
        }


        protected override DriverResult Editor(FeaturedItemPart part, dynamic shapeHelper) 
        {
            return ContentShape(
                "Parts_FeaturedItem_Edit"
                , () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts.FeaturedItem.Edit"
                    , Model: BuildViewModel(part)
                )
            );
        }


        protected override DriverResult Editor(FeaturedItemPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part, "", null, null);
            return Editor(part, shapeHelper);
        }


        private FeaturedItemEditViewModel BuildViewModel(FeaturedItemPart part)
        {
            var groups = _contentManager.Query<FeaturedItemGroupPart, FeaturedItemGroupPartRecord>("FeaturedItemGroup").List();
            return new FeaturedItemEditViewModel
            {
                Headline    = part.Headline   
                , SubHeadline = part.SubHeadline
                , LinkUrl     = part.LinkUrl    
                , GroupName = part.GroupName
                , SlideOrder = part.SlideOrder
                , ContentHtml = part.ContentHtml
                , Groups = groups.ToList()
                , AddMediaPath = "ContentSlider"
            };
        }


        protected override void Importing(FeaturedItemPart part, ImportContentContext context) {
            var headline = context.Attribute(part.PartDefinition.Name, "Headline");
            if (headline != null) {
                part.Headline = headline;
            }
            var subHeadline = context.Attribute(part.PartDefinition.Name, "SubHeadline");
            if (subHeadline != null) {
                part.SubHeadline = subHeadline;
            }
            var linkUrl = context.Attribute(part.PartDefinition.Name, "LinkUrl");
            if (linkUrl != null) {
                part.LinkUrl = linkUrl;
            }
            var groupName = context.Attribute(part.PartDefinition.Name, "GroupName");
            if (groupName != null) {
                part.GroupName = groupName;
            }
            string slideOrder = context.Attribute(part.PartDefinition.Name, "SlideOrder");
            int slideOrderNumber;
            if (slideOrder != null && int.TryParse(slideOrder, out slideOrderNumber)) {
                part.SlideOrder = slideOrderNumber;
            }
            var contentHtml = context.Attribute(part.PartDefinition.Name, "ContentHtml");
            if (contentHtml != null) {
                part.ContentHtml = contentHtml;
            }
        }


        protected override void Exporting(FeaturedItemPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Headline", part.Headline);
            context.Element(part.PartDefinition.Name).SetAttributeValue("SubHeadline", part.SubHeadline);
            context.Element(part.PartDefinition.Name).SetAttributeValue("LinkUrl", part.LinkUrl);
            context.Element(part.PartDefinition.Name).SetAttributeValue("GroupName", part.GroupName);
            context.Element(part.PartDefinition.Name).SetAttributeValue("SlideOrder", part.SlideOrder);
            context.Element(part.PartDefinition.Name).SetAttributeValue("ContentHtml", part.ContentHtml);
        }
    }
}