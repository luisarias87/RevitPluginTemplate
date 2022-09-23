using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;

namespace RevitPluginTemplate
{
    [Transaction(TransactionMode.Manual)]
    internal class CreateFloorPlan : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Application app = uiapp.Application;

            using (System.Windows.Forms.Form form2 = new Form2(doc)) 
            {
                if (form2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return Result.Succeeded;

                }
                else
                {
                    return Result.Failed;
                }
            
            }
            
                

            



               
        }
    }
}
