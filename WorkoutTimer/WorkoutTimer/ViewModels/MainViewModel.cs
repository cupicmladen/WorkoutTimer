using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutTimer.Models;
using Xamarin.Forms;

namespace WorkoutTimer.ViewModels
{
	public class MainViewModel : BaseNavigationViewModel
	{
		public MainViewModel()
		{
			Settings = new Settings();
		}

		public Settings Settings { get; set; }

		public ICommand ExitCommand
		{
			get
			{
				return new Command(() =>
				{

				});
			}
		}

		public ICommand NavigateToSecondPageCommand
		{
			get
			{
				return new Command(async () =>
				{
					if(_secondPage == null)
						_secondPage = new SecondPage { BindingContext = Settings };

					InsertPageBefore(_secondPage, NavigationPage.CurrentPage);
					await PopAsync().ConfigureAwait(false);
				});
			}
		}

		private SecondPage _secondPage;
	}
}

