using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.Mobile
{
    public class UnfocusedAttached
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(View),
                defaultValue: null,
                defaultBindingMode: BindingMode.OneWay,
                validateValue: null,
                propertyChanged: OnUnfocused);


        public static ICommand GetCommand(BindableObject bindable)
        {
            return (ICommand)bindable.GetValue(CommandProperty);
        }

        public static void SetItemTapped(BindableObject bindable, ICommand value)
        {
            bindable.SetValue(CommandProperty, value);
        }

        public static void OnUnfocused(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as View;

            if (control != null)
            {
                control.Unfocused += Control_Unfocused;
            }
        }

        private static void Control_Unfocused(object sender, FocusEventArgs e)
        {
            var control = sender as View;

            var command = GetCommand(control);

            if (command != null && command.CanExecute(null))
                command.Execute(null);
        }
    }
}
