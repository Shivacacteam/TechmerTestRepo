using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechmerVisionManager.Provider
{
    class AppMenu
    {
        private String menu;
        public AppMenu()
        {
            menu = "\r\n0) Exit";
            menu += "\r\n1) Set CORS for Azure Storage";
            menu += "\r\n2) Asset Mover";
            menu += "\r\n3) Delete User";
            menu += "\r\n4) CopyProductTemplates";
        }

        public void writeMenu()
        {
            Console.WriteLine(menu);
        }
    }
}
