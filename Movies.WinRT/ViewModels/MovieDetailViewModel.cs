using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.WinRT.Models;

namespace Movies.WinRT.ViewModels
{
	public class MovieDetailViewModel : ModelBase
	{
		// TODO: Use the flip view to allow flipping between all the movies that came from where we navigated from.
		private Movie movie;
		public Movie Movie
		{
			get { return movie; }
			set
			{
				movie = value;
				NotifyPropertyChanged("Movie");
			}
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
	}
}
