using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Movies.WinRT.Messages;
using Movies.WinRT.Models;

namespace Movies.WinRT.ViewModels
{
	public class MovieGroupViewModel
	{
		public MovieGroupViewModel(IMessenger messenger)
		{
			SelectCommand = new RelayCommand(() =>
				{
					messenger.Send<GroupSelectedMessage>(new GroupSelectedMessage { GroupName = Title });
				});
		}

		public RelayCommand SelectCommand { get; private set; }

		public string Title { get; set; }
		public IEnumerable<Movie> Movies { get; set; }

		public string GroupImageUrl { get; set; }
	}
}
