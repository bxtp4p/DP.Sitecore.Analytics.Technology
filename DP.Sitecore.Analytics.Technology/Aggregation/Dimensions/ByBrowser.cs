using DP.Sitecore.Analytics.Technology.Model.Dimensions;
using Sitecore.Analytics.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Aggregation.Dimensions
{
    public class ByBrowser : VisitDimensionBase
    {
        public ByBrowser(Guid dimensionId) : base(dimensionId) { }

        protected override string GetKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            return GetBrowserKey(context);
        }

        private static string GetBrowserKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            try
            {
                BrowserData browser = context.Visit.Browser;
                return browser.BrowserMajorName + " " + browser.BrowserMinorName + " " + browser.BrowserVersion;
            }
            catch
            {
                return null;
            }
        }

        protected override bool HasDimensionKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            //should always have a key
            return GetBrowserKey(context) != null;
        }
    }
}
