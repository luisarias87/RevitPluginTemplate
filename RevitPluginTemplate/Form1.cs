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
using NuGet.Protocol.Plugins;

namespace RevitPluginTemplate
{
    
    public partial class Form1 : System.Windows.Forms.Form
    {
        Document Doc;
        public Form1(Document doc)
        {
            InitializeComponent();
            Doc = doc;
            duplicateRB.Checked = true;
        }               
        public void btn_Create_Click(object sender, EventArgs e)
        {
            
            CreateSheet();
            DialogResult = DialogResult.OK;
            Close();
        }



        public ViewSheet CreateSheet() 
        {
            using (Transaction sheetTrans = new Transaction(Doc, "Create Sheets"))
            {
                sheetTrans.Start();
                IList<Element> ViewPlans = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();
            string viewInComboBox = this.viewsComboBox.SelectedItem.ToString();

            Autodesk.Revit.DB.View duplicatedView = null;
            foreach (Element viewPlan in ViewPlans)
            {
                if (viewPlan.Name == viewInComboBox)
                {
                    duplicatedView = viewPlan as Autodesk.Revit.DB.View;
                }
            }

            if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.Duplicate) == true && duplicateRB.Checked == true)
            {
                duplicatedView.Duplicate(ViewDuplicateOption.Duplicate); ;
            }

            else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.WithDetailing) == true && duplicateWithDetailingRB.Checked == true)
            {
                duplicatedView.Duplicate(ViewDuplicateOption.WithDetailing);
            }
            else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.AsDependent) == true && duplicateAsDependentRB.Checked == true)
            {
                   
                duplicatedView.Duplicate(ViewDuplicateOption.AsDependent);                                     
            }
            else
            {
                MessageBox.Show("View Cannot be duplicated!");
            }
                IList<Element> colViewPlans = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();

                foreach (ViewPlan vPlan in colViewPlans)
                {
                    if (vPlan.Id == duplicatedView.GetPrimaryViewId())
                    {
                        vPlan.get_Parameter(BuiltInParameter.VIEW_NAME).Set("Hello");

                    }

                }
                
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
            
                
                ViewSheet newViewSheet = ViewSheet.Create(Doc, titleBlock.Id);
                
               
               
                // add passed in view onto center of sheet 
                UV location = new UV((newViewSheet.Outline.Max.U - newViewSheet.Outline.Min.U) / 2, (newViewSheet.Outline.Max.V - (newViewSheet.Outline.Min.V) / 2));
                
                // create viewport
                Viewport newViewPort = Viewport.Create(Doc, newViewSheet.Id, duplicatedView.Id, new XYZ(location.U, location.V, 0));
                
                // set viewport settings
                newViewPort.LookupParameter("View Scale").Set(24);
                newViewPort.SetBoxCenter(new XYZ(location.U, location.V, 0));
                bool newViewportTypeParameterShowLabel = Doc.GetElement(newViewPort.GetTypeId()).get_Parameter(BuiltInParameter.VIEWPORT_ATTR_SHOW_LABEL).Set(1);

                // Grab viewport labels "viewtitles"
                FilteredElementCollector colViewTitles = new FilteredElementCollector(Doc);
                colViewTitles.OfClass(typeof(FamilySymbol));
                colViewTitles.OfCategory(BuiltInCategory.OST_ViewportLabel);

                // set view title parameter
                bool newViewportTypeParameterChangeLabel = Doc.GetElement(newViewPort.GetTypeId()).get_Parameter(BuiltInParameter.VIEWPORT_ATTR_LABEL_TAG).Set(colViewTitles.FirstElementId());
                sheetTrans.Commit();

                return newViewSheet;
            }


        }
        
        
        //public Autodesk.Revit.DB.View DuplicateView() 
        //{

        //    using (Transaction transaction = new Transaction(Doc, "Duplicate View"))
        //    {
        //        transaction.Start();

        //        IList<Element> ViewPlans = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();
        //        string viewInComboBox = this.viewsComboBox.SelectedItem.ToString();

        //        Autodesk.Revit.DB.View duplicatedView = null;
        //        foreach (Element viewPlan in ViewPlans)
        //        {
        //            if (viewPlan.Name == viewInComboBox)
        //            {
        //                duplicatedView = viewPlan as Autodesk.Revit.DB.View;
        //            }
        //        }

        //        if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.Duplicate) == true && duplicateRB.Checked == true)
        //        {
        //            duplicatedView.Duplicate(ViewDuplicateOption.Duplicate); duplicatedView.Name = duplicatedView.Name + "Test";
        //        }

        //        else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.WithDetailing) == true && duplicateWithDetailingRB.Checked == true)
        //        {
        //            duplicatedView.Duplicate(ViewDuplicateOption.WithDetailing); duplicatedView.Name = duplicatedView.Name + "Test";
        //        }
        //        else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.AsDependent) == true && duplicateAsDependentRB.Checked == true)
        //        {
        //            duplicatedView.Duplicate(ViewDuplicateOption.AsDependent);
        //            duplicatedView.Name = duplicatedView.Name + "Test";
        //        }
        //        else
        //        {
        //            MessageBox.Show("View Cannot be duplicated!");
        //        }
        //        transaction.Commit();
        //        return duplicatedView;

        //    }            
        //}

       
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
        }
   
    }
}
