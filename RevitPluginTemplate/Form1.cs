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
            DuplicateView();
            CreateSheet();
            CreateViewPort();






        }
        public XYZ xyz = new XYZ(0,0,0);
        public Autodesk.Revit.DB.View testView = null;
        public ViewSheet newViewSheet = null;
        public void CreateViewPort() 
        {
            using (Transaction t = new Transaction(Doc, "create Viewport")) 
            
            {

                IList<Element> viewForViewPort = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();

                foreach (Element viewPlan in viewForViewPort)
                {
                    if (viewPlan.Name == testView.Name)
                    {
                        Viewport.Create(Doc, newViewSheet.Id, testView.Id, xyz);
                    }
                }


            }

        
        }
        
        public ViewSheet CreateSheet() 
        {

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
            using (Transaction sheetTrans = new Transaction(Doc, "Create Sheets"))
            {
                sheetTrans.Start();
                ViewSheet newViewSheet = ViewSheet.Create(Doc, titleBlock.Id);
                
                sheetTrans.Commit();
            }
            DialogResult = DialogResult.OK;
            Close();
            return newViewSheet;
        }
        
        
        public Autodesk.Revit.DB.View DuplicateView() 
        {

            using (Transaction transaction = new Transaction(Doc, "Duplicate View"))
            {
                transaction.Start();

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
                    duplicatedView.Duplicate(ViewDuplicateOption.Duplicate); duplicatedView.Name = duplicatedView.Name+"Test" ;
                }

                else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.WithDetailing) == true && duplicateWithDetailingRB.Checked == true)
                {
                    duplicatedView.Duplicate(ViewDuplicateOption.WithDetailing); duplicatedView.Name = duplicatedView.Name + "Test";
                }
                else if (duplicatedView.CanViewBeDuplicated(ViewDuplicateOption.AsDependent) == true && duplicateAsDependentRB.Checked == true)
                {
                    duplicatedView.Duplicate(ViewDuplicateOption.AsDependent);
                    duplicatedView.Name = duplicatedView.Name + "Test";
                }
                else
                {
                    MessageBox.Show("View Cannot be duplicated!");
                }
                transaction.Commit();
                return testView =  duplicatedView;

                
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
        }
   
    }
}
