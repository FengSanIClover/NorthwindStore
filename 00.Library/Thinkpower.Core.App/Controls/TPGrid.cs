using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Thinkpower.Core.App.Controls
{
    public class TPGrid : Grid
    {
        public static readonly BindableProperty SelectIndexProperty = BindableProperty.Create(nameof(SelectIndex), typeof(int), typeof(TPGrid), 0, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty DatesProperty = BindableProperty.Create(nameof(Dates), typeof(List<string>), typeof(TPGrid), null, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty IndexChangeCommandProperty = BindableProperty.Create(nameof(IndexChangeCommand), typeof(ICommand), typeof(TPGrid), null, defaultBindingMode: BindingMode.TwoWay);
        //public static readonly BindableProperty ColumnCountProperty = BindableProperty.Create(nameof(ColumnCount), typeof(int), typeof(TPGrid), null, defaultBindingMode: BindingMode.TwoWay);
        public int ColumnCount { get; set; } = 5;
        public event EventHandler<int> IndexChanged;
        public ICommand IndexChangeCommand
        {
            get { return (ICommand)GetValue(IndexChangeCommandProperty); }
            set { SetValue(IndexChangeCommandProperty, value); }
        }

        public List<string> Dates
        {
            get { return (List<string>)GetValue(DatesProperty); }
            set { SetValue(DatesProperty, value); }
        }

        public int SelectIndex
        {
            get { return (int)GetValue(SelectIndexProperty); }
            set { SetValue(SelectIndexProperty, value); }
        }

        public TPGrid()
        {
            this.RowSpacing = 0;
            this.ColumnSpacing = 0;
            this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            this.RowDefinitions.Add(new RowDefinition() { Height = 5 });

            for (int i = 0; i < this.ColumnCount; i++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                var label = new Label();
                label.VerticalTextAlignment = TextAlignment.Center;
                label.HorizontalTextAlignment = TextAlignment.Center;

                label.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    CommandParameter = i,
                    Command = new Command((arg) =>
                    {
                        var index = (int)arg;
                        if (this.SelectIndex != index)
                        {
                            this.SelectIndex = index;
                            this.IndexChanged?.DynamicInvoke(this, index);
                            this.IndexChangeCommand?.Execute(index);
                        }

                    })
                });


                var boxView = new BoxView();
                boxView.BackgroundColor = Color.Gray;

                var item = new MyGridViewItem()
                {
                    BoxView = boxView,
                    Label = label
                };
                this._views.Add(item);

                if (this.Dates != null && this.Dates.Count == this.ColumnCount)
                {
                    label.Text = this.Dates[i].Substring(5);
                    item.Value = this.Dates[i];
                }
                this.Children.Add(label, i, 0);
                this.Children.Add(boxView, i, 1);

            }

            this._views[SelectIndex].BoxView.BackgroundColor = Color.Orange;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == DatesProperty.PropertyName)
            {
                if (this.Dates != null && this.Dates.Count == this.ColumnCount)
                {
                    for (int i = 0; i < this.ColumnCount; i++)
                    {
                        var lbl = _views[i].Label;
                        lbl.Text = this.Dates[i].Substring(5);
                        _views[i].Value = this.Dates[i];
                    }
                }
            }

            if (propertyName == SelectIndexProperty.PropertyName)
            {
                for (int i = 0; i < this._views.Count; i++)
                {
                    this._views[i].BoxView.BackgroundColor = i == this.SelectIndex ? Color.Orange : Color.Gray;
                }
            }
        }


        private List<MyGridViewItem> _views = new List<MyGridViewItem>();

    }

    public class MyGridViewItem
    {
        public Label Label { get; set; }
        public BoxView BoxView { get; set; }
        public string Value { get; set; }
    }
}
