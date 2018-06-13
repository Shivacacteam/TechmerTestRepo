using System;

namespace TechmerVision.Areas.HelpPage.ModelDescriptions
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     Use this attribute to change the name of the <see cref="ModelDescription"/> generated for
    ///     a type.
    /// </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class ModelNameAttribute : Attribute
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="name"> The name. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ModelNameAttribute(string name)
        {
            Name = name;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Name { get; private set; }
    }
}