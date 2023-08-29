using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;

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

        public string Comments { get; set; }

        public LocationCurve Location { get; private set; }

        public XYZ Direction { get; private set; }

        public Line PlacementLine { get; private set; }

       


        public CableTrayInfo(Element cableTray)
        {
            CableTray = cableTray;
            InitializeParameters();
        }

        private void InitializeParameters() 
        {
            BottomElevation =  GetBottomElevation();
            TopElevation = GetTopElevation();
            ReferenceLevel= GetReferenceLevel();
            Width= GetWidth();
            Length= GetLength();
            Location = GetLocation();
            Direction = GetNormalizedDirection();
            PlacementLine= CreatePlacementLine();
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

        public double GetLength() 
        {
            
           var locationCurve =  CableTray.Location as LocationCurve;

            var length = locationCurve?.Curve.Length;

            return length.Value;

        }

        public double GetWidth() 
        {
            var widthParam = CableTray.LookupParameter("Width");

            return widthParam?.AsDouble() ?? 0.0;
        }

        public LocationCurve GetLocation() 
        {

            return CableTray?.Location as LocationCurve;
           
        }

        public XYZ GetNormalizedDirection() 
        {
            var location = CableTray?.Location as LocationCurve;

            var start = location.Curve.GetEndPoint(0);
            var end = location.Curve.GetEndPoint(1);

            var direction = end - start;

            var normalizedDirection  = direction.Normalize();

            return normalizedDirection;
        }
        public Line CreatePlacementLine() 
        {
            var start = this.Location.Curve.GetEndPoint(0);
            var end  = this.Location?.Curve.GetEndPoint(1);

            var startPoint = start + this.Direction - this.Direction;

            var endPoint  = startPoint + this.Direction * this.Length;

            return Line.CreateBound(startPoint, endPoint);
            
        }
    }
}
