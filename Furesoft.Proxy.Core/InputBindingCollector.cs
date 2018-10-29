using Furesoft.Proxy.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Furesoft.Proxy.Core
{
    public static class InputBindingCollector
    {
        public static Dictionary<KeyGesture, ICommand> KeyBindings = new Dictionary<KeyGesture, ICommand>();
        public static Dictionary<MouseGesture, ICommand> MouseBindings = new Dictionary<MouseGesture, ICommand>();

        public static void Collect(Assembly a)
        {
            var kgc = new KeyGestureConverter();
            var mgc = new MouseGestureConverter();

            //Collect gestures
            CollectAttribute<KeyBindingCommandAttribute>(a, (at, inst) =>
            {
                KeyBindings.Add((KeyGesture)kgc.ConvertFromString(at.KeyBindingString), inst);
            });
            CollectAttribute<MouseBindingCommandAttribute>(a, (at, inst) =>
            {
                MouseBindings.Add((MouseGesture)mgc.ConvertFromString(at.MouseBindingString), inst);
            });
        }

        // add systemwide and context bound bindings
        public static void ApplyGesturesTo(Control ctrl)
        {
            foreach (var kb in KeyBindings)
            {
                ctrl.InputBindings.Add(new InputBinding(kb.Value, kb.Key));
            }
            foreach (var mb in MouseBindings)
            {
                ctrl.InputBindings.Add(new InputBinding(mb.Value, mb.Key));
            }
        }

        private static void CollectAttribute<Att>(Assembly a, Action<Att, ICommand> callback)
            where Att : Attribute
        {
            var types = a.GetTypes();

            foreach (var t in types)
            {
                var att = t.GetCustomAttribute<Att>();

                if (att != null)
                {
                    var instance = (ICommand)Activator.CreateInstance(t);

                    callback(att, instance);
                }
            }
        }
    }
}