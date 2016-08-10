using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTimer.Models;
using Xamarin.Forms;

namespace WorkoutTimer
{
	public partial class SecondPage : ContentPage
	{
		public SecondPage()
		{
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
			BindingContextChanged += SecondPage_BindingContextChanged;

			Device.StartTimer(TimeSpan.FromSeconds(1), Callback);
		}

		private void SecondPage_BindingContextChanged(object sender, EventArgs e)
		{
			_settings = BindingContext as Settings;

			_restSetTime = _settings.SetRest.Minutes * 60 + _settings.SetRest.Seconds;
			_restExerciseSeconds = _settings.ExerciseRest.Minutes * 60 + _settings.ExerciseRest.Seconds;

			switch (_settings.SelectedDay)
			{
				case Day.Monday:
					SetsToGo.Text = "0 sets done, " + _settings.BenchPress + " more to go!";
					Exercise.Text = "BENCH PRESS";
					break;
				case Day.Wednesday:
					SetsToGo.Text = "0 sets done, " + _settings.Squats + " more to go!";
					Exercise.Text = "SQUATS";
					break;
				case Day.Friday:
					SetsToGo.Text = "0 sets done, " + _settings.DeadLifts + " more to go!";
					Exercise.Text = "DEAD-LIFTS";
					break;
				default:
					break;
			}
		}

		private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
		{
			switch (_settings.SelectedDay)
			{
				case Day.Monday:
					Monday();
					break;
				case Day.Wednesday:
					Monday();
					break;
				case Day.Friday:
					Monday();
					break;
				default:
					break;
			}
		}

		private void Monday()
		{
			if (!_isBenchPressOver)
			{
				_isBenchPressOver = SetLabelsAndTimer("NEXT INCLINE BENCH PRESS", _settings.BenchPress, _settings.InclineBench, "INCLINE BENCH PRESS");
			}
			else if (!_isInclineBenchOver)
			{
				_isInclineBenchOver = SetLabelsAndTimer("NEXT DUMBBELL FLYES", _settings.InclineBench, _settings.DumbbellFlyes, "DUMBBELL FLYES");
			}
			else if (!_isDumbbellFlyesOver)
			{
				_isDumbbellFlyesOver = SetLabelsAndTimer("NEXT CHEST DIPS", _settings.DumbbellFlyes, _settings.ChestDips, "CHEST DIPS");
			}
			else if (!_isChestDipsOver)
			{
				_isChestDipsOver = SetLabelsAndTimer("END", _settings.ChestDips, 0);
			}
		}

		private void Wednesday()
		{
			if (!_isSquatsOver)
			{
				_isSquatsOver = SetLabelsAndTimer("NEXT LEG EXTENSION", _settings.Squats, _settings.LegExtension, "LEG EXTENSION");
			}
			else if (!_isLegExtensionOver)
			{
				_isLegExtensionOver = SetLabelsAndTimer("NEXT FRON SQUATS", _settings.LegExtension, _settings.FrontSquats, "FRON SQUATS");
			}
			else if (!_isFrontSquatsOver)
			{
				_isFrontSquatsOver = SetLabelsAndTimer("NEXT CHEST DIPS", _settings.FrontSquats, 0);
			}
		}

		private void Friday()
		{
			if (!_isDeadLiftsOver)
			{
				_isDeadLiftsOver = SetLabelsAndTimer("NEXT REVERSE LEG EXTENSION", _settings.DeadLifts, _settings.ReverseLegExtension, "REVERSE LEG EXTENSION");
			}
			else if (!_isReverseLegExtensionOver)
			{
				_isReverseLegExtensionOver = SetLabelsAndTimer("NEXT WIDE PULL-UPS", _settings.ReverseLegExtension, _settings.WidePullUps, "WIDE PULL-UPS");
			}
			else if (!_isWidePullUpsOver)
			{
				_isWidePullUpsOver = SetLabelsAndTimer("NEXT NARROW PULL-UPS", _settings.WidePullUps, _settings.NarrowPullUps, "NARROW PULL-UPS");
			}
			else if (!_isNarrowPullUpsOver)
			{
				_isNarrowPullUpsOver = SetLabelsAndTimer("NEXT BENT-OVER ROW", _settings.NarrowPullUps, _settings.BentOverRow, "BENT-OVER ROW");
			}
			else if (!_isBentOverRowOver)
			{
				_isBentOverRowOver = SetLabelsAndTimer("END", _settings.BentOverRow, 0);
			}
		}

		//private bool _isDeadLiftsOver;
		//private bool _isReverseLegExtensionOver;
		//private bool _isWidePullUpsOver;
		//private bool _isNarrowPullUpsOver;
		//private bool _isBentOverRowOver;

		private bool SetLabelsAndTimer(string exerciseRestString, int workoutTotal, int nextWorkoutTotal, string nextExercise = "")
		{
			_workoutDone++;
			if (_workoutDone < workoutTotal)
			{
				var textPlural = _workoutDone == 1 ? " set done, " : " sets done, ";
				SetsToGo.Text = "" + _workoutDone + textPlural + (workoutTotal - _workoutDone) + " more to go!";
				CircularProgress.MaxValueIndicator = _restSetTime;
				CircularProgress.Indicator = _restSetTime;
				_isSetRestPeriod = true;
				return false;
			}

			SetsToGo.Text = exerciseRestString;
			Exercise.Text = nextExercise;
			CircularProgress.MaxValueIndicator = _restExerciseSeconds;
			CircularProgress.Indicator = _restExerciseSeconds;
			_isRestExercisePeriod = true;
			_workoutDone = 0;
			SetsToGo.Text = "0 sets done, " + nextWorkoutTotal + " more to go!";
			return true;
		}

		private bool Callback()
		{
			_totalTimeInSeconds++;
			var timespan = TimeSpan.FromSeconds(_totalTimeInSeconds);
			TotalTime.Text = timespan.ToString(@"hh\:mm\:ss");

			if (_isSetRestPeriod)
			{
				_restSetTime--;
				CircularProgress.Indicator = _restSetTime;

				var timespanRest = TimeSpan.FromSeconds(_restSetTime);
				CircularProgress.Text = timespanRest.ToString(@"mm\:ss");

				if (_restSetTime == 0)
				{
					_isSetRestPeriod = false;
					_restSetTime = _settings.SetRest.Minutes * 60 + _settings.SetRest.Seconds;
				}
			}

			if (_isRestExercisePeriod)
			{
				_restExerciseSeconds--;
				CircularProgress.Indicator = _restExerciseSeconds;

				var timespanExercise = TimeSpan.FromSeconds(_restExerciseSeconds);
				CircularProgress.Text = timespanExercise.ToString(@"mm\:ss");

				if (_restExerciseSeconds == 0)
				{
					_isRestExercisePeriod = false;
					_restExerciseSeconds = _settings.ExerciseRest.Minutes * 60 + _settings.ExerciseRest.Seconds;
				}
			}

			return true;
		}

		private int _totalTimeInSeconds = 0;
		private Settings _settings;

		private int _workoutDone = 0;

		private int _restSetTime;
		private int _restExerciseSeconds;

		private bool _isSetRestPeriod;
		private bool _isRestExercisePeriod;

		private bool _isBenchPressOver;
		private bool _isInclineBenchOver;
		private bool _isDumbbellFlyesOver;
		private bool _isChestDipsOver;

		private bool _isSquatsOver;
		private bool _isLegExtensionOver;
		private bool _isFrontSquatsOver;

		private bool _isDeadLiftsOver;
		private bool _isReverseLegExtensionOver;
		private bool _isWidePullUpsOver;
		private bool _isNarrowPullUpsOver;
		private bool _isBentOverRowOver;
	}
}
