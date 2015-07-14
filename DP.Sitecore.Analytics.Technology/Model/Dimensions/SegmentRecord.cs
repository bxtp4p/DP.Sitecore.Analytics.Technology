using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Model.Dimensions
{
    public class SegmentRecords:Dimension<SegmentRecords.Key, SegmentRecords.Value>
    {
        public SegmentRecords() { }

        public Hash64 Add(Guid segmentId, DateTime date, Hash32 siteNameId, Hash64 dimensionKeyId)
        {
            Hash64 id = Hash64.Compute(new object[] { segmentId, date, siteNameId, dimensionKeyId });
            base.Add(new Key(id), new Value(segmentId, date, siteNameId, dimensionKeyId));
            return id;
        }

        public class Key : DictionaryKey
        {
            public Key(Hash64 id)
            {
                this.SegmentRecordId = id;
            }

            public Hash64 SegmentRecordId { get; private set; }
        }

        public class Value : DictionaryValue
        {
            public Value(Guid segmentId, DateTime date, Hash32 siteNameId, Hash64 dimensionKeyId)
            {
                this.SegmentId = segmentId;
                this.Date = date;
                this.SiteNameId = siteNameId;
                this.DimensionKeyId = dimensionKeyId;
            }

            public DateTime Date { get; set; }
            public Hash64 DimensionKeyId { get; set; }
            public Guid SegmentId { get; set; }
            public Hash32 SiteNameId { get; set; }

        }
    }
}
