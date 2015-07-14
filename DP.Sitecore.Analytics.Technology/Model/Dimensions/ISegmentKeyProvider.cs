using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Model.Dimensions
{
    internal interface ISegmentKeyProvider
    {
        string GetSegmentKeyValue();
    }
}
