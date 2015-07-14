using System;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Core;

namespace DP.Sitecore.Analytics.Technology.Model.Dimensions
{
    public class BrowserKey : DictionaryKey
    {
        public Guid BrowserId { get; private set; }

        public BrowserKey(BrowserValue browserValue)
        {
            this.BrowserId = Hash128.Compute(new object[] { browserValue.BrowserMajorName, browserValue.BrowserMinorName, browserValue.BrowserVersion }).ToGuid();
        }
    }

    public class BrowserValue : DictionaryValue
    {
        public string BrowserMajorName { get; set; }
        public string BrowserMinorName { get; set; }
        public string BrowserVersion { get; set; }
    }

    public class Browsers : Dimension<BrowserKey, BrowserValue>, ISegmentKeyProvider
    {
        public BrowserKey Key { get; set; }
        public BrowserValue Value { get; set; }

        public Browsers() { }

        private Browsers(BrowserKey key, BrowserValue value)
        {
            Key = key;
            Value = value;
        }

        public Browsers Add(string majorName, string minorName, string version)
        {
            BrowserValue value = new BrowserValue { BrowserMajorName = majorName, BrowserMinorName = minorName, BrowserVersion = version };
            BrowserKey key = new BrowserKey(value);
            base.Add(key, value);
            return new Browsers(key, value);
        }

        public string GetSegmentKeyValue()
        {
            return Value.BrowserMajorName;
        }
    }
}
