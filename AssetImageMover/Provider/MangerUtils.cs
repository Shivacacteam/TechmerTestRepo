using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechmerVisionManager.Provider
{
    class MangerUtils
    {
        public static string Quotes(string input)
        {
            String ret = "'" + input + "'";
            return ret;
        }
    }
}
