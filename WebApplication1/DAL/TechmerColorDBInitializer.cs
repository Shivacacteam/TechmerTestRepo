using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TechmerVision.Models;

namespace TechmerVision.DAL
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A techmer vision database initializer. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class TechmerVisionDBInitializer : System.Data.Entity.CreateDatabaseIfNotExists<WorkspaceContext> //System.Data.Entity.DropCreateDatabaseIfModelChanges<WorkspaceContext>// System.Data.Entity.DropCreateDatabaseAlways<WorkspaceContext>//
    { 
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     A method that should be overridden to actually add data to the context for seeding. The
        ///     default implementation does nothing.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="context">  The context to seed. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Seed(WorkspaceContext context)
        {
            #region Default Products
            IList<ProductTemplate> defaultProductTemplates = new List<ProductTemplate>();

            defaultProductTemplates.Add(new ProductTemplate() { Id = 1, Title = "Bottle", Image= "../images/bottle.png", Active = true, NumColors = 2 });
            defaultProductTemplates.Add(new ProductTemplate() { Id = 2, Title = "Cup", Image = "../images/cup.png", Active = true, NumColors = 1 });

            foreach (ProductTemplate prod in defaultProductTemplates)
            {
                context.ProductTemplates.Add(prod);
            }
            #endregion


            #region Default Product Colors
            IList<ProductTemplateColor> defaultProductTemplateColors = new List<ProductTemplateColor>();

            defaultProductTemplateColors.Add(new ProductTemplateColor() { ProductTemplateId = 1, ProductTemplate = defaultProductTemplates[0], ColorNumber = 1, ColorData = new int[] { 30, 53, 127, 255 }, RecolorToleranceLowerLimit = 200, RecolorToleranceUpperLimit = 300 });
            defaultProductTemplateColors.Add(new ProductTemplateColor() { ProductTemplateId = 1, ProductTemplate = defaultProductTemplates[0], ColorNumber = 2, ColorData = new int[] { 219, 111, 39, 255 }, RecolorToleranceLowerLimit = -20, RecolorToleranceUpperLimit = 100 });
            defaultProductTemplateColors.Add(new ProductTemplateColor() { ProductTemplateId = 2, ProductTemplate = defaultProductTemplates[1], ColorNumber = 1, ColorData = new int[] { 231, 84, 33, 255 }, RecolorToleranceLowerLimit = 0, RecolorToleranceUpperLimit = 360 });

            foreach (ProductTemplateColor prodColor in defaultProductTemplateColors)
            {
                context.ProductTemplateColors.Add(prodColor);
            }
            #endregion


            #region Add Default Workspaces
            IList<Workspace> defaultWorkspaces = new List<Workspace>();

            defaultWorkspaces.Add(new Workspace() { Id = 1, UserId = "adam.edmonds@gmail.com", Image = "", Pixelation = 1 });
            foreach(Workspace wsp in defaultWorkspaces)
            {
                context.Workspaces.Add(wsp);
            }
            #endregion

            #region  Add Default Grids
            IList<Grid> defaultGrids = new List<Grid>();

            defaultGrids.Add(new Grid()
            {
                //Id = 1,
                WorkspaceId = defaultWorkspaces[0].Id,
                TopLeftColorData = new int[] { 255,0,0,1},
                TopRightColorData = new int[] { 0, 255, 0, 1 },
                BottomLeftColorData = new int[] { 0, 0, 255, 1 },
                BottomRightColorData = new int[] { 128, 128, 128, 1 },
                HorizontalWeight = 1,
                ModifiedTimeStamp = 0,
                VerticalWeight = 1,
                Height = 10,
                Width = 10
            });

            defaultGrids.Add(new Grid()
            {
                //Id = 1,
                WorkspaceId = defaultWorkspaces[0].Id,
                TopLeftColorData = new int[] { 255, 0, 0, 1 },
                TopRightColorData = new int[] { 0, 255, 0, 1 },
                BottomLeftColorData = new int[] { 0, 0, 255, 1 },
                BottomRightColorData = new int[] { 128, 128, 128, 1 },
                HorizontalWeight = 1,
                ModifiedTimeStamp = 0,
                VerticalWeight = 1,
                Height = 10,
                Width = 10
            });
            foreach (Grid grid in defaultGrids)
            {
                context.Grids.Add(grid);
            }
            #endregion

            #region Add Default ColorSelections
            IList <ColorSelection> defaultColorSelections = new List<ColorSelection>();

            defaultColorSelections.Add(new ColorSelection() { WorkspaceId = defaultWorkspaces[0].Id, TimeStamp = new DateTime(1900, 1, 1, 00, 00, 00).ToUniversalTime().Ticks, ColorData = new int[] { 125, 54, 93, 1 }, hsl = new Decimal[] { 0, 0, 0 }, ColorStyle = "rgba(125,54,93,1)", ColorString = "Red: 125 Green: 54 Blue: 93" });
            defaultColorSelections.Add(new ColorSelection() { WorkspaceId = defaultWorkspaces[0].Id, TimeStamp = new DateTime(1900, 1, 1, 00, 00, 00).ToUniversalTime().Ticks, ColorData = new int[] { 84, 26, 197, 1 }, hsl = new Decimal[] { 0, 0, 0 }, ColorStyle = "rgba(84,26,197,1)", ColorString = "Red: 84 Green: 26 Blue: 197" });
            defaultColorSelections.Add(new ColorSelection() { WorkspaceId = defaultWorkspaces[0].Id, TimeStamp = new DateTime(1900, 1, 1, 00, 00, 00).ToUniversalTime().Ticks, ColorData = new int[] { 97, 147, 139, 1 }, hsl = new Decimal[] { 0, 0, 0 }, ColorStyle = "rgba(97,147,139,1)", ColorString = "Red: 97 Green: 147 Blue: 139" });
            defaultColorSelections.Add(new ColorSelection() { WorkspaceId = defaultWorkspaces[0].Id, TimeStamp = new DateTime(1900, 1, 1, 00, 00, 00).ToUniversalTime().Ticks, ColorData = new int[] { 125, 54, 93, 1 }, hsl = new Decimal[] { 0, 0, 0 }, ColorStyle = "rgba(84,26,197,1)", ColorString = "Red: 84 Green: 26 Blue: 197" });
            defaultColorSelections.Add(new ColorSelection() { WorkspaceId = defaultWorkspaces[0].Id, TimeStamp = new DateTime(1900, 1, 1, 00, 00, 00).ToUniversalTime().Ticks, ColorData = new int[] { 84, 26, 197, 1 }, hsl = new Decimal[] {0,0,0}, ColorStyle = "rgba(84,26,197,1)", ColorString = "Red: 84 Green: 26 Blue: 197"});

            for(int i = 0; i<10; i++) { 
                defaultColorSelections.Add(new ColorSelection() { WorkspaceId = defaultWorkspaces[0].Id, TimeStamp = new DateTime(1900, 1, 1, 00, 00, 00).ToUniversalTime().Ticks, ColorData = new int[] { 255, 255, 255, 1 },LAB = new Decimal[] { 255, 255, 255}, hsl = new Decimal[] { 0, 0, 0, 0 }, ColorStyle = "rgba(255,255,255,1)", ColorString = "Red: 255 Green: 255, Blue: 255", Favorite = true });
            }
            foreach (ColorSelection cs in defaultColorSelections)
            {
                context.ColorSelections.Add(cs);
            }
            #endregion

            base.Seed(context);
            
        }
    }
}