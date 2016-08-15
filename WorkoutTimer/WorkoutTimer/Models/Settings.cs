using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutTimer.Models
{
	public class Settings
	{
		//public RestPeriod SetRest { get; set; } = new RestPeriod {Minutes = 0, Seconds = 10};
		//public RestPeriod ExerciseRest { get; set; } = new RestPeriod { Minutes = 0, Seconds = 20 };
		public RestPeriod SetRest { get; set; } = new RestPeriod { Minutes = 1, Seconds = 30 };
		public RestPeriod ExerciseRest { get; set; } = new RestPeriod { Minutes = 3, Seconds = 00 };
		public Day SelectedDay { get; set; } = Day.Monday;

		public int BenchPress { get; set; } = 4;
		public int InclineBench { get; set; } = 4;
		public int DumbbellFlyes { get; set; } = 3;
		public int ChestDips { get; set; } = 4;

		public int Squats { get; set; } = 7;
		public int LegExtension { get; set; } = 4;
		public int FrontSquats { get; set; } = 4;

		public int DeadLifts { get; set; } = 5;
		public int ReverseLegExtension { get; set; } = 4;
		public int WidePullUps { get; set; } = 4;
		public int NarrowPullUps { get; set; } = 2;
		public int BentOverRow { get; set; } = 4;
	}

	public class RestPeriod
	{
		public int Minutes { get; set; }
		public int Seconds { get; set; }
	}

	public enum Day
	{
		Monday = 0,
		Wednesday = 1,
		Friday = 2
	}
}
