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
            var tabandPanel = CreateTabandPanel(application);
            
            tabandPanel.AddItem(PushButton(application));
            
            
            
            
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public RibbonPanel CreateTabandPanel(UIControlledApplication a)
        {
            String tab = "MEP Coordination";
            String Panel1 = "Levels";
            a.CreateRibbonTab(tab);
            RibbonPanel ribbonPanel = a.CreateRibbonPanel(tab, Panel1);
            

            return ribbonPanel;

        }


        public PushButtonData PushButton(UIControlledApplication a)
        {
            string assembly = Assembly.GetExecutingAssembly().Location;
            Uri uriImage = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources", "Button1.png"));
            BitmapImage largeImage = new BitmapImage(uriImage);

            PushButtonData button1 = new PushButtonData("Button 1", " Show\nLevels", assembly, "RevitPluginTemplate.Command")
            {
                ToolTip = "This is  a sample tool tip",                
            };
            button1.LargeImage = largeImage;


            return button1;
        }
    }
    
  
}
