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
    internal class NewSheetAndView
    {
        public void newSheetAndView(ExternalCommandData commandData) 
        {

            // Get application and document objects
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument; Document doc = uidoc.Document;
            Application app = uiapp.Application;


            // Get an available titleblock  - Filtered element collector
            FilteredElementCollector colTitleBlocks = new FilteredElementCollector(doc);
            colTitleBlocks.OfClass(typeof(FamilySymbol));
            colTitleBlocks.OfCategory(BuiltInCategory.OST_TitleBlocks);
            //  Grab available viewplans from document throught filtered element collector
            FilteredElementCollector colViewPlans = new FilteredElementCollector(doc);
            colViewPlans.OfClass(typeof(ViewPlan));
            List<ViewPlan> vPlans = new List<ViewPlan>();
            foreach (ViewPlan viewPlan in colViewPlans)
            {
                if (viewPlan.ViewType == ViewType.FloorPlan && viewPlan.CanViewBeDuplicated(ViewDuplicateOption.Duplicate) == true)
                {
                    vPlans.Add(viewPlan);
                    vPlans.ToString();
                }
            }




            // Grab first view plan as example 
            ViewPlan duplicatedPlan = vPlans[1];

            // Grab viewport labels "viewtitles"
            FilteredElementCollector colViewTitles = new FilteredElementCollector(doc);
            colViewTitles.OfClass(typeof(FamilySymbol));
            colViewTitles.OfCategory(BuiltInCategory.OST_ViewportLabel);

            // Grab Viewports
            FilteredElementCollector colViewPorts = new FilteredElementCollector(doc);
            colViewTitles.OfClass(typeof(FamilySymbol));
            colViewTitles.OfCategory(BuiltInCategory.OST_Viewports);


            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    tx.Start("Create New Sheet");
                    // Check for titleblocks, if none, load a family
                    if (colTitleBlocks != null)
                    {
                        LoadTitleBlock loadTitleBlock = new LoadTitleBlock();
                        loadTitleBlock.loadFamily(commandData);
                    }

                    // grab first

                    FamilySymbol firstTitleBlock = colTitleBlocks.FirstElement() as FamilySymbol;

                    // create a new sheet
                    ViewSheet viewSheet = ViewSheet.Create(doc, firstTitleBlock.Id);
                    if (viewSheet == null)
                    {
                        throw new Exception("Failed to Create New Sheet");
                    }
                    // duplicate viewPlan
                    ElementId duplicatedPlanId = duplicatedPlan.Duplicate(ViewDuplicateOption.Duplicate);

                    // add passed in view onto center of sheet 
                    UV location = new UV((viewSheet.Outline.Max.U - viewSheet.Outline.Min.U) / 2, (viewSheet.Outline.Max.V - (viewSheet.Outline.Min.V) / 2));

                    // create viewport
                    Viewport newViewPort = Viewport.Create(doc, viewSheet.Id, duplicatedPlan.Id, new XYZ(location.U, location.V, 0));
                    // set viewport settings
                    newViewPort.LookupParameter("View Scale").Set(24);
                    newViewPort.SetBoxCenter(new XYZ(location.U, location.V, 0));
                    bool newViewportTypeParameterShowLabel = doc.GetElement(newViewPort.GetTypeId()).get_Parameter(BuiltInParameter.VIEWPORT_ATTR_SHOW_LABEL).Set(1);

                    // load viewtitle family
                    if (colViewTitles.Count() == 0)
                    {
                        LoadViewTitle loadViewTitle = new LoadViewTitle();
                        loadViewTitle.loadFamily(commandData);
                    }


                    // set view title parameter
                    bool newViewportTypeParameterChangeLabel = doc.GetElement(newViewPort.GetTypeId()).get_Parameter(BuiltInParameter.VIEWPORT_ATTR_LABEL_TAG).Set(colViewTitles.FirstElementId());



                    tx.Commit();

                }
                catch (Exception)
                {

                    throw;
                }
            }


        }
    }
}
