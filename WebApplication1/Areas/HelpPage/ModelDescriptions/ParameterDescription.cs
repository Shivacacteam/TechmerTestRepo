using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TechmerVision.Areas.HelpPage.ModelDescriptions
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Description of the parameter. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ParameterDescription
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ParameterDescription()
        {
            Annotations = new Collection<ParameterAnnotation>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the annotations. </summary>
        ///
        /// <value> The annotations. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Collection<ParameterAnnotation> Annotations { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the documentation. </summary>
        ///
        /// <value> The documentation. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Documentation { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Name { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets information describing the type. </summary>
        ///
        /// <value> Information describing the type. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ModelDescription TypeDescription { get; set; }
    }
}