/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Movies.WinRT"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Movies.WinRT.Repositories;
using Movies.WinRT.ViewModels.Design;

namespace Movies.WinRT.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if (ViewModelBase.IsInDesignModeStatic)
			{
				// Create design time view services and models
				Register<GroupedMoviesViewModel, DesignGroupedMoviesViewModel>();
				Register<MovieGroupDetailViewModel, DesignMovieGroupDetailViewModel>();
			}
			else
			{
				// Create run time view services and models
				Register<GroupedMoviesViewModel>();
				Register<MovieGroupDetailViewModel>();
				Register<IMovieRepository, MovieRepository>();

				Register<IMessenger>(new Messenger());
			}
        }

		public GroupedMoviesViewModel GroupedMovies
		{
			get { return ServiceLocator.Current.GetInstance<GroupedMoviesViewModel>(); }
		}

		public MovieGroupDetailViewModel MovieGroupDetail
		{
			get { return ServiceLocator.Current.GetInstance<MovieGroupDetailViewModel>(); }
		}
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

		/// <summary>
		/// Registers a class for the given base type with the IoC container
		/// </summary>
		/// <typeparam name="TBase">The base class, or interface, type</typeparam>
		/// <typeparam name="TImpl">A concrete type that implements of the base class or interface</typeparam>
		private void Register<TBase, TImpl>()
			where TBase : class
			where TImpl : class
		{
			if (!SimpleIoc.Default.IsRegistered<TBase>())
				SimpleIoc.Default.Register<TBase, TImpl>();
		}

		/// <summary>
		/// Registers a class with the IoC container
		/// </summary>
		/// <typeparam name="TImpl">A concrete type</typeparam>
		private void Register<TImpl>()
			where TImpl : class
		{
			if (!SimpleIoc.Default.IsRegistered<TImpl>())
				SimpleIoc.Default.Register<TImpl>();
		}

		/// <summary>
		/// Registers an instance of a class with the IoC container
		/// </summary>
		/// <typeparam name="TImpl">A concrete type</typeparam>
		/// <param name="instance">The instance</param>
		private void Register<TImpl>(TImpl instance)
			where TImpl : class
		{
			if (!SimpleIoc.Default.IsRegistered<TImpl>())
			{
				SimpleIoc.Default.Register<TImpl>(new System.Func<TImpl>(() => { return instance; }));
			}
		}
    }
}