using System;

namespace Furesoft.Proxy.Core
{
    public static class CommandContext
    {
        public static CommandContextIds CurrentContext { get; internal set; }
        public static event Action ContextChanged = delegate { };

        public static void SetContext(CommandContextIds id)
        {
            ContextChanged?.Invoke();
            CurrentContext = id;
        }
        public static void AddContext(CommandContextIds id)
        {
            ContextChanged?.Invoke();
            CurrentContext |= id;
        }

        public static bool IsInContext(CommandContextIds id)
        {
            return CurrentContext.HasFlag(id);
        }

        public static bool IsInContext(params CommandContextIds[] ids)
        {
            bool result = false;

            foreach (var id in ids)
            {
                result = CurrentContext.HasFlag(id);
            }

            return result;
        }
    }

    [Flags]
    public enum CommandContextIds : int
    {
        AddFilterOpened = 0b0001,
        FilterPage = 0b0010,
        FilterSelected = 0b0100,
    }
}