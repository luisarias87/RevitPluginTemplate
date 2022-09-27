using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace RevitPluginTemplate
{
    
    public partial class Form1 : System.Windows.Forms.Form
    {
        Document Doc;
        IList<string> disciplines = new List<string>();
        public Form1(Document doc)
        {
            InitializeComponent();
            Doc = doc;
            duplicateRB.Checked = true;            
        }               
        public void btn_Create_Click(object sender, EventArgs e)
        {
            CreateView();
            CreateSheet();




            if (disciplineListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a discipline");

            }
            DialogResult = DialogResult.OK;
            Close();
        }

        // list of newly created views
        IList<ElementId> newViews = new List<ElementId>();
        Autodesk.Revit.DB.View duplicatedView = null;
        IList<string> checkedItem = new List<string>();
        ElementId newViewId = null;
        public ElementId CreateView()

        {
            // empty Variable for new Element Id when view is duplicated
            ElementId newView = null;

            // Create a list of checked item objects
            IList<object> selected = new List<object>();
            foreach (var item in disciplineListBox.CheckedItems)
            {
                selected.Add(item);
            }
            // put the transaction in a for each loop
            foreach (var item in selected)
            {
                checkedItem.Add(item.ToString());
                using (Transaction viewTrans = new Transaction(Doc, "Duplicate View"))
                {
                    viewTrans.Start();

                    // Collect all View Plans from project
                    IList<Element> ViewPlans = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();

                    // Retrieve Selected Combo Box Item to string
                    string viewInComboBox = this.viewsComboBox.SelectedItem.ToString();
                    foreach (Element viewPlan in ViewPlans)
                    {
                        if (viewPlan.Name == viewInComboBox)
                        {
                            duplicatedView = viewPlan as Autodesk.Revit.DB.View;
                        }
                    }

                    // View duplication using the selected View duplicate Option
                    if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.Duplicate) == true && duplicateRB.Checked == true)
                    {

                        newView =
                        duplicatedView.Duplicate(ViewDuplicateOption.Duplicate);
                        newViews.Add(newView);
                       
                       
                    }
                    else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.WithDetailing) == true && duplicateWithDetailingRB.Checked == true)
                    {
                        newView =
                        duplicatedView.Duplicate(ViewDuplicateOption.WithDetailing); ;
                        {
                            
                            duplicatedView.get_Parameter(BuiltInParameter.VIEW_NAME).Set(duplicatedView.Name + "_" + disciplineListBox.SelectedItem.ToString());
                        }
                    }
                    else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.AsDependent) == true && duplicateAsDependentRB.Checked == true)
                    {
                        newView =
                     duplicatedView.Duplicate(ViewDuplicateOption.AsDependent);
                        {
                            duplicatedView.get_Parameter(BuiltInParameter.VIEW_NAME).Set(duplicatedView.Name + "_" +" "+ disciplineListBox.SelectedItem.ToString());
                        };

                    }
                    else
                    {
                        MessageBox.Show("View Cannot be duplicated!");
                    }                    
                    viewTrans.Commit();
                }
                using (Transaction viewTran = new Transaction(Doc, "Duplicate View"))
                {                     
                    viewTran.Start();
                    IList<Element> ViewPlanss = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();
                    foreach (var i in ViewPlanss)
                    {
                        if (newView == i.Id)
                        {
                           ViewPlan v =  i as Autodesk.Revit.DB.ViewPlan;
                            
                            v.get_Parameter(BuiltInParameter.VIEW_NAME).Set(v.GenLevel.Name +" "+ "Area "  +" "+ areaName.Text +" "+ item.ToString());
                            viewTran.Commit();

                            
                        }
                        
                    }                                        
                }
                

            }
            newViewId = newView;
            return newView;

            




        }
        public void CreateSheet()
        {
            // Create a list of checked item objects
            IList<object> select = new List<object>();
            foreach (var item in disciplineListBox.CheckedItems)
            {
                select.Add(item);
            }
            

            foreach (var item in newViews) 
            {
                try
                {
                    
                    using (Transaction sheetTrans = new Transaction(Doc, "Create Sheet"))
                    {
                        sheetTrans.Start();
                        // Get Title Blocks
                        IList<Element> tBlockTypes = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_TitleBlocks).WhereElementIsElementType().ToElements();
                        string selectedTBlock = this.sheet_titleBlock.SelectedItem.ToString();
                        Element titleBlock = null;
                        foreach (Element tBlockType in tBlockTypes)
                        {
                            if (tBlockType.Name == selectedTBlock)
                            {
                                titleBlock = tBlockType;
                            }
                        }
                        // Create a filtered element collector to get all levels
                        string sheetLevel = null;
                        FilteredElementCollector lvlCol = new FilteredElementCollector(Doc);
                        ICollection<Element> lvlCollection = lvlCol.OfClass(typeof(Level)).ToElements();
                        foreach (Element l in lvlCol)
                        {
                            Level lvl = l as Level;
                            if (lvl.Name == duplicatedView.GenLevel.Name)
                            {
                                sheetLevel = lvl.Name;
                            }
                        }
                        // Create View Sheet
                        
                        ViewSheet newViewSheet = ViewSheet.Create(Doc, titleBlock.Id);
                        {
                            IList<Element> ViewPlanss = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();
                            foreach (var i in ViewPlanss)
                            {
                                if (item == i.Id)
                                {
                                    ViewPlan v = i as Autodesk.Revit.DB.ViewPlan;

                                    if (areaName != null)
                                    {
                                        newViewSheet.Name = sheetLevel + "Area " + " " + areaName.Text + v.Name ;
                                    }
                                    else
                                    {
                                        newViewSheet.Name = sheetLevel + " " + "Area " + areaName.Text + v.Name;
                                    }
                                }



                            }


                            

                            
                        }
                        // add passed in view onto center of sheet 
                        UV location = new UV((newViewSheet.Outline.Max.U - newViewSheet.Outline.Min.U) / 2, (newViewSheet.Outline.Max.V - (newViewSheet.Outline.Min.V) / 2));
                        // create viewport
                        try
                        {
                             Viewport newViewPort = Viewport.Create(Doc, newViewSheet.Id, item, new XYZ(location.U, location.V, 0));

                            
                            

                            // set viewport settings
                            //newViewPort.LookupParameter("View Scale").Set(64);
                            //newViewPort.SetBoxCenter(new XYZ(location.U, location.V, 0));
                            //bool newViewportTypeParameterShowLabel = Doc.GetElement(newViewPort.GetTypeId()).get_Parameter(BuiltInParameter.VIEWPORT_ATTR_SHOW_LABEL).Set(1);

                            // Grab viewport labels "viewtitles"
                            FilteredElementCollector colViewTitles = new FilteredElementCollector(Doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_ViewportLabel);
                            List<Element> viewTitleElements = new List<Element>();
                            foreach (var viewT in colViewTitles)
                            {
                                viewTitleElements.Add(viewT);

                            }
                            colViewTitles.OfClass(typeof(FamilySymbol));
                            colViewTitles.OfCategory(BuiltInCategory.OST_ViewportLabel);

                        }
                        catch (Exception ex)
                        {
                            TaskDialog.Show("Crash", ex.Message);
                            throw;
                        }

                        sheetTrans.Commit();
                    }


                }
                catch (Exception)
                {

                    throw;
                }
            }
            

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;    
            Close();    
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
                IList<Element> tBlockTypes = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_TitleBlocks).WhereElementIsElementType().ToElements();
                try
                {
                IList<string> titleBlocks = new List<string>();
                foreach (Element tBlock in tBlockTypes)
                {
                    titleBlocks.Add(tBlock.Name);
                }
                sheet_titleBlock.DataSource = titleBlocks;
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Error", ex.ToString());
                }
         

            // Collect all floor plan views and put them in a combo Box
            IList<Element> ViewPlans  = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();
            
            IList<string> floorplanViews = new List<string>();                        
            foreach (ViewPlan floorPlan in ViewPlans)
            {
                if (floorPlan.ViewType == ViewType.FloorPlan && floorPlan.IsTemplate == false)
                {
                    floorplanViews.Add(floorPlan.Name);
                }
            }
            viewsComboBox.DataSource = floorplanViews;

            
            disciplines.Add("Distribution");
            disciplines.Add("Branch Power");
            disciplines.Add("Emergency Power");
            disciplines.Add("Emergency Lighting");
            disciplines.Add("Branch Lighting");
            disciplines.Add("Penetrations");
            disciplines.Add("Equipment Layout");
            disciplines.Add("Hangers");

            disciplineListBox.DataSource = disciplines;
            

        }
   
    }
}
