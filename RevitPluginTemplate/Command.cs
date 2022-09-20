using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using RevitPluginTemplate;

namespace RevitPluginTemplate
{
    [Transaction(TransactionMode.Manual)]


    internal class Command : IExternalCommand
    {
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument; Document doc = uidoc.Document;
            Application app = uiapp.Application;

            using (System.Windows.Forms.Form form = new Form1(doc))
            {
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    return Result.Succeeded;

                }
                else
                {
                    return Result.Cancelled;
                }

            }






           
                
        }
        
    }
}
