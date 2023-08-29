using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitPluginTemplate.CableTray;
using RevitPluginTemplate.Views;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RevitPluginTemplate.Clearance
{
    [Transaction(TransactionMode.Manual)]
    public class ClearanceCommand : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            var app = uiapp.Application;

            //Instance of the UI Window
            ClearanceWindow clearanceWindow = new ClearanceWindow(doc);
            clearanceWindow.ShowDialog();
            

            if (!string.IsNullOrEmpty(clearanceWindow.SelectedFamilyName))
            {
                // Filter and loop to find the selected family symbol
                var familySymbolCollector = new FilteredElementCollector(doc)
                    .OfClass(typeof(FamilySymbol))
                    .OfCategory(BuiltInCategory.OST_GenericModel)
                    .WhereElementIsElementType();

                // Collector of Cable Tray elements in Revit Document
                var cTrayElements = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_CableTray)
                    .WhereElementIsNotElementType()
                    .ToElements();
                
                // Instance of the CableTrayManager class to store cable tray elements
                var cableTrayManager = new CableTrayManager();


                // Creating a new instance of CableTrayElement class of each
                // Cable Tray element in the Collector and adding the element to 
                // the Cable tray manager class
                foreach (var cableTrayElement in cTrayElements)
                {
                    ICableTrayElement revitCableTrayElement = new CableTrayElement(cableTrayElement);

                    //Add the cable tray element to the manager
                    cableTrayManager.AddCableTrayElement(revitCableTrayElement);



                    using (Transaction t = new Transaction(doc, "Placing Family"))
                    {

   
                        t.Start();
                        FamilySymbol famToPlace = null;

                        foreach (FamilySymbol symbol in familySymbolCollector)
                        {


                            if (symbol.Family.Name == clearanceWindow.SelectedFamilyName)
                            {
                                famToPlace = symbol;
                                break;
                            }
                        }


                        if (famToPlace != null)
                        {
                            famToPlace.Activate();
                            //retrieve the CableTrayInfo instance for the current tray element
                            CableTrayInfo cableTrayInfo = revitCableTrayElement.GetCableTrayInfo();

                            //Create the placement line for the family instance usind Cable Tray info
                            Autodesk.Revit.DB.Line placementLine = cableTrayInfo.PlacementLine;

                            FamilyInstance familyInstance = doc.Create.NewFamilyInstance(placementLine, famToPlace, cableTrayInfo.ReferenceLevel, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                            // Find and set the AFF parameter
                            var instanceHParameter = familyInstance.GetParameters("AFF").FirstOrDefault(p => p.Definition.Name == "AFF");


                            if (instanceHParameter != null)
                            {

                               

                                if (instanceHParameter != null)
                                {
                                    instanceHParameter.Set(cableTrayInfo.TopElevation);
                                }

                            }
                        }
                        else
                        {
                            TaskDialog.Show("FamilyNot Found", "the selected family symbol was not found in the project");
                        }





                        t.Commit();
                    }

                }


              
                


            }
            return Result.Succeeded;
        }
    }
}
