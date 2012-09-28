using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.WinRT.Models
{
	public class Movie : ModelBase
	{
		public Movie()
		{
			Id = -1;
		}


		public Movie(int id, String name, Genres genre, Ratings rating, string imageUrl, string synopsis)
		{
			Id = id;
			Name = name;
			Genre = genre;
			Rating = rating;
			ImageUrl = imageUrl;
			Synopsis = synopsis;
		}


		public int Id { get; set; }


		private String name;
		public String Name
		{
			get { return name; }
			set
			{
				name = value;
				NotifyPropertyChanged("Name");
			}
		}


		private Genres genre;
		public Genres Genre
		{
			get { return genre; }
			set
			{
				genre = value;
				NotifyPropertyChanged("Genre");
			}
		}


		private Ratings rating;
		public Ratings Rating
		{
			get { return rating; }
			set
			{
				rating = value;
				NotifyPropertyChanged("Rating");
			}
		}

		private string imageUrl;
		public string ImageUrl
		{
			get { return imageUrl; }
			set
			{
				imageUrl = value;
				NotifyPropertyChanged("ImageUrl");
			}
		}

		private string synopsis;
		public string Synopsis
		{
			get { return synopsis; }
			set
			{
				synopsis = value;
				NotifyPropertyChanged("Synopsis");
			}
		}
	}
}
