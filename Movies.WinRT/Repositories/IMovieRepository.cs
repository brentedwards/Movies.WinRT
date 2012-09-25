using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.WinRT.Models;
using Windows.Foundation;

namespace Movies.WinRT.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> LoadMovies();
    }
}
