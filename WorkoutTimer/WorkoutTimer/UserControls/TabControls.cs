using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTimer.Models;
using WorkoutTimer.ViewModels;
using Xamarin.Forms;

namespace WorkoutTimer.UserControls
{
	public class TabButtonsEx : StackLayout { }

	public class TabButtonEx : Button
	{

		public TabButtonEx()
		{
			Clicked += ThisTabButtonClicked;
		}

		private void ThisTabButtonClicked(object s, EventArgs e)
		{
			var tabsEx = ValidParentBetterTabs();
			if (tabsEx == null)
				return;

			tabsEx.SelectedTabButtonEx = this;
		}

		private TabsEx ValidParentBetterTabs()
		{
			if (Parent?.Parent != null && Parent.Parent.GetType() == typeof(TabsEx))
				return ((TabsEx)Parent.Parent);

			return null;
		}
	}

	public class TabEx : StackLayout { }

	public class TabsEx : StackLayout
	{
		public Color SelectedColor { private get; set; } = Color.White;

		public Color UnselectedColor { private get; set; } = Color.Silver;

		private List<TabButtonEx> TabButtons
		{
			get
			{
				TabButtonsEx tabButtonsEx =
				   (TabButtonsEx)Children.
				   First(c => c.GetType() ==
					  typeof(TabButtonsEx));
				var buttonEnumerable =
				   tabButtonsEx.Children.Select(c =>
					  (TabButtonEx)c);
				var buttonList =
				   buttonEnumerable.Where(c => c.GetType() ==
					  typeof(TabButtonEx)).ToList();
				return buttonList;
			}
		}

		private List<TabEx> Tabs
		{
			get
			{
				var childList =
				   Children.Where(c => c.GetType() ==
					  typeof(TabEx));
				var tabList =
				   childList.Select(c => (TabEx)c).ToList();
				return tabList;
			}
		}

		private int _selectedTabIndex;
		public int SelectedTabIndex
		{
			get { return _selectedTabIndex; }
			set
			{
				_selectedTabIndex = value;

				if (Tabs.Count > 0)
					SelectionUiUpdate();
			}
		}

		public TabButtonEx SelectedTabButtonEx
		{
			get { return TabButtons[_selectedTabIndex]; }
			set
			{
				var tabIndex = TabButtons.FindIndex(t => t == value);

				if (tabIndex != _selectedTabIndex)
					SelectedTabIndex = tabIndex;
			}
		}

		private TabEx SelectedTabEx => Tabs[_selectedTabIndex];

		protected override void OnParentSet()
		{
			base.OnParentSet();
			SelectionUiUpdate();
		}

		private void SelectionUiUpdate()
		{
			foreach (var btn in TabButtons)
				btn.BackgroundColor = UnselectedColor;

			SelectedTabButtonEx.BackgroundColor = SelectedColor;

			foreach (var tb in Tabs)
				tb.IsVisible = false;

			SelectedTabEx.IsVisible = true;

			if(BindingContext != null)
				(BindingContext as MainViewModel).Settings.SelectedDay = (Day)SelectedTabIndex;
		}
	}
}
