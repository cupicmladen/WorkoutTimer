using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WorkoutTimer.UserControls;
using Xamarin.Forms.Platform.Android;

namespace WorkoutTimer.Droid.Views
{
	public class CircularProgressView : View
	{
		public CircularProgressView(float density, Context context) : base(context)
		{
			this._density = density;
		}

		public CircularProgress CircularProgress { get; set; }

		public new int Width
		{
			get { return base.Width - (int)(Resize(CircularProgress.Padding.HorizontalThickness)); }
		}

		public new int Height
		{
			get { return base.Height - (int)(Resize(CircularProgress.Padding.VerticalThickness)); }
		}

		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);
			HandleShapeDraw(canvas);
		}

		protected virtual void HandleShapeDraw(Canvas canvas)
		{
			_x = GetX() + Resize(CircularProgress.Padding.Left);
			_y = GetY() + Resize(CircularProgress.Padding.Top);
			_cx = _x + Width / 2;
			_cy = _y + Height / 2;

			var rectF = GetPortraitRectF();
			HandleArcDraw(p => canvas.DrawArc(rectF, 0, 360, false, p), CircularProgress.StrokeColor, CircularProgress.StrokeWidth);

			//HandleArcDraw(p => canvas.DrawArc(rectF, _odometerStartPosition, 360 * (CircularProgress.Indicator / CircularProgress.MaxValueIndicator), false, p), CircularProgress.IndicatorStrokeColor, CircularProgress.StrokeWidth);
			//HandleTextDraw(p => canvas.DrawText(CircularProgress.Text, _cx, _cy - (p.Descent() + p.Ascent() / 2), p), CircularProgress.TextColor, 30, 20);
		}

		private void HandleArcDraw(Action<Paint> drawShape, Xamarin.Forms.Color strokeColor, float lineWidth)
		{
			var strokePaint = new Paint(PaintFlags.AntiAlias);
			strokePaint.SetStyle(Paint.Style.Stroke);
			strokePaint.StrokeWidth = Resize(lineWidth);
			strokePaint.StrokeCap = Paint.Cap.Round;
			strokePaint.Color = strokeColor.ToAndroid();
			drawShape(strokePaint);
		}

		private void HandleTextDraw(Action<Paint> drawShape, Xamarin.Forms.Color textColor, int textSize, int strokeWidth = 0)
		{
			var textPaint = new Paint
			{
				AntiAlias = true,
				Color = textColor.ToAndroid(),
				TextSize = textSize,
				StrokeWidth = strokeWidth,
				TextAlign = Paint.Align.Center,
			};

			textPaint.SetStyle(Paint.Style.FillAndStroke);
			drawShape(textPaint);
		}

		private RectF GetPortraitRectF()
		{
			var left = _x;
			var top = _y;
			var right = _x + Width;
			var bottom = _y + Width;

			var rectF = new RectF(left, top, right, bottom);
			return rectF;
		}

		private float Resize(float input)
		{
			return input * _density;
		}

		private float Resize(double input)
		{
			return Resize((float)input);
		}

		private readonly float _density;
		private readonly float _quarterTurnCounterClockwise = -90;
		private readonly float _odometerStartPosition = 135;
		private float _x;
		private float _y;
		private float _cx;
		private float _cy;
		private float _radius;
	}
}