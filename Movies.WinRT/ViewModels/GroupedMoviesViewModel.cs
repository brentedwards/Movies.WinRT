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
	public class GroupedMoviesViewModel : ModelBase
	{
		private readonly IMovieRepository MovieRepository;

		public GroupedMoviesViewModel(
			IMessenger messenger,
			IMovieRepository movieRepository)
		{
			MovieRepository = movieRepository;

			if (MovieRepository != null)
			{
				MovieGroups = MovieRepository.LoadMovies()
					.GroupBy(m => m.Genre)
					.Select(g => new MovieGroupViewModel(messenger)
					{
						Title = g.Key.ToString(),
						Movies = g.ToList(),
						GroupImageUrl = g.First().ImageUrl
					});
			}

			MovieTappedCommand = new RelayCommand<Movie>((movie) =>
				{
					var movieGroup = MovieGroups.First(mg => mg.Movies.Contains(movie));
					messenger.Send<MovieSelectedMessage>(new MovieSelectedMessage { Movie = movie, MovieGroup = movieGroup });
				});

			MovieSelectedCommand = new RelayCommand(() =>
				{
					var movieGroup = MovieGroups.First(mg => mg.Movies.Contains(SelectedMovie));
					messenger.Send<MovieSelectedMessage>(new MovieSelectedMessage { Movie = SelectedMovie, MovieGroup = movieGroup });
				});
		}

		private IEnumerable<MovieGroupViewModel> movieGroups;
		public IEnumerable<MovieGroupViewModel> MovieGroups
		{
			get { return movieGroups; }
			set
			{
				movieGroups = value;
				NotifyPropertyChanged("Movies");
			}
		}

		private Movie selectedMovie;
		public Movie SelectedMovie
		{
			get { return selectedMovie; }
			set
			{
				selectedMovie = value;
				NotifyPropertyChanged("SelectedMovie");
			}
		}

		public RelayCommand<Movie> MovieTappedCommand { get; private set; }
		public RelayCommand MovieSelectedCommand { get; private set; }
	}
}
