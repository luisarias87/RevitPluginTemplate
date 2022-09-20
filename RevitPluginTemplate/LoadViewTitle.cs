using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

namespace RevitPluginTemplate
{
    internal class LoadViewTitle
    {
        public void loadFamily(ExternalCommandData commandData)
        {
            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument; Document doc = uidoc.Document;
            Application app = uiapp.Application;

            // set family to null
            Family family = null;

            // set path
            string path = "C:\\ProgramData\\Autodesk\\RVT 2020\\Libraries\\US Imperial\\Annotations\\View Title.rfa";

            // load family
            doc.LoadFamily(path, out family);

        }

    }
}
