﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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



            if (!string.IsNullOrEmpty(clearanceWindow.SelectedFamilyName))
            {
                // Filter and loop to find the selected family symbol
                var familySymbolCollector = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_GenericModel).
                    WhereElementIsElementType();

                var levelCollector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType().First();

                var cTray = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_CableTray).WhereElementIsNotElementType().First();

                var location = cTray.Location as LocationCurve;

                var start = location.Curve.GetEndPoint(0);
                var end  = location.Curve.GetEndPoint(1);

                foreach (FamilySymbol symbol in familySymbolCollector)
                {
                    FamilySymbol famToPlace = null;

                    if (symbol.Family.Name == clearanceWindow.SelectedFamilyName)
                    {
                        using (Transaction t = new Transaction(doc, "Placing Family"))
                        {
                            t.Start();
                            
                            famToPlace = symbol;
                            if (famToPlace != null)
                            {
                                famToPlace.Activate();
                                

                            }
                            FamilyInstance familyInstance = doc.Create.NewFamilyInstance(start, symbol, levelCollector,Autodesk.Revit.DB.Structure.StructuralType.NonStructural);

                            t.Commit();
                        }

                    }
                }

                
            }
            return Result.Succeeded;
        }



    }
}
