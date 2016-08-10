using Xamarin.Forms;

namespace WorkoutTimer.UserControls
{
	public class CircularProgress : BoxView
	{
		public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create<CircularProgress, Color>(s => s.StrokeColor, Color.Default);

		public static readonly BindableProperty IndicatorStrokeColorProperty = BindableProperty.Create<CircularProgress, Color>(s => s.IndicatorStrokeColor, Color.Default);

		public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create<CircularProgress, float>(s => s.StrokeWidth, 1f);

		public static readonly BindableProperty IndicatorProperty = BindableProperty.Create<CircularProgress, float>(s => s.Indicator, 0f);

		public static readonly BindableProperty MaxValueIndicatorProperty = BindableProperty.Create<CircularProgress, float>(s => s.MaxValueIndicator, 100f);

		public static readonly BindableProperty TextProperty = BindableProperty.Create<CircularProgress, string>(s => s.Text, string.Empty);

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create<CircularProgress, Color>(s => s.TextColor, Color.Default);

		public static readonly BindableProperty PaddingProperty = BindableProperty.Create<CircularProgress, Thickness>(s => s.Padding, default(Thickness));

		public Color StrokeColor
		{
			get { return (Color)GetValue(StrokeColorProperty); }
			set { SetValue(StrokeColorProperty, value); }
		}

		public Color IndicatorStrokeColor
		{
			get { return (Color)GetValue(IndicatorStrokeColorProperty); }
			set { SetValue(IndicatorStrokeColorProperty, value); }
		}

		public float StrokeWidth
		{
			get { return (float)GetValue(StrokeWidthProperty); }
			set { SetValue(StrokeWidthProperty, value); }
		}

		public float Indicator
		{
			get { return (float)GetValue(IndicatorProperty); }
			set { SetValue(IndicatorProperty, value); }
		}

		public float MaxValueIndicator
		{
			get { return (float)GetValue(MaxValueIndicatorProperty); }
			set { SetValue(MaxValueIndicatorProperty, value); }
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public Color TextColor
		{
			get { return (Color)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		public Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingProperty); }
			set { SetValue(PaddingProperty, value); }
		}
	}
}
