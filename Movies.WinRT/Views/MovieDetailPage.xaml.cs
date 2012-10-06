using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Movies.WinRT.Messages;
using Movies.WinRT.Models;
using Movies.WinRT.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232

namespace Movies.WinRT.Views
{
	/// <summary>
	/// A page that displays details for a single item within a group while allowing gestures to
	/// flip through other items belonging to the same group.
	/// </summary>
	public sealed partial class MovieDetailPage : Movies.WinRT.Common.LayoutAwarePage
	{
		public MovieDetailPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Populates the page with content passed during navigation.  Any saved state is also
		/// provided when recreating a page from a prior session.
		/// </summary>
		/// <param name="navigationParameter">The parameter value passed to
		/// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
		/// </param>
		/// <param name="pageState">A dictionary of state preserved by this page during an earlier
		/// session.  This will be null the first time a page is visited.</param>
		protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
		{
			// Allow saved page state to override the initial item to display
			if (pageState != null && pageState.ContainsKey("SelectedItem"))
			{
				navigationParameter = pageState["SelectedItem"];
			}

			var viewModel = (MovieDetailViewModel)DataContext;
			var movieSelectedMessage = (MovieSelectedMessage)navigationParameter;
			viewModel.Movie = movieSelectedMessage.Movie;
			viewModel.MovieGroup = movieSelectedMessage.MovieGroup;

			// The following is a hack to make the FlipView respond to SelectedItem.
			var setSelectedItem = true;
			flipView.LayoutUpdated += (sender, args) =>
				{
					if (setSelectedItem)
					{
						setSelectedItem = false;
						flipView.SelectedItem = null;
						flipView.SelectedItem = viewModel.Movie;
					}
				};

			var dataTransferManager = DataTransferManager.GetForCurrentView();
			dataTransferManager.DataRequested += dataTransferManager_DataRequested;
		}

		/// <summary>
		/// Preserves state associated with this page in case the application is suspended or the
		/// page is discarded from the navigation cache.  Values must conform to the serialization
		/// requirements of <see cref="SuspensionManager.SessionState"/>.
		/// </summary>
		/// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
		protected override void SaveState(Dictionary<String, Object> pageState)
		{
			var selectedItem = this.flipView.SelectedItem;
			// TODO: Derive a serializable navigation parameter and assign it to pageState["SelectedItem"]

			var dataTransferManager = DataTransferManager.GetForCurrentView();
			dataTransferManager.DataRequested -= dataTransferManager_DataRequested;
		}

		private async void dataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
		{
			var viewModel = (MovieDetailViewModel)DataContext;

			var request = args.Request;

			request.Data.Properties.Title = viewModel.Movie.Name;
			request.Data.SetText(viewModel.Movie.Synopsis);

			var file = await Package.Current.InstalledLocation.GetFileAsync(viewModel.Movie.ImageUrl.Substring(1).Replace("/", @"\"));

			List<IStorageItem> imageItems = new List<IStorageItem>();
			imageItems.Add(file);
			request.Data.SetStorageItems(imageItems);

			RandomAccessStreamReference imageStreamRef = RandomAccessStreamReference.CreateFromFile(file);
			request.Data.Properties.Thumbnail = imageStreamRef;
			request.Data.SetBitmap(imageStreamRef);
		}
	}
}
