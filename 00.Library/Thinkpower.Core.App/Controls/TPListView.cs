using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Thinkpower.Core.App.Controls
{
    /// <summary>
    /// 1.增加Item Tap的Binding Command
    /// 2.Item點選後自動反選擇
    /// </summary>
    public class TPListView : ListView
    {
        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create<TPListView, ICommand>(x => x.TapCommand, null);

        public ICommand TapCommand
        {
            get { return (ICommand)GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        public TPListView() : base() { this.SetCommand(); }
        public TPListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy) { this.SetCommand(); }

        public void SetCommand()
        {
            this.ItemTapped += (sender, e) =>
            {
                this.SelectedItem = null;
                if (this.TapCommand != null && this.TapCommand.CanExecute(e.Item))
                    this.TapCommand.Execute(e.Item);
            };
        }
    }
}
