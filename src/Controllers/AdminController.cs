using System.Linq;
using System.Web.Mvc;
using ContentSlider.Models;
using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.UI.Admin;
using System;
using System.Linq;
using System.Web.Mvc;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Models;
using Orchard.Core.Containers.Models;
using Orchard.Core.Containers.ViewModels;
using Orchard.Localization;
using Orchard.UI.Notify;
using Orchard.DisplayManagement;
using Orchard.Core.Containers.Extensions;
using System.Web.Routing;
using Orchard.Settings;
using Orchard.Core.Feeds;
using Orchard.UI.Navigation;
using Orchard.ContentManagement.Aspects;

namespace ContentSlider.Controllers {

    [ValidateInput(false), Admin]
    public class AdminController : Controller {
        private readonly IContentManager _contentManager;

        public AdminController(IContentManager contentManager, IShapeFactory shapeFactory) {
            _contentManager = contentManager;
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }

        public ActionResult Items(string groupName) {
            var list = Shape.List();
            var featuredItemsQuery = _contentManager
                .Query<FeaturedItemPart, FeaturedItemPartRecord>("FeaturedItem");

            if (!string.IsNullOrWhiteSpace(groupName)) {
                featuredItemsQuery.Where(fi => fi.GroupName == groupName);
            }

            featuredItemsQuery
                .Join<CommonPartRecord>()
                .OrderByDescending(cr => cr.ModifiedUtc);

            var featuredItems = featuredItemsQuery.List();
            list.AddRange(featuredItems.Select(fi => _contentManager.BuildDisplay(fi, "SummaryAdmin")));

            dynamic viewModel = Shape.ViewModel();
            viewModel.ContentItems(list);
            viewModel.NumberOfItems(featuredItems.Count());
            
            return View(viewModel);
        }

        public ActionResult Groups() {
            var list = Shape.List();

            var groups = _contentManager
                .Query<FeaturedItemGroupPart, FeaturedItemGroupPartRecord>("FeaturedItemGroup")
                .Where<FeaturedItemGroupPartRecord>(s => s.Name != null && s.Name != "")
                .List();
            list.AddRange(groups.Select(g => _contentManager.BuildDisplay(g, "SummaryAdmin")));
            
            dynamic viewModel = Shape.ViewModel();
            viewModel.ContentItems(list);
            viewModel.NumberOfGroups(groups.Count());

            return View(viewModel);
        }
    }
}