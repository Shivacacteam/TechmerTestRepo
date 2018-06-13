using System.Data.SqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace TechmerVision.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A color selection. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ColorSelection
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier. </summary>
        ///
        /// <value> The identifier. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier of the workspace. </summary>
        ///
        /// <value> The identifier of the workspace. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public long WorkspaceId { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the time stamp. </summary>
        ///
        /// <value> The time stamp. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public long TimeStamp { get; set; }

        #region ColorData

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the internal color string. </summary>
        ///
        /// <value> The internal color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalColorString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets information describing the color. </summary>
        ///
        /// <value> Information describing the color. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public int[] ColorData
        {
            get
            {
                return Array.ConvertAll(InternalColorString.Split(','), int.Parse);
            }
            set
            {
                var _data = value;
                InternalColorString = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }

        #endregion

        #region HSL

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the internal hsl. </summary>
        ///
        /// <value> The internal hsl. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalHSL { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the hsl. </summary>
        ///
        /// <value> The hsl. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public Decimal[] hsl
        {
            get
            {
                return Array.ConvertAll(InternalHSL.Split(','), Decimal.Parse);
            }
            set
            {
                var _data = value;
                InternalHSL = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        #endregion


        #region LAB

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the internal hsl. </summary>
        ///
        /// <value> The internal hsl. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalLAB { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the hsl. </summary>
        ///
        /// <value> The hsl. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public Decimal[] LAB
        {
            get
            {
                return Array.ConvertAll(InternalLAB.Split(','), Decimal.Parse);
            }
            set
            {
                var _data = value;
                InternalLAB = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        #endregion

        #region HSV
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalHSV { get; set; }

        [NotMapped]
        public Decimal[] HSV
        {
            get
            {
               return Array.ConvertAll(InternalHSV.Split(','), Decimal.Parse);
            }
            set
            {
                var _data = value;
                InternalHSV = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        
        #endregion


        #region CMYK
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string InternalCMYK { get; set; }
        [NotMapped]
        public Decimal[] CMYK
        {
            get
            {
                return Array.ConvertAll(InternalCMYK.Split(','), Decimal.Parse);
            }
            set
            {
                var _data = value;
                InternalCMYK = String.Join(",", _data.Select(p => p.ToString()).ToArray());
            }
        }
        #endregion

        public string InternalHEX { get; set; }


        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the color style. </summary>
        ///
        /// <value> The color style. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ColorStyle { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the color string. </summary>
        ///
        /// <value> The color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ColorString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the favorite. </summary>
        ///
        /// <value> True if favorite, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //public string ColorSpace { get; set; }
        public Boolean Favorite { get; set; }


        #region NavProperties

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>  Accessor property for the linked workspace. </summary>
        ///
        /// <value> The workspace. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [ForeignKey("WorkspaceId")]
        public Workspace Workspace { get; set; }
        #endregion

    }
}