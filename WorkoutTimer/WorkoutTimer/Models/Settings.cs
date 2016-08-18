using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkoutTimer.Annotations;
using Xamarin.Forms;

namespace WorkoutTimer.Models
{
	public class Settings : INotifyPropertyChanged
	{
		public Settings()
		{
			BenchPress = 4;
			InclineBench = 4;
			DumbbellFlyes = 3;
			ChestDips = 4;
			Squats = 7;
			LegExtension = 4;
			FrontSquats = 4;
			DeadLifts = 5;
			ReverseLegExtension = 4;
			WidePullUps = 4;
			NarrowPullUps = 2;
			BentOverRow = 4;
		}
		//public RestPeriod SetRest { get; set; } = new RestPeriod {Minutes = 0, Seconds = 10};
		//public RestPeriod ExerciseRest { get; set; } = new RestPeriod { Minutes = 0, Seconds = 20 };
		public RestPeriod SetRest { get; set; } = new RestPeriod { Minutes = 1, Seconds = 30 };
		public RestPeriod ExerciseRest { get; set; } = new RestPeriod { Minutes = 3, Seconds = 00 };
		public Day SelectedDay { get; set; } = Day.Monday;


		public int BenchPress
		{
			get { return _benchPress; }
			set
			{
				_benchPress = value;
				OnPropertyChanged("BenchPress");
				OnPropertyChanged("MondayTotalSets");
			}
		}

		public int InclineBench
		{
			get { return _inclineBench; }
			set
			{
				_inclineBench = value;
				OnPropertyChanged("InclineBench");
				OnPropertyChanged("MondayTotalSets");
			}
		}

		public int DumbbellFlyes
		{
			get { return _dumbbellFlyes; }
			set
			{
				_dumbbellFlyes = value;
				OnPropertyChanged("DumbbellFlyes");
				OnPropertyChanged("MondayTotalSets");
			}
		}

		public int ChestDips
		{
			get { return _chestDips; }
			set
			{
				_chestDips = value;
				OnPropertyChanged("ChestDips");
				OnPropertyChanged("MondayTotalSets");
			}
		}


		public int Squats
		{
			get { return _squats; }
			set
			{
				_squats = value;
				OnPropertyChanged("Squats");
				OnPropertyChanged("WednesdayTotalSets");
			}
		}

		public int LegExtension
		{
			get { return _legExtension; }
			set
			{
				_legExtension = value;
				OnPropertyChanged("LegExtension");
				OnPropertyChanged("WednesdayTotalSets");
			}
		}

		public int FrontSquats
		{
			get { return _frontSquats; }
			set
			{
				_frontSquats = value;
				OnPropertyChanged("FrontSquats");
				OnPropertyChanged("WednesdayTotalSets");
			}
		}


		public int DeadLifts
		{
			get { return _deadLifts; }
			set
			{
				_deadLifts = value;
				OnPropertyChanged("DeadLifts");
				OnPropertyChanged("FridayTotalSets");
			}
		}

		public int ReverseLegExtension
		{
			get { return _reverseLegExtension; }
			set
			{
				_reverseLegExtension = value;
				OnPropertyChanged("ReverseLegExtension");
				OnPropertyChanged("FridayTotalSets");
			}
		}

		public int WidePullUps
		{
			get { return _widePullUps; }
			set
			{
				_widePullUps = value;
				OnPropertyChanged("WidePullUps");
				OnPropertyChanged("FridayTotalSets");
			}
		}

		public int NarrowPullUps
		{
			get { return _narrowPullUps; }
			set
			{
				_narrowPullUps = value;
				OnPropertyChanged("NarrowPullUps");
				OnPropertyChanged("FridayTotalSets");
			}
		}

		public int BentOverRow
		{
			get { return _bentOverRow; }
			set
			{
				_bentOverRow = value;
				OnPropertyChanged("BentOverRow");
				OnPropertyChanged("FridayTotalSets");
			}
		}


		public int MondayTotalSets => BenchPress + InclineBench + DumbbellFlyes + ChestDips;

		public int WednesdayTotalSets => Squats + LegExtension + FrontSquats;

		public int FridayTotalSets => DeadLifts + ReverseLegExtension + WidePullUps + NarrowPullUps + BentOverRow;
		
		public event PropertyChangedEventHandler PropertyChanged;
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private int _benchPress;
		private int _inclineBench;
		private int _dumbbellFlyes;
		private int _chestDips;
		private int _squats;
		private int _legExtension;
		private int _frontSquats;
		private int _deadLifts;
		private int _reverseLegExtension;
		private int _widePullUps;
		private int _narrowPullUps;
		private int _bentOverRow;
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
