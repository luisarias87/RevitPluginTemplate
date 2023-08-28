using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;


namespace RevitPluginTemplate.CableTray
{
    public class CableTrayInfo
    {
        public Element CableTray { get; private set; }

        public double BottomElevation { get; private set; }

        public double TopElevation { get; private set; }

        public Level ReferenceLevel { get; private set; }

        public double Width { get; private set; }

        public double Length { get; private set; }

        public string Comments { get; private set; }


        public CableTrayInfo(Element cableTray)
        {
            CableTray = cableTray;
            InitializeParameters();
        }

        private void InitializeParameters() 
        {
             BottomElevation =  GetBottomElevation();
        }


        public double GetBottomElevation() 
        {
            Parameter parameter = CableTray.LookupParameter("Bottom Elevation"); ;

            return parameter?.AsDouble() ?? 0.0;
        }
        public double GetTopElevation()
        {
            Parameter parameter = CableTray.LookupParameter("Top Elevation"); ;

            return parameter?.AsDouble() ?? 0.0;
        }

        public Level GetReferenceLevel() 
        {
            Parameter parameter = CableTray.LookupParameter("Reference Level");

            if (parameter != null && parameter.HasValue && parameter.StorageType == StorageType.ElementId)
            {
                ElementId levelId = parameter.AsElementId();

                if (levelId != ElementId.InvalidElementId) 
                {
                    Element levelElement  = CableTray.Document.GetElement(levelId);
                    if (levelElement is Level level)
                    {
                        return level;
                    }
                }
            }
            return null;

        }
    }
}
