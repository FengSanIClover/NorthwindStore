using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Thinkpower.Core.App.Controls
{
    public class TPPressStackLayout : StackLayout
    {
        public static readonly BindableProperty PressColorProperty = BindableProperty.Create("PressColor", typeof(Color), typeof(TPPressStackLayout), Color.Default, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(TPPressStackLayout), null, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(TPPressStackLayout), null, defaultBindingMode: BindingMode.TwoWay);

        
        public Color PressColor
        {
            get { return (Color)GetValue(PressColorProperty); }
            set { SetValue(PressColorProperty, value); }
        }

        public event EventHandler Click;

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public void FireClick()
        {
            this.Click?.DynamicInvoke(this,null);
        }
    }
}
