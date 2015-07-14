using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Sitecore.Analytics.Technology.Model.Dimensions
{
    public class DimensionKeys : Dimension<DimensionKeys.Key, DimensionKeys.Value>
    {

        public DimensionKeys() { }

        public Hash64 Add(string value)
        {
            Hash64 id = Hash64.Compute(value);
            base.Add(new Key(id), new Value(value));
            return id;
        }

        public class Key : DictionaryKey
        {
            public Key(Hash64 id)
            {
                this.DimensionKeyId = id;
            }

            public Hash64 DimensionKeyId { get; private set; }
        }

        public class Value: DictionaryValue
        {
            public Value (string value)
            {
                this.DimensionKey = value;
            }

            public string DimensionKey { get; set; }

        }
    }
}
