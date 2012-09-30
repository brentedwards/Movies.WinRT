using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Movies.WinRT.Helpers
{
	/// <summary>
	/// The original idea for this class came from http://geoffwebbercross.blogspot.com/2012/05/windows-8-metro-gridview-itemclick.html
	/// </summary>
	public class GridViewItemClick
	{
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GridViewItemClick), new PropertyMetadata(null, CommandPropertyChanged));

		public static void SetCommand(DependencyObject attached, ICommand command)
		{
			attached.SetValue(CommandProperty, command);
		}

		public static ICommand GetCommand(DependencyObject attached)
		{
			return (ICommand)attached.GetValue(CommandProperty);
		}

		private static void CommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((GridView)d).ItemClick += ItemClick_ItemClick;
		}

		private static void ItemClick_ItemClick(object sender, ItemClickEventArgs e)
		{
			var gridView = (GridView)sender;
			var command = GetCommand(gridView);
			command.Execute(e.ClickedItem);
		}
	}
}
