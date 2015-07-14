using System;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Core;

namespace DP.Sitecore.Analytics.Technology.Model.Dimensions
{
    public class ScreenKey : DictionaryKey
    {
        public Guid ScreenId { get; private set; }
        public ScreenKey(ScreenValue screenValue)
        {
            this.ScreenId = Hash128.Compute(new object[] { screenValue.Text, screenValue.ScreenHeight, screenValue.ScreenWidth }).ToGuid();
        }
    }

    public class ScreenValue : DictionaryValue
    {
        public string Text { get; set; }
        public int ScreenHeight { get; set; }
        public int ScreenWidth { get; set; }
    }

    public class Screens : Dimension<ScreenKey, ScreenValue>, ISegmentKeyProvider
    {
        public ScreenKey Key { get; set; }
        public ScreenValue Value { get; set; }

        public Screens() { }

        private Screens(ScreenKey key, ScreenValue value)
        {
            Key = key;
            Value = value;
        }

        public Screens Add(int height, int width)
        {
            ScreenValue value = new ScreenValue { Text = height.ToString() + "x" + width.ToString(), ScreenHeight = height, ScreenWidth = width };
            ScreenKey key = new ScreenKey(value);
            base.Add(key, value);
            return new Screens(key, value);
        }

        public string GetSegmentKeyValue()
        {
            return Value.Text;
        }
    }

}
