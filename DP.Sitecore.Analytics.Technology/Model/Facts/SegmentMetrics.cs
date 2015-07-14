using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Core;
using Sitecore.ExperienceAnalytics.Aggregation.Data.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Model.Facts
{
    public class SegmentMetrics : Fact<SegmentMetrics.Key, SegmentMetricsValue>
    {
        public SegmentMetrics():base(SegmentMetricsValue.Reduce) { }

        public class Key : DictionaryKey
        {
            public byte ContactTransitionType { get; set; }
            public Hash64 SegmentRecordId { get; set; }
        }
    }
}
