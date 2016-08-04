using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WorkoutTimer.UserControls
{
	public partial class TimerControl : ContentView
	{
		public TimerControl()
		{
			InitializeComponent();
		}

		public static readonly BindableProperty MinutesProperty = BindableProperty.Create<TimerControl, int>(s => s.Minutes, 0);
		public static readonly BindableProperty SecondsProperty = BindableProperty.Create<TimerControl, int>(s => s.Seconds, 0);

		public int Minutes
		{
			get { return (int)GetValue(MinutesProperty); }
			set { SetValue(MinutesProperty, value); }
		}

		public int Seconds
		{
			get { return (int)GetValue(SecondsProperty); }
			set { SetValue(SecondsProperty, value); }
		}

		private void MinutesPicker_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			Minutes = (sender as Picker).SelectedIndex;
		}

		private void SecondsPicker_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			Seconds = (sender as Picker).SelectedIndex;
		}

		private void MinutesPicker_OnMeasureInvalidated(object sender, EventArgs e)
		{
			MinutesPicker.SelectedIndex = Minutes;
		}

		private void SecondsPicker_OnMeasureInvalidated(object sender, EventArgs e)
		{
			SecondsPicker.SelectedIndex = Seconds;
		}
	}
}
