using Android.App;
using Android.Media;
using WorkoutTimer.Droid.Implementation;
using WorkoutTimer.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AudioService))]
namespace WorkoutTimer.Droid.Implementation
{
	public class AudioService : IAudioService
	{
		public void PlaySound()
		{
			var media = MediaPlayer.Create(Application.Context, Resource.Raw.timerend);
			media.Start();
		}
	}
}