﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechmerVision.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A workspace. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Workspace : Asset
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the identifier of the user. </summary>
        ///
        /// <value> The identifier of the user. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string UserId { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the pixelation. </summary>
        ///
        /// <value> The pixelation. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Pixelation { get; set; }

        //public virtual ICollection<Grid> Grids { get; set; }

        //public virtual ICollection<ColorSelection> ColorSelections { get; set; }

    }
}