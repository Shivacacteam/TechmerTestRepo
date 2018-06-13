﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechmerVision.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A product color. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ProductColor
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
        /// <summary>   Gets or sets the identifier of the product. </summary>
        ///
        /// <value> The identifier of the product. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public long ProductId { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the color number. </summary>
        ///
        /// <value> The color number. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int ColorNumber { get; set; }

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the image. </summary>
        ///
        /// <value> The image. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public string Image { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the filename of the file. </summary>
        ///
        /// <value> The filename. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String Filename { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Accessor property for the linked product. </summary>
        ///
        /// <value> The product. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}