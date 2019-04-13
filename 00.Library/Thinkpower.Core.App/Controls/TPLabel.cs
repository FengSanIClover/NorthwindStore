using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Thinkpower.Core.App.Controls
{
    public class TPLabel : Label
    {
        public TPLabel()
        {

        }

        public static BindableProperty MaxLinesProperty =
            BindableProperty.Create("MaxLines", typeof(int), typeof(TPLabel), int.MaxValue, BindingMode.Default);

        public int MaxLines
        {
            get => (int)GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateFormattedTextBindingContext();
        }

        private void UpdateFormattedTextBindingContext()
        {
            var formattedText = this.FormattedText as FormattedString;

            if (formattedText == null)
                return;

            foreach (var span in formattedText.BindableSpans)
                span.BindingContext = this.BindingContext;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == FormattedTextProperty.PropertyName)
                UpdateFormattedTextBindingContext();
        }
    }

    [ContentProperty("BindableSpans")]
    public class FormattedString : Xamarin.Forms.FormattedString
    {
        private ObservableCollection<TPSpan> _bindableSpans =
            new ObservableCollection<TPSpan>();

        public IList<TPSpan> BindableSpans
        {
            get { return this._bindableSpans; }
        }

        public FormattedString()
        {
            this._bindableSpans.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var bindableSpan in e.OldItems.Cast<TPSpan>())
                    base.Spans.Remove(bindableSpan);
            }
            if (e.NewItems != null)
            {
                foreach (var bindableSpan in e.NewItems.Cast<TPSpan>())
                    base.Spans.Add(bindableSpan);
            }
        }

        /// <param name="text">To be added.</param>
        /// <summary>
        /// Cast a string to a FromattedString that contains 
        /// a single span with no attribute set.
        /// </summary>
        /// <returns>To be added.</returns>
        public static implicit operator FormattedString(string text)
        {
            return new FormattedString
            {
                BindableSpans = {
                    new TPSpan { Text = text ?? "" }
                }
            };
        }
        /// <param name="formatted">To be added.</param>
        /// <summary>
        /// Cast the FormattedString to a string, 
        /// stripping all the attributes.
        /// </summary>
        /// <returns>To be added.</returns>
        public static explicit operator string(FormattedString formatted)
        {
            return formatted.ToString();
        }
    }

    [ContentProperty("Text")]
    public sealed class TPSpan : BindableObject
    {
        Xamarin.Forms.Span _innerSpan;

        public TPSpan() : this(new Xamarin.Forms.Span()) { }

        public TPSpan(Xamarin.Forms.Span span)
        {
            _innerSpan = span;
            // important for triggering property inheritance from parent Label
            this.BackgroundColor = this._innerSpan.BackgroundColor;
            this.FontSize = this._innerSpan.FontSize;
            this.FontAttributes = this._innerSpan.FontAttributes;
            this.FontFamily = this._innerSpan.FontFamily;
            this.ForegroundColor = this._innerSpan.ForegroundColor;
            this.Text = this._innerSpan.Text ?? "";
        }
        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor),
                typeof(Color), typeof(TPSpan), Color.Default);

        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(nameof(FontAttributes),
                typeof(FontAttributes),
                typeof(TPSpan),
                FontAttributes.None);

        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)GetValue(FontAttributesProperty); }
            set { SetValue(FontAttributesProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily),
                typeof(string),
                typeof(TPSpan),
                string.Empty);

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize),
                typeof(double),
                typeof(TPSpan), -1.0, BindingMode.OneWay,
                null, null, null, null,
                bindable => Device.GetNamedSize(
                    NamedSize.Default, typeof(Label)));

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty
            ForegroundColorProperty =
                BindableProperty.Create(nameof(ForegroundColor),
                   typeof(Color),
                   typeof(TPSpan),
                   Color.Default);

        public Color ForegroundColor
        {
            get { return (Color)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text),
                                    typeof(string),
                                    typeof(TPSpan),
                                    string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Xamarin.Forms.Span InnerSpan
        {
            get { return _innerSpan; }
        }

        protected override void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            _innerSpan.BackgroundColor = this.BackgroundColor;
            _innerSpan.FontSize = this.FontSize;
            _innerSpan.FontAttributes = this.FontAttributes;
            _innerSpan.FontFamily = this.FontFamily;
            _innerSpan.ForegroundColor = this.ForegroundColor;
            _innerSpan.Text = this.Text ?? "";
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        public static implicit operator Xamarin.Forms.Span(TPSpan bindableSpan)
        {
            return bindableSpan.InnerSpan;
        }

        public static implicit operator TPSpan(
            Xamarin.Forms.Span span)
        {
            return new TPSpan(span);
        }
    }
}
