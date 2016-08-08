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
					await PushAsync(new SecondPage());
				});
			}
		}
	}
}
