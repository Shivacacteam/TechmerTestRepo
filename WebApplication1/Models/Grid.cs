using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Drawing;

namespace TechmerVision.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Store selected colors and visuzualization information for a design. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Grid : Asset
    {        
        #region TopLeft

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Private internal use to store color information in datastore. </summary>
        ///
        /// <value> The internal top left color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalTopLeftColorString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Pulic property to store selected color information. </summary>
        ///
        /// <value> Information describing the top left color. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public int[] TopLeftColorData
        {
            get
            {
                return Array.ConvertAll(InternalTopLeftColorString.Split(','), int.Parse);
            }
            set
            {
                var _data = value;
                InternalTopLeftColorString = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        #endregion

        #region TopRight

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Private internal use to store color information in datastore. </summary>
        ///
        /// <value> The internal top right color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalTopRightColorString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Pulic property to store selected color information. </summary>
        ///
        /// <value> Information describing the top right color. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public int[] TopRightColorData
        {
            get
            {
                return Array.ConvertAll(InternalTopRightColorString.Split(','), int.Parse);
            }
            set
            {
                var _data = value;
                InternalTopRightColorString = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        #endregion

        #region BottomLeft

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Private internal use to store color information in datastore. </summary>
        ///
        /// <value> The internal bottom left color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalBottomLeftColorString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Pulic property to store selected color information. </summary>
        ///
        /// <value> Information describing the bottom left color. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public int[] BottomLeftColorData
        {
            get
            {
                return Array.ConvertAll(InternalBottomLeftColorString.Split(','), int.Parse);
            }
            set
            {
                var _data = value;
                InternalBottomLeftColorString = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        #endregion

        #region BottomRight

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Private internal use to store color information in datastore. </summary>
        ///
        /// <value> The internal bottom right color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalBottomRightColorString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Pulic property to store selected color information. </summary>
        ///
        /// <value> Information describing the bottom right color. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public int[] BottomRightColorData
        {
            get
            {
                return Array.ConvertAll(InternalBottomRightColorString.Split(','), int.Parse);
            }
            set
            {
                var _data = value;
                InternalBottomRightColorString = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        #endregion

        #region Styling

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Store horizontal weighting information for grid. </summary>
        ///
        /// <value> The horizontal weight. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public decimal HorizontalWeight {get; set;}

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Store vertical weighting information for grid. </summary>
        ///
        /// <value> The vertical weight. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public decimal VerticalWeight { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Store number of nodes to use for height of grid. </summary>
        ///
        /// <value> The height. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Height { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Store number of nodes to use for width of grid. </summary>
        ///
        /// <value> The width. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Width { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Store value to use for spacing between grid nodes. </summary>
        ///
        /// <value> The spacing. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int spacing { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Border Radius for HTML Node in design Grid. </summary>
        ///
        /// <value> The border radius. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int borderRadius { get; set; }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Stores image data string for client display of image of the design grid. </summary>
        ///
        /// <value> Information describing the image. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public System.Drawing.Image imageData
        {
            get;
                /*
            {
                var imageBytes = Convert.FromBase64String(Image);
                // Convert byte[] to Image
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = System.Drawing.Image.FromStream(ms, true);
                    return image;
                };
            }
            */
            set;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Calculated two dimensional array of Colors. </summary>
        ///
        /// <value> An Array of grids. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public Color[][] GridArray { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Stores workspaceId. </summary>
        ///
        /// <value> The identifier of the workspace. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public long WorkspaceId { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Accessor Method for linked workspace. </summary>
        ///
        /// <value> The workspace. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [ForeignKey("WorkspaceId")]
        public Workspace Workspace { get; set; }

    }
}