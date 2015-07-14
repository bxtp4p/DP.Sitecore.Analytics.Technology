using Sitecore.Analytics.Model;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Aggregation.Dimensions
{
    public class ByScreen : VisitDimensionBase
    {
        public ByScreen(Guid dimensionId) : base (dimensionId)
        { }

        protected override string GetKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            return GetScreenKey(context);
        }

        private static string GetScreenKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {

            try
            {
                ScreenData screen = context.Visit.Screen;

                return screen.ScreenHeight.ToString() + " x " + screen.ScreenWidth.ToString();
            }
            catch
            {
                return null;
            }
        }

        protected override bool HasDimensionKey(global::Sitecore.Analytics.Aggregation.Data.Model.IVisitAggregationContext context)
        {
            return GetScreenKey(context) != null;
        }



    }
}
