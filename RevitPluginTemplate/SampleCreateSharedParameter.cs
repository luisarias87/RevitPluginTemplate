using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
namespace RevitPluginTemplate
{
    internal class SampleCreateSharedParameter
    {
        public void CreateSampleSharedParameters(Document doc, Application app) 
        {
            Category category = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Walls);
            CategorySet categorySet = app.Create.NewCategorySet();
            categorySet.Insert(category);

            string originalFile = app.SharedParametersFilename;
            string tempFile = @"C:\Users\larias\OneDrive - ABLe Communications\Luis BIM\10_Dummy\SharedParameters\SharedParameters.txt";
            try
            {
                app.SharedParametersFilename = tempFile;
                DefinitionFile sharedParameterFile = app.OpenSharedParameterFile();
                foreach (DefinitionGroup dg in sharedParameterFile.Groups)
                {
                    if (dg.Name == "SharedParameters")
                    {
                        ExternalDefinition externalDefinition = dg.Definitions.get_Item("Sample") as ExternalDefinition;
                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Add Shared Parameters");
                            //parameter binding
                            InstanceBinding newIB = app.Create.NewInstanceBinding(categorySet);
                            //parameter group to text
                            doc.ParameterBindings.Insert(externalDefinition, newIB, BuiltInParameterGroup.PG_TEXT);

                            t.Commit();
                        }

                    }
                }


            }
            catch { }
            finally 
            {
                //reset to original file
                app.SharedParametersFilename=originalFile;  
            }

        }

    }
}
