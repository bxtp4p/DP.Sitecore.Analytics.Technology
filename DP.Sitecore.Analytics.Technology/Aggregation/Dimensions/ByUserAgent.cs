using Sitecore.Analytics.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Aggregation.Dimensions
{
    public class ByUserAgent : VisitDimensionBase
    {
        public ByUserAgent(Guid dimensionId) : base(dimensionId)
        { }

        protected override string GetKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            return context.Visit.UserAgent;
        }

        protected override bool HasDimensionKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            return !string.IsNullOrEmpty(context.Visit.UserAgent);
        }
    }
}
