using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace TechmerVision.Areas.HelpPage
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   This is used to identify the place where the sample should be applied. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class HelpPageSampleKey
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new <see cref="HelpPageSampleKey"/> based on media type. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="mediaType">    The media type. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HelpPageSampleKey(MediaTypeHeaderValue mediaType)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            ActionName = String.Empty;
            ControllerName = String.Empty;
            MediaType = mediaType;
            ParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Creates a new <see cref="HelpPageSampleKey"/> based on media type and CLR type.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="mediaType">    The media type. </param>
        /// <param name="type">         The CLR type. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, Type type)
            : this(mediaType)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            ParameterType = type;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Creates a new <see cref="HelpPageSampleKey"/> based on <see cref="SampleDirection"/>,
        ///     controller name, action name and parameter names.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="InvalidEnumArgumentException"> Thrown when an Invalid Enum Argument error
        ///                                                 condition occurs. </exception>
        /// <exception cref="ArgumentNullException">        Thrown when one or more required arguments
        ///                                                 are null. </exception>
        ///
        /// <param name="sampleDirection">  The <see cref="SampleDirection"/>. </param>
        /// <param name="controllerName">   Name of the controller. </param>
        /// <param name="actionName">       Name of the action. </param>
        /// <param name="parameterNames">   The parameter names. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HelpPageSampleKey(SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames)
        {
            if (!Enum.IsDefined(typeof(SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int)sampleDirection, typeof(SampleDirection));
            }
            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }
            if (actionName == null)
            {
                throw new ArgumentNullException("actionName");
            }
            if (parameterNames == null)
            {
                throw new ArgumentNullException("parameterNames");
            }

            ControllerName = controllerName;
            ActionName = actionName;
            ParameterNames = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            SampleDirection = sampleDirection;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Creates a new <see cref="HelpPageSampleKey"/> based on media type,
        ///     <see cref="SampleDirection"/>, controller name, action name and parameter names.
        /// </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="mediaType">        The media type. </param>
        /// <param name="sampleDirection">  The <see cref="SampleDirection"/>. </param>
        /// <param name="controllerName">   Name of the controller. </param>
        /// <param name="actionName">       Name of the action. </param>
        /// <param name="parameterNames">   The parameter names. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames)
            : this(sampleDirection, controllerName, actionName, parameterNames)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            MediaType = mediaType;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name of the controller. </summary>
        ///
        /// <value> The name of the controller. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ControllerName { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the name of the action. </summary>
        ///
        /// <value> The name of the action. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ActionName { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the media type. </summary>
        ///
        /// <value> The media type. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public MediaTypeHeaderValue MediaType { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the parameter names. </summary>
        ///
        /// <value> A list of names of the parameters. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public HashSet<string> ParameterNames { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the type of the parameter. </summary>
        ///
        /// <value> The type of the parameter. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Type ParameterType { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the <see cref="SampleDirection"/>. </summary>
        ///
        /// <value> The sample direction. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public SampleDirection? SampleDirection { get; private set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determines whether the specified object is equal to the current object. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="obj">  The object to compare with the current object. </param>
        ///
        /// <returns>
        ///     true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override bool Equals(object obj)
        {
            HelpPageSampleKey otherKey = obj as HelpPageSampleKey;
            if (otherKey == null)
            {
                return false;
            }

            return String.Equals(ControllerName, otherKey.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                String.Equals(ActionName, otherKey.ActionName, StringComparison.OrdinalIgnoreCase) &&
                (MediaType == otherKey.MediaType || (MediaType != null && MediaType.Equals(otherKey.MediaType))) &&
                ParameterType == otherKey.ParameterType &&
                SampleDirection == otherKey.SampleDirection &&
                ParameterNames.SetEquals(otherKey.ParameterNames);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Serves as the default hash function. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A hash code for the current object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override int GetHashCode()
        {
            int hashCode = ControllerName.ToUpperInvariant().GetHashCode() ^ ActionName.ToUpperInvariant().GetHashCode();
            if (MediaType != null)
            {
                hashCode ^= MediaType.GetHashCode();
            }
            if (SampleDirection != null)
            {
                hashCode ^= SampleDirection.GetHashCode();
            }
            if (ParameterType != null)
            {
                hashCode ^= ParameterType.GetHashCode();
            }
            foreach (string parameterName in ParameterNames)
            {
                hashCode ^= parameterName.ToUpperInvariant().GetHashCode();
            }

            return hashCode;
        }
    }
}
