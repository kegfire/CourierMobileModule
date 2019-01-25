using Xamarin.Forms;

namespace Courier.CustomUIElements
{
	public class CustomLabel : Label
	{
		public static readonly BindableProperty FontSizeFactorProperty =
			BindableProperty.Create(
				"FontSizeFactor", typeof(double), typeof(CustomLabel),
				defaultValue: 1.0, propertyChanged: OnFontSizeFactorChanged);

		public double FontSizeFactor
		{
			get => (double)GetValue(FontSizeFactorProperty);
			set => SetValue(FontSizeFactorProperty, value);
		}

		private static void OnFontSizeFactorChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((CustomLabel)bindable).OnFontSizeChangedImpl();
		}

		public static readonly BindableProperty NamedFontSizeProperty =
			BindableProperty.Create(
				"NamedFontSize", typeof(NamedSize), typeof(CustomLabel),
				defaultValue: NamedSize.Small, propertyChanged: OnNamedFontSizeChanged);

		public NamedSize NamedFontSize
		{
			get => (NamedSize)GetValue(NamedFontSizeProperty);
			set => SetValue(NamedFontSizeProperty, value);
		}

		private static void OnNamedFontSizeChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((CustomLabel)bindable).OnFontSizeChangedImpl();
		}

		protected virtual void OnFontSizeChangedImpl()
		{
			if (this.FontSizeFactor != 1)
				this.FontSize = (this.FontSizeFactor * Device.GetNamedSize(NamedFontSize, typeof(Label)));
		}
	}
}