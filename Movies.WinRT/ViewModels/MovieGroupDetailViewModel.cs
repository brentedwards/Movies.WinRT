using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Movies.WinRT.Messages;
using Movies.WinRT.Models;
using Movies.WinRT.Repositories;

namespace Movies.WinRT.ViewModels
{
	public class MovieGroupDetailViewModel : ModelBase
	{
		private readonly IMovieRepository MovieRepository;
		private readonly IMessenger Messenger;

		public MovieGroupDetailViewModel(
			IMovieRepository movieRepository,
			IMessenger messenger)
		{
			MovieRepository = movieRepository;
			Messenger = messenger;

			MovieSelectedCommand = new RelayCommand<Movie>((movie) =>
			{
				messenger.Send<MovieSelectedMessage>(new MovieSelectedMessage { Movie = movie, MovieGroup = MovieGroup });
			});
		}

		public void LoadMovieGroup(string groupName)
		{
			var movies = MovieRepository.LoadMoviesByGroup(groupName);
			MovieGroup = new MovieGroupViewModel(Messenger)
			{
				Title = groupName,
				Movies = movies,
				GroupImageUrl = movies.First().ImageUrl
			};
		}

		private MovieGroupViewModel movieGroup;
		public MovieGroupViewModel MovieGroup
		{
			get { return movieGroup; }
			set
			{
				movieGroup = value;
				NotifyPropertyChanged("MovieGroup");
			}
		}

		public RelayCommand<Movie> MovieSelectedCommand { get; private set; }
	}
}
