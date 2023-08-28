using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitPluginTemplate.CableTray
{
    public interface ICableTrayElement
    {
        CableTrayInfo GetCableTrayInfo();
    }
}
