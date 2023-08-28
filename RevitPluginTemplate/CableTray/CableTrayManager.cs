using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitPluginTemplate.CableTray
{
    public class CableTrayManager
    {
        private List<ICableTrayElement> _cableTrayElements = new List<ICableTrayElement>();

        public void AddCableTRayElement(ICableTrayElement cableTrayElement) 
        {
            _cableTrayElements.Add(cableTrayElement);
        }

        public List<CableTrayInfo> GetCableTrayInfos() 
        {
            List<CableTrayInfo> cableTrayInfos= new List<CableTrayInfo>();

            foreach (var element  in _cableTrayElements)
            {
                cableTrayInfos.Add(element.GetCableTrayInfo());
            }
            return cableTrayInfos;
        }
    }

}
