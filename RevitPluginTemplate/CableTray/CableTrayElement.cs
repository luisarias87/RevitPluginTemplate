using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitPluginTemplate.CableTray
{
    public class CableTrayElement : ICableTrayElement
    {
        private Element _cableTrayElement;


        public CableTrayElement( Element cableTrayElement)
        {
            _cableTrayElement = cableTrayElement;

        }
        public CableTrayInfo GetCableTrayInfo()
        {
            return new CableTrayInfo(_cableTrayElement);
        }
    }
}
