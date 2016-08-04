﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		}
	}
}
