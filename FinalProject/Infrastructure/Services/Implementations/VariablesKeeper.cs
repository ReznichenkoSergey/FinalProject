using FinalProject.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Concurrent;

namespace FinalProject.Infrastructure.Services.Implementations
{
    public class VariablesKeeper : IVariablesKeeper
    {
        readonly ConcurrentDictionary<string, object> pairs;

        public VariablesKeeper()
        {
            pairs = new ConcurrentDictionary<string, object>();
        }
        public object GetVariableByKey(string key)
        {
            if(pairs.ContainsKey(key))
            {
                return pairs[key];
            }
            return null;
        }

        public void SetVariable(string key, object value)
        {
            if (!pairs.ContainsKey(key))
            {
                pairs.TryAdd(key, value);
            }
            else
                pairs[key] = value;
        }
    }
}
