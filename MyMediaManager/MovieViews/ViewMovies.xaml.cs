using MyMediaDataLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyMediaManager.MovieViews
{
    /// <summary>
    /// Interaction logic for ViewMovies.xaml
    /// </summary>
    public partial class ViewMovies : Window
    {
        public Visibility DirectoryOpenVisibility
        {
            get
            {
                var selectedMovie = (this.movieDataGrid.SelectedItem as MyMediaDataLayer.Movie);

                if (selectedMovie != null)
                {
                    return IsFileDirectory(selectedMovie.StorageLocation) ? Visibility.Visible : Visibility.Collapsed;
                }

                return Visibility.Collapsed;
            }
        }

        public bool IsFileDirectory(string text)
        {

            // Use regular Expression to discern if 'text' is a folder name
            
            //var regex = new Regex(@"^(?:[a - zA - Z]\:|\\\\[\w\.]+\\[\w.$]+)\\(?:[\w]+\\)*\w([\w.])+$");

            //var result = regex.IsMatch(text);

            return text != "Media Room DVD Rack"; /* Temporary for testing purposes */
        }

        public ViewMovies()
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void FilterList()
        {
            var db = new MyMediaDataAccess();

            var name = nameFilterTexBox.Text;
            var storageLocation = storageLocationFilterTexBox.Text;
            var genre = genreFilterTexBox.Text;

            System.Windows.Data.CollectionViewSource movieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("movieViewSource")));

            movieViewSource.Source = db.GetFilteredMovies(name, storageLocation, genre);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource movieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("movieViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // movieViewSource.Source = [generic data source]

            movieViewSource.Source = new MyMediaDataLayer.MyMediaDataAccess().GetAllMovies();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selectedMovie = (movieDataGrid.SelectedItem as Movie);
            var manageMovie = new ManageMovie(selectedMovie);

            manageMovie.Closed += Child_Closed;
            //manageMovie.DataContext = selectedMovie;
            //manageMovie.DataContext = movieDataGrid.Items.CurrentItem;
            manageMovie.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var db = new MyMediaDataAccess();

                var selectedMovie = (movieDataGrid.SelectedItem as Movie);

                db.DeleteMovie(selectedMovie.Id);

                RefreshMovieListGridView();
            }
            else
            {
                // Do nothing at the moment
            }
        }

        private void RefreshMovieListGridView()
        {
            System.Windows.Data.CollectionViewSource movieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("movieViewSource")));

            var db = new MyMediaDataAccess();

            movieViewSource.Source = db.GetAllMovies();
        }

        private void Child_Closed(object sender, EventArgs e)
        {
            RefreshMovieListGridView();
        }

        private void OpenDirectory_Click(object sender, RoutedEventArgs e)
        {
            var fileDir = (this.movieDataGrid.SelectedItem as MyMediaDataLayer.Movie).StorageLocation;

            if(Directory.Exists(fileDir))
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = fileDir;
                process.Start();
            }
            else
            {
                System.Windows.MessageBox.Show("Folder does not exist on current system", "Folder not found", MessageBoxButton.OK);
            }
        }

        private void ContextMenuRightClick(object sender, MouseButtonEventArgs e)
        {
            OpenDirectoryMenuItem.Visibility = DirectoryOpenVisibility;
        }

        private void HomePageHyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterList();
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            var manageMovie = new ManageMovie();

            manageMovie.Closed += Child_Closed;

            manageMovie.ShowDialog();
        }
    }
}
