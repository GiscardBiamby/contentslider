using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Data;

namespace ContentSlider {
    public class Migrations : DataMigrationImpl {

        public int Create() {

            // 
            // Slide Groups: 
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
            ContentDefinitionManager.AlterPartDefinition("FeaturedItemGroupPart"
                , builder => builder
                    .Attachable()
                    .Named("Slide Show")
            ); 
            ContentDefinitionManager.AlterTypeDefinition("FeaturedItemGroup"
                , builder => builder
                    .DisplayedAs("Slide Show")
                    .WithPart("FeaturedItemGroupPart")
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
            );

            // 
            // Slides
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
                    .DisplayedAs("Slide")
                    .WithPart("FeaturedItemPart")
                    .WithPart("CommonPart")
                    .WithPart("IdentityPart")
            );

            // 
            // Widget
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

        public int UpdateFrom1() {
            SchemaBuilder.AlterTable(
                "FeaturedItemGroupPartRecord"
                , t => t.CreateIndex("ncix_FeaturedItemGroupPartRecord__Name", "Name")
            );
            SchemaBuilder.AlterTable(
                "FeaturedItemPartRecord"
                , t => t.CreateIndex("ncix_FeaturedItemPartRecord__GroupName", "GroupName")
            );
            SchemaBuilder.AlterTable(
                "FeaturedItemSliderWidgetPartRecord"
                , t => t.CreateIndex("ncix_FeaturedItemSliderWidgetPartRecord__GroupName", "GroupName")
            );
            return 2;
        }
    }
}