using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.WinRT.Models;
using Movies.WinRT.ViewModels;

namespace Movies.WinRT.Messages
{
	public class MovieSelectedMessage
	{
		public MovieGroupViewModel MovieGroup { get; set; }
		public Movie Movie { get; set; }
	}
}
