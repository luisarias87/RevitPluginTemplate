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

            ClearanceWindow clearanceWindow = new ClearanceWindow(doc);
            clearanceWindow.ShowDialog();

            var cableTrayManager = new CableTrayManager();

            var cTray = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_CableTray).WhereElementIsNotElementType();

            foreach (var cableTrayElement in cTray)
            {
                var revitCableTrayElement = new CableTrayElement(cableTrayElement);
                cableTrayManager.AddCableTRayElement(revitCableTrayElement);
            }
            var cableTrayInfos = cableTrayManager.GetCableTrayInfos();

            foreach (var cableTrayInfo in cableTrayInfos)
            {
                var doul = cableTrayInfo.BottomElevation;
            }

            if (!string.IsNullOrEmpty(clearanceWindow.SelectedFamilyName))
            {
                // Filter and loop to find the selected family symbol
                var familySymbolCollector = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_GenericModel).
                    WhereElementIsElementType();

                var levelCollector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType().ToElements().First() as Level;

                

                //CableTrayInfo cTrayInfo = new CableTrayInfo(cTray);

                //var bottomElevation = cTrayInfo.BottomElevation;

                //var cTrayHeight = cTray.LookupParameter("Top Elevation");

                //var location = cTray.Location as LocationCurve;

                //// use the normalized cable tray direction vector to determine the placement point
                //var cableTrayDirection = location.Curve.GetEndPoint(1) - location.Curve.GetEndPoint(0);
                //var normalizedDirection = cableTrayDirection.Normalize();

                //var start = location.Curve.GetEndPoint(0);
                //var end = location.Curve.GetEndPoint(1);
                //var desiredFamilyLength = location.Curve.Length;

                //// calculate the placement point based on the normalized direction
                //XYZ placementPoint = start  + normalizedDirection - normalizedDirection ;

                //XYZ placementEndPoint  = placementPoint + normalizedDirection * desiredFamilyLength;


                //// create the line for family placement
                //Line line = Line.CreateBound(placementPoint,placementEndPoint);



                ////// create a new line using the placement point and the normalized direction
                ////Line line = Line.CreateBound(placementPoint,placementPoint + normalizedDirection *desiredFamilyLength);

                //FamilySymbol famToPlace = null;

                //foreach (FamilySymbol symbol in familySymbolCollector)
                //{
                    

                //    if (symbol.Family.Name == clearanceWindow.SelectedFamilyName)
                //    {
                //        famToPlace = symbol;
                //        break;
                //    }
                //}
                
                //using (Transaction t = new Transaction(doc, "Placing Family"))
                //{
                //    t.Start();

                    

                //    if (famToPlace != null)
                //    {
                //        famToPlace.Activate();
                //    }

                //    FamilyInstance familyInstance = doc.Create.NewFamilyInstance(line, famToPlace, levelCollector, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                //    // Find and set the AFF parameter
                //    var instanceHParameter = familyInstance.GetParameters("AFF").FirstOrDefault( p => p.Definition.Name == "AFF").AsValueString();


                //    if (instanceHParameter != null)
                //    {
                //        var famCollector = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).FirstOrDefault( f => f.Name == familyInstance.Name) as FamilyInstance;

                //        var param  = famCollector.GetParameters("AFF").FirstOrDefault( p=> p.Definition.Name == "AFF");

                //        if (param != null)
                //        {
                //            param.Set(cTrayHeight.AsDouble());
                //        }

                //    }

                //    t.Commit();
                //}
            }
            return Result.Succeeded;
        }
    }
}
