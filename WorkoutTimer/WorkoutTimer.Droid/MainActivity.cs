using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Media;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace WorkoutTimer.Droid
{
	[Activity(Label = "WorkoutTimer", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());

			var color = Color.FromHex("#1A1A1A");
			SetStatusBarColor(color.ToAndroid());

			Window.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
		}

		protected override void OnPause()
		{
			base.OnPause();
			SendMessage();
		}

		protected override void OnResume()
		{
			base.OnResume();

			var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.CancelAll();

		}

		protected override void OnDestroy()
		{
			var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.CancelAll();

			base.OnDestroy();
		}

		private void SendMessage()
		{
			var notificationIntent = new Intent(this, typeof(MainActivity));
			notificationIntent.AddCategory(Intent.CategoryLauncher);
			notificationIntent.SetAction(Intent.ActionMain);

			var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.OneShot);

			var notificationBuilder = new Notification.Builder(this)
				.SetSmallIcon(Resource.Drawable.icon)
				.SetContentTitle("WorkoutTimer")
				.SetContentText("Opan last activity")
				.SetAutoCancel(true)
				.SetContentIntent(pendingIntent);

			var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
			notificationManager.Notify(0, notificationBuilder.Build());
		}
	}
}

