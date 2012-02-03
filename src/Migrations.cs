using Orchard.ContentManagement.MetaData;
using Orchard.Data.Migration;

namespace ContentSlider {
    public class Migrations : DataMigrationImpl {

        public int Create() {
            SchemaBuilder.CreateTable("FeaturedItemGroupPartRecord"
                , builder => builder
                    .ContentPartRecord()
                    .Column<string>("Name", col => col.WithLength(100))
                    .Column<int>("GroupWidth")
                    .Column<int>("GroupHeight")
                    .Column<string>("BackgroundColor")
                    .Column<string>("ForegroundColor")
                    .Column<int>("SlideSpeed", cfg => cfg.WithDefault(300))
                    .Column<int>("SlidePause", cfg => cfg.WithDefault(5000))
                    .Column<bool>("ShowSlideNumbers", col => col.WithDefault(false))
                    .Column<bool>("ShowPager", col => col.WithDefault(true))
                    .Column<string>("TransitionEffect", col => col.WithDefault("hslide"))
            );

            ContentDefinitionManager.AlterTypeDefinition("FeaturedItemGroup"
                , builder => builder
                    .DisplayedAs("Featured Item Group")
                    .WithPart("FeaturedItemGroupPart")
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
            );

            SchemaBuilder.CreateTable("FeaturedItemPartRecord"
                , builder => builder
                    .ContentPartRecord()
                    .Column<string>("Headline", col => col.Unlimited())
                    .Column<string>("SubHeadline", col => col.Unlimited())
                    .Column<string>("LinkUrl", col => col.Unlimited())
                    .Column<string>("GroupName", col => col.WithLength(100))
                    .Column<int>("SlideOrder", col => col.WithDefault(0))
                    .Column<string>("ContentHtml", col => col.Unlimited())
            );

            ContentDefinitionManager.AlterTypeDefinition("FeaturedItem"
                , builder => builder
                    .DisplayedAs("Featured Item")
                    .WithPart("FeaturedItemPart")
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
            );

            SchemaBuilder.CreateTable("FeaturedItemSliderWidgetPartRecord"
                , builder => builder
                    .ContentPartRecord()
                    .Column<string>("GroupName", col => col.WithLength(100))
            );

            ContentDefinitionManager.AlterTypeDefinition("FeaturedItemSliderWidget"
                , builder => builder
                    .WithPart("FeaturedItemSliderWidgetPart")
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithPart("IdentityPart")
                    .WithSetting("Stereotype", "Widget")
            );

            return 1;
        }

    }
}