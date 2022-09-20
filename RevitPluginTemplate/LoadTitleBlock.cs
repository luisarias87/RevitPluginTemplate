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
    internal class LoadTitleBlock
    {
        public void loadFamily(ExternalCommandData commandData) 
        {
            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument; Document doc = uidoc.Document;
            Application app = uiapp.Application;
            

            // set path to folder
            DirectoryInfo multiPath = new DirectoryInfo(@"C:\ProgramData\Autodesk\RVT 2022\Libraries\English-Imperial\Structural Precast\Title Blocks");
            FileInfo[] files = multiPath.GetFiles(".rfa");//Grabs rfa files
            
            // set family to null

            Family family = null;

            // create for loop to load all families in folder

            foreach (FileInfo file in files)
            {
                doc.LoadFamily(multiPath + file.Name, out family);
            }


        }
        
    }
}
