using System;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Core;
using Sitecore.Diagnostics;

namespace DP.Sitecore.Analytics.Technology.Model.Dimensions
{
    public class OSKey : DictionaryKey
    {
        public Guid OSId { get; private set; }
        public OSKey (OSValue osValue)
        {
            this.OSId = Hash128.Compute(new object[] { osValue.OSName }).ToGuid();
        }
    }

    public class OSValue : DictionaryValue
    {
        public string OSName { get; set; }
    }

    public class OS : Dimension<OSKey, OSValue>, ISegmentKeyProvider
    {
        public OSKey Key { get; set; }
        public OSValue Value { get; set; }

        public OS() { }

        private OS(OSKey key, OSValue value)
        {
            Key = key;
            Value = value;
        }

        public OS Add(string name)
        {
            OSValue value = new OSValue { OSName = name };
            OSKey key = new OSKey(value);
            try
            {
                base.Add(key, value);
            }
            catch
            {
                //TODO: Log
            }
            return new OS(key, value);
        }

        public string GetSegmentKeyValue()
        {
            return Value.OSName;
        }
    }
}
