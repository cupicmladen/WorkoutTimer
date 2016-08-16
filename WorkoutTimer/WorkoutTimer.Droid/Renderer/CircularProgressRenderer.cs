using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WorkoutTimer.Droid.Renderer;
using WorkoutTimer.Droid.Views;
using WorkoutTimer.UserControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircularProgress), typeof(CircularProgressRenderer))]
namespace WorkoutTimer.Droid.Renderer
{
	public class CircularProgressRenderer : ViewRenderer<CircularProgress, CircularProgressView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<CircularProgress> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || this.Element == null)
				return;

			SetNativeControl(new CircularProgressView(Resources.DisplayMetrics.Density, Context)
			{
				CircularProgress = Element
			});
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "Indicator" || e.PropertyName == "Text" || e.PropertyName == "MaxValueIndicator" || e.PropertyName == "ShowWarning")
				Control.Invalidate();
		}
	}
}