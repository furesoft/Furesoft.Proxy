using System;

namespace Furesoft.Proxy.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandContextAttribute : Attribute
    {
        public CommandContextIds[] Ids { get; set; }

        public CommandContextAttribute(params CommandContextIds[] id)
        {
            Ids = id;
        }
    }
}