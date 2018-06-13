using System;

namespace TechmerVision.Areas.HelpPage
{
    /// <summary>
    /// This represents a preformatted text sample on the help page. There's a display template named TextSample associated with this class.
    /// </summary>
    public class TextSample
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="text"> The text. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public TextSample(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            Text = text;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the text. </summary>
        ///
        /// <value> The text. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Text { get; private set; }

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
            TextSample other = obj as TextSample;
            return other != null && Text == other.Text;
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
            return Text.GetHashCode();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns a string that represents the current object. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <returns>   A string that represents the current object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public override string ToString()
        {
            return Text;
        }
    }
}