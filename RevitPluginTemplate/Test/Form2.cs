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
    public partial class Form2 : System.Windows.Forms.Form
    {
        Document Doc;
        public Form2(Document doc)
        {
            InitializeComponent();
            Doc = doc;
        }

        private void testButton_Click(object sender, EventArgs e)
        {

            using (Transaction lvlTrans = new Transaction(Doc, "Create Floor Plan"))
            {
                try
                {
                    //Obtain family symbol for floor plans
                    ViewFamilyType viewFamily = new FilteredElementCollector(Doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>().First(x => x.ViewFamily == ViewFamily.FloorPlan);



                    // Ilist of Level Elements
                    IList<Element> levelsCollector = new FilteredElementCollector(Doc).OfCategory(BuiltInCategory.OST_Levels).ToElements();
                // Grab selected string from Combo box
                string selectedLevel = this.levelsComboBox.SelectedItem.ToString();
                // For each level in the Document, See if the name equals the string in Combo Box
                Element levelSelected = null;
                foreach (Element lv in levelsCollector)
                    {
                        if (lv.Name == selectedLevel)
                        {
                            levelSelected = lv;
                        }
                    
                }
                    lvlTrans.Start();
                    // Create new Floor plan
                    ViewPlan newFloorPlan = ViewPlan.Create(Doc, viewFamily.Id, levelSelected.Id);
                    Parameter ViewTemplate = newFloorPlan.get_Parameter(BuiltInParameter.VIEW_TEMPLATE);
                    ViewTemplate.Set(ElementId.InvalidElementId);
                    lvlTrans.Commit();                   
                }
                catch (Exception)
                {

                    throw;
                }
            }                            
            DialogResult = DialogResult.OK;
            Close();
            
        }
        
        
        private void Form2_Load(object sender, EventArgs e)
        {
            // Get all levels
            IList<Element> levelsCol = new FilteredElementCollector(Doc).WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Levels).ToElements().ToList();
            // add Levels as strings to Combo Box
            
            try
            {
                IList<string> levels = new List<string>();
                foreach (Element l in levelsCol)
                {

                    levels.Add(l.Name);
                }
                levelsComboBox.DataSource = levels;
                
            }
            catch (Exception ex)
            {

                TaskDialog.Show("Error", ex.ToString());
            }

            
           





        }
    }
}
