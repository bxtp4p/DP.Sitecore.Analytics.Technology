using Sitecore.Analytics.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Aggregation.Dimensions
{
    public class ByOS : VisitDimensionBase
    {
        public ByOS(Guid dimensionGuid) : base(dimensionGuid)
        { }



        protected override string GetKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            return GetOSKey(context);
        }

        private static string GetOSKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {

            try
            {
                OperatingSystemData os = context.Visit.OperatingSystem;

                return os.Name + " " + os.MajorVersion + " " + os.MinorVersion;
            }
            catch
            {
                return null;
            }
        }

        protected override bool HasDimensionKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            return GetOSKey(context) != null;
        }
    }
}
