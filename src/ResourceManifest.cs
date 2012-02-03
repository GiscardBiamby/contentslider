using Orchard.UI.Resources;

namespace ContentSlider
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            // 
            // Styles:
            manifest.DefineStyle("ContentSliderStyles").SetUrl("aw-showcase-style.css");


            // 
            // Scripts: 
            manifest.DefineScript("jQueryAwkwardShowcase").SetUrl("jquery.aw-showcase.min.js", "jquery.aw-showcase.js").SetDependencies("jQuery");
        }

    }
}