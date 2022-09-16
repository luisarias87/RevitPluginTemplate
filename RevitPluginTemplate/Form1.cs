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
        private void btn_Create_Click(object sender, EventArgs e)
        {
            using (Transaction transaction = new Transaction(Doc,"Duplicate Views")) 
            {
                transaction.Start();
                IList<Element> ViewPlans = new FilteredElementCollector(Doc).OfClass(typeof(ViewPlan)).ToElements();                                
                string viewsFloorPlans = this.viewsComboBox.SelectedItem.ToString();

                Autodesk.Revit.DB.View viewId = null;

                foreach (Element viewPlan in ViewPlans)
                {
                    if (viewPlan.Name  == viewsFloorPlans)
                    {
                        viewId = viewPlan as Autodesk.Revit.DB.View;
                    }
                }
                if (viewId.CanViewBeDuplicated(ViewDuplicateOption.Duplicate) == true && duplicateRB.Checked == true) 
                {
                    viewId.Duplicate(ViewDuplicateOption.Duplicate);
                }
                if (viewId.CanViewBeDuplicated(ViewDuplicateOption.WithDetailing) == true && duplicateWithDetailingRB.Checked == true)
                {
                    viewId.Duplicate(ViewDuplicateOption.WithDetailing);
                }
                if (viewId.CanViewBeDuplicated(ViewDuplicateOption.AsDependent) == true && duplicateAsDependentRB.Checked == true)
                {
                    viewId.Duplicate(ViewDuplicateOption.AsDependent);
                }
                else
                {
                    MessageBox.Show("View Cannot be duplicated!");
                }
                    
                 transaction.Commit();  

            }
               



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

            if (SheetName == "" || SheetNumber == "")
            {
                TaskDialog.Show("Null value", string.Format("One or more fileds are missing"));
            }
            else
            {
                using (Transaction sheetTrans = new Transaction(Doc, "Create Sheets"))
                {
                    sheetTrans.Start();
                    ViewSheet newSheet = ViewSheet.Create(Doc, titleBlock.Id);
                    newSheet.Name = SheetName;
                    newSheet.SheetNumber = SheetNumber;
                    sheetTrans.Commit();
                }
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        public string SheetName 
        {
            get { return this.sheetName.Text; }
        
        }
        public string SheetNumber
        {
            get { return this.sheetNumber.Text; }

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
