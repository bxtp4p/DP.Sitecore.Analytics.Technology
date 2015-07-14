using System;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Analytics.Core;

namespace DP.Sitecore.Analytics.Technology.Model.Dimensions
{
    public class UserAgentKey : DictionaryKey
    {
        public Guid UserAgentId { get; private set; }
        public UserAgentKey(UserAgentValue uaValue)
        {
            this.UserAgentId = Hash128.Compute(new object[] { uaValue.UserAgentName }).ToGuid();
        }
    }

    public class UserAgentValue: DictionaryValue
    {
        public string UserAgentName { get; set; }
    }

    public class UserAgents : Dimension<UserAgentKey, UserAgentValue>, ISegmentKeyProvider
    {
        public UserAgentKey Key { get; set; }
        public UserAgentValue Value { get; set; }

        public UserAgents() { }
        
        private UserAgents(UserAgentKey key, UserAgentValue value)
        {
            Key = key;
            Value = value;
        }

        public UserAgents Add(string name)
        {
            UserAgentValue value = new UserAgentValue { UserAgentName = name };
            UserAgentKey key = new UserAgentKey(value);
            base.Add(key, value);
            return new UserAgents(key, value);
        }

        public string GetSegmentKeyValue()
        {
            return Value.UserAgentName;
        }
    }
}
