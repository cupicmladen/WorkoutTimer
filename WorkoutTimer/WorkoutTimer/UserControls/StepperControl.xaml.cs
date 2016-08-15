using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace WorkoutTimer.UserControls
{
	public partial class StepperControl : ContentView
	{
		public StepperControl()
		{
			InitializeComponent();
		}

		public static readonly BindableProperty ValueProperty = BindableProperty.Create<StepperControl, int>(s => s.Value, 0);

		public int Value
		{
			get { return (int)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		private void MinusButton_OnClicked(object sender, EventArgs e)
		{
			if(Value > 0)
				Value--;
		}

		private void PlusButton_OnClicked(object sender, EventArgs e)
		{
			Value++;
		}
	}
}
