using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.IO;
using System.Windows.Controls;

namespace RevitPluginTemplate
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel tabandPanel = CreateTabandPanel(application);
            
            tabandPanel.AddItem(PushButton(application));
            tabandPanel.AddItem(TestPushButton(application));
            
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public RibbonPanel CreateTabandPanel(UIControlledApplication a)
        {
            String tab = "Shop Drawings";
            String Panel1 = "Sheets";
            a.CreateRibbonTab(tab);
            RibbonPanel ribbonPanel = a.CreateRibbonPanel(tab, Panel1);
            return ribbonPanel;

        }
        public PushButtonData PushButton(UIControlledApplication a)
        {
            string assembly = Assembly.GetExecutingAssembly().Location;
            Uri uriImage = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "Button1.png"));
            //BitmapImage largeImage = new BitmapImage(uriImage);

            PushButtonData button1 = new PushButtonData("Sample", " Create\nSheets", assembly, "RevitPluginTemplate.Command")
            {
                ToolTip = "This is  a sample tool tip",                
            };
            //button1.LargeImage = largeImage;


            return button1;
        }

        public PushButtonData TestPushButton(UIControlledApplication a) 
        { 
            // button constructor
            string name = "Cable tray Clearance";
            string text = "Cable Tray Clearance";
            string assembly = Assembly.GetExecutingAssembly().Location;
            string className = "RevitPluginTemplate.Clearance.ClearanceCommand";

            // button large image
            //Uri uriImage = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources","test.png"));
            //BitmapImage largeimage = new BitmapImage(uriImage);


            PushButtonData testButton = new PushButtonData(name, text, assembly, className)
            {
                //LargeImage = largeimage,
                ToolTip = "Sample Tool Tip"

            };

            return testButton;
        }
    }
    
  
}
