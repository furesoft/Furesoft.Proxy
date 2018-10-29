using System;

namespace Furesoft.Proxy.Core.Attributes
{
    //ToDo: Add ability to only execute key binding if in correct context like page opened
    [AttributeUsage(AttributeTargets.Class)]
    public class KeyBindingCommandAttribute : Attribute
    {
        public string KeyBindingString { get; set; }

        public KeyBindingCommandAttribute(string kbs)
        {
            KeyBindingString = kbs;
        }
    }
}