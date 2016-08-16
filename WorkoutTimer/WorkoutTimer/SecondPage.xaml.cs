using System;
using WorkoutTimer.Interfaces;
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

			_audioService = DependencyService.Get<IAudioService>();

			Device.StartTimer(TimeSpan.FromSeconds(1), TimerTick);
		}

		private void SecondPage_BindingContextChanged(object sender, EventArgs e)
		{
			_settings = BindingContext as Settings;

			_restSetTime = _settings.SetRest.Minutes * 60 + _settings.SetRest.Seconds;
			_restExerciseTime = _settings.ExerciseRest.Minutes * 60 + _settings.ExerciseRest.Seconds;

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
			if (_isTimerInProgress || _isLastSet)
			{
				ForceStopTimer();
				return;
			}

			if (!_isTimerStopped)
			{
				_isTimerStopped = true;
				PlusTime.Text = "+ 00:00";
				_plusTime = 0;
				return;
			}

			_isTimerInProgress = true;

			switch (_settings.SelectedDay)
			{
				case Day.Monday:
					Monday();
					break;
				case Day.Wednesday:
					Wednesday();
					break;
				case Day.Friday:
					Friday();
					break;
				default:
					break;
			}
		}

		private void ForceStopTimer()
		{
			_isTimerStopped = true;
			_isTimerInProgress = false;

			_isSetRestPeriod = false;
			_isRestExercisePeriod = false;

			_restSetTime = _settings.SetRest.Minutes * 60 + _settings.SetRest.Seconds;
			_restExerciseTime = _settings.ExerciseRest.Minutes * 60 + _settings.ExerciseRest.Seconds;

			CircularProgress.Indicator = 0;
			CircularProgress.Text = "00 00";

			PlusTime.Text = "+ 00:00";
			_plusTime = 0;

			if (_isLastSet)
			{
				Exercise.Text = "";
				SetsToGo.Text = "WORKOUT DONE";
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

				_isLastSet = nextWorkoutTotal == 0 && _workoutDone == workoutTotal - 1;

				return false;
			}

			SetsToGo.Text = exerciseRestString;
			Exercise.Text = nextExercise;
			CircularProgress.MaxValueIndicator = _restExerciseTime;
			CircularProgress.Indicator = _restExerciseTime;
			_isRestExercisePeriod = true;
			_workoutDone = 0;
			SetsToGo.Text = "0 sets done, " + nextWorkoutTotal + " more to go!";
			return true;
		}

		private void AnimationTest()
		{
			var a = new Animation
			{
				{0, 0.5, new Animation(f => CircularProgress.Scale = f, 1, 1.2, Easing.Linear, null)},
				{0.5, 1, new Animation(f => CircularProgress.Scale = f, 1.2, 1, Easing.Linear, null)}
			};
			//a.Commit(CircularProgress, "ScaleTo", 16, 700, null, null, () => true);

			CircularProgress.Animate("ScaleTo", a, 16, 700, null, null, () => true);
		}

		private bool TimerTick()
		{
			_totalTimeInSeconds++;
			var timespan = TimeSpan.FromSeconds(_totalTimeInSeconds);
			TotalTime.Text = timespan.ToString(@"hh\:mm\:ss");

			if (!_isTimerStopped)
			{
				_plusTime++;
				var timespanPlus = TimeSpan.FromSeconds(_plusTime);
				PlusTime.Text = "+ " + timespanPlus.ToString(@"mm\:ss");
			}

			if (_isSetRestPeriod)
			{
				StartStopTimer(ref _restSetTime, ref _isSetRestPeriod);
			}

			if (_isRestExercisePeriod)
			{
				StartStopTimer(ref _restExerciseTime, ref _isRestExercisePeriod);
			}

			return true;
		}

		private void StartStopTimer(ref int time, ref bool restPeriod)
		{
			time--;
			CircularProgress.Indicator = time;

			var timespanRest = TimeSpan.FromSeconds(time);
			CircularProgress.Text = timespanRest.ToString(@"mm\ ss");

			if (time == 0)
			{
				restPeriod = false;
				time = _settings.SetRest.Minutes * 60 + _settings.SetRest.Seconds;
				_audioService.PlaySound();
				_isTimerInProgress = false;
				_isTimerStopped = false;
			}
		}

		private void EndWorkoutButton_OnClicked(object sender, EventArgs e)
		{

		}

		private readonly IAudioService _audioService;
		private bool _isTimerInProgress;
		private bool _isTimerStopped = true;
		private bool _isLastSet;

		private int _totalTimeInSeconds = 0;
		private int _plusTime = 0;
		private Settings _settings;

		private int _workoutDone = 0;

		private int _restSetTime;
		private int _restExerciseTime;

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
