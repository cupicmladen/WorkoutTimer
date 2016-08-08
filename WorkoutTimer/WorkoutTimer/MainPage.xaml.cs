using WorkoutTimer.ViewModels;
using Xamarin.Forms;

namespace WorkoutTimer
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			BindingContext = new MainViewModel();

			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}
