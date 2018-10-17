using System.Windows;
using System.Windows.Controls;

namespace Furesoft.Proxy.Core.AttachedProperties
{
    public class IsFocusedProperty
    {
        public static bool GetValue(DependencyObject obj)
        {
            return (bool)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, bool value)
        {
            obj.SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(bool), typeof(IsFocusedProperty), new PropertyMetadata(ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // If we don't have a control, return
            if (!(d is Control control))
                return;

            // Focus this control once loaded
            control.Loaded += (s, se) => control.Focus();
        }
    }

    /// <summary>
    /// Focuses (keyboard focus) this element if true
    /// </summary>
    public class FocusProperty
    {
        public static bool GetValue(DependencyObject obj)
        {
            return (bool)obj.GetValue(ValueProperty);
        }

        public static void SetValue(DependencyObject obj, bool value)
        {
            obj.SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached("Value", typeof(bool), typeof(FocusProperty), new PropertyMetadata(ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Control control))
                return;

            if ((bool)e.NewValue)
                // Focus this control
                control.Focus();
        }
    }
}