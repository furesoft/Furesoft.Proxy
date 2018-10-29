using System;

namespace Furesoft.Proxy.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SearchableCommandAttribute : Attribute
    {
        public string Name { get; set; }

        public SearchableCommandAttribute(string name)
        {
            Name = name;
        }
    }
}