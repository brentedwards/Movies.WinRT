using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Movies.WinRT.Models;
using Movies.WinRT.Repositories;

namespace Movies.WinRT.ViewModels
{
	public class GroupedMoviesViewModel : ModelBase
	{
		private readonly IMovieRepository MovieRepository;

		public GroupedMoviesViewModel(IMessenger messenger)
		{
			MovieRepository = new MovieRepository();

			MovieGroups = MovieRepository.LoadMovies().GroupBy(m => m.Genre).Select(g => new MovieGroupViewModel(messenger) { Title = g.Key.ToString(), Movies = g.ToList() });
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
	}
}
