using System;

namespace Furesoft.Proxy.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MouseBindingCommandAttribute : Attribute
    {
        public string MouseBindingString { get; set; }

        public MouseBindingCommandAttribute(string kbs)
        {
            MouseBindingString = kbs;
        }
    }
}