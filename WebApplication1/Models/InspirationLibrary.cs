﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechmerVision.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A sample inspiration. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SampleInspiration
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
        /// <summary>   Gets or sets the title. </summary>
        ///
        /// <value> The title. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String Title { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the image. </summary>
        ///
        /// <value> The image. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [NotMapped]
        public String Image { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets a value indicating whether the active. </summary>
        ///
        /// <value> True if active, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Boolean Active { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the filename of the file. </summary>
        ///
        /// <value> The filename. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public String Filename { get; set; }

    }
}