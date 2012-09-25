using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.WinRT.Models;
using Windows.Foundation;

namespace Movies.WinRT.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public IEnumerable<Movie> LoadMovies()
        {
			var movies = new List<Movie>();

			var id = 1;
			movies.Add(new Movie(id++, "The Dark Knight Rises", Genres.Action, Ratings.PG13, "/Posters/The-Dark-Knight-Rises.jpg"));
			movies.Add(new Movie(id++, "The Hangover Part 2", Genres.Comedy, Ratings.R, "/Posters/The-Hangover-Part-2.jpg"));
			movies.Add(new Movie(id++, "Prometheus", Genres.SciFi, Ratings.R, "/Posters/Prometheus.jpg"));
			movies.Add(new Movie(id++, "The Expendables 2", Genres.Action, Ratings.R, "/Posters/The-Expendables-2.jpg"));
			movies.Add(new Movie(id++, "Finding Nemo 3D", Genres.Adventure, Ratings.G, "/Posters/Finding-Nemo-3D.jpg"));
			movies.Add(new Movie(id++, "Act Of Valor", Genres.Action, Ratings.R, "/Posters/Act-Of-Valor.jpg"));
			movies.Add(new Movie(id++, "The Hunger Games", Genres.Adventure, Ratings.PG13, "/Posters/The-Hunger-Games.jpg"));
			movies.Add(new Movie(id++, "The Avengers", Genres.Action, Ratings.PG13, "/Posters/The-Avengers.jpg"));
			movies.Add(new Movie(id++, "Men In Black 3", Genres.Comedy, Ratings.PG13, "/Posters/Men-In-Black-3.jpg"));
			movies.Add(new Movie(id++, "Ted", Genres.Comedy, Ratings.R, "/Posters/Ted.jpg"));
			movies.Add(new Movie(id++, "The Campaign", Genres.Comedy, Ratings.R, "/Posters/The-Campaign.jpg"));
			movies.Add(new Movie(id++, "Looper", Genres.SciFi, Ratings.R, "/Posters/Looper.jpg"));

			return movies;
        }
    }
}
