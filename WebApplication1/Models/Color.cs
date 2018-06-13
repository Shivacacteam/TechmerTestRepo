using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechmerVision.Areas.HelpPage.ModelDescriptions;

// compile with: /doc:DocFileName.xml 

namespace TechmerVision.Models
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Color Data Class. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    [ModelName("TechmerColor")]
    public class Color
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   colorData Stores a list of color values for the color (i.e. r,b,g,a) </summary>
        ///
        /// <value> Information describing the color. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<int> ColorData { get; set; }
        public string Hex { get; set; }
        public List<int> RGB { get; set; }
        public List<int> LAB { get; set; }
        public List<int> HSV { get; set; }
        public List<int> CMYK { get; set; }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   internalColorString is a stringified version of colorData. </summary>
        ///
        /// <value> The internal color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string InternalColorString { get; set; }
        public string InternalLAB { get; set; }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   UnixTimeStamp - usually used to store creation timestamp. </summary>
        ///
        /// <value> The time stamp. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public long TimeStamp { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Stores the workspaceId. </summary>
        ///
        /// <value> The identifier of the workspace. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int WorkspaceId { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Stores a css-ready color Style string. </summary>
        ///
        /// <value> The color style. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ColorStyle { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Stores a human-readable string version of colorStyle. </summary>
        ///
        /// <value> The color string. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string ColorString { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   ValueObj containg Red data( value (0-255) and percentage) </summary>
        ///
        /// <value> The r. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ValueObj R { get; set; }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   ValueObj containg Green data( value (0-255) and percentage) </summary>
        ///
        /// <value> The g. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public ValueObj G { get; set; }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   ValueObj containg Blue data( value (0-255) and percentage) </summary>
        ///
        /// <value> The b. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public ValueObj B { get; set; }

        //color Scheam l.a.b
        public ValueObj L { get; set; }
        public ValueObj A { get; set; }
        public ValueObj Blab { get; set; }

        //color Scheam H.S.V
        public ValueObj H { get; set; }
        public ValueObj S { get; set; }
        public ValueObj V { get; set; }

        //Color CMYK
        public ValueObj C { get; set; }
        public ValueObj M { get; set; }
        public ValueObj Y { get; set; }
        public ValueObj K { get; set; }

        //Public Hex
       
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default Contructor. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Color()
        {
           
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Recommended constructor used to initialize data. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="colorInfo">    Integer array of color values [r,g,b,a]. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Color(int[] colorInfo)
        {
            //ColorData = colorInfo.ToList<int>();
            ColorData = colorInfo.ToList<int>();
            LAB = colorInfo.ToList<int>();
            InternalLAB = string.Join(",", LAB);
            InternalColorString = string.Join(",", ColorData);
            ColorStyle = _displayColorStyle(ColorData);
            ColorString = _displayColorData(ColorData);
            TimeStamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            WorkspaceId = WorkspaceId;
           
            R = new ValueObj
            {
                Value = ColorData[0],
                Percent = (int)Math.Round((double)(ColorData[0] / 256 * 100))
                //Value = RGB[0],
                //Percent = (int)Math.Round((double)(RGB[0] / 256 * 100))
            };
            G = new ValueObj
            {
                Value = ColorData[1],
                //Value = RGB[1],
                Percent = (int)Math.Round((double)(ColorData[1] / 256 * 100))
            };
            B = new ValueObj
            {
                Value = ColorData[2],
                //Value = RGB[2],
                Percent = (int)Math.Round((double)(ColorData[2] / 256 * 100))
            };

            //L.A.B
            L = new ValueObj
            {
                Value = LAB[0],
                Percent = (int)Math.Round((float)(LAB[0] / 256 * 100))
            };
            A = new ValueObj
            {
                Value = LAB[1],
                Percent = (int)Math.Round((float)(LAB[1] / 256 * 100))
            };
            Blab = new ValueObj
            {
                Value = LAB[2],
                Percent = (int)Math.Round((float)(LAB[2] / 256 * 100))
            };

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Builds css-ready color style string for provided color list. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="color">    list of color values i.e. [r,g,b,a]. </param>
        ///
        /// <returns>   css-ready color style string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private string _displayColorStyle(List<int> color)
        {
            var ret = "rgba(" + color[0].ToString() + "," + color[1].ToString() + "," + color[2].ToString() + "," + color[3].ToString() + ")";
            return ret;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Builds human readable string for provided color values list. </summary>
        ///
        /// <remarks>   Aedmonds, 8/25/2017. </remarks>
        ///
        /// <param name="color">    list of color values i.e. [r,g,b,a]. </param>
        ///
        /// <returns>   human readable color string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private string _displayColorData(List<int> color)
        {
            var ret = "Red: " + color[0].ToString() + "\n\rGreen: " + color[1].ToString() + "\n\rBlue: " + color[2].ToString();
            return ret;
        }
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary>   Generic object to store needed color data. </summary>
///
/// <remarks>   Aedmonds, 8/25/2017. </remarks>
////////////////////////////////////////////////////////////////////////////////////////////////////

public class ValueObj
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Stores int color value. </summary>
    ///
    /// <value> The value. </value>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public int Value { get; set; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Stores int color percentage of max. </summary>
    ///
    /// <value> The percent. </value>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public int Percent { get; set; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Default Constructor. </summary>
    ///
    /// <remarks>   Aedmonds, 8/25/2017. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public ValueObj()
    {
    }
}