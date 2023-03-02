using System;

namespace ControlConsumo.Shared.Models.Json
{
    public class Parameters
    {
        public Parameters(String key, String value)
        {
            Key = key;
            Value = value;
        }

        public String Key { get; set; }
        public String Value { get; set; }
    }
}
