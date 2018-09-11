using MyMediaDataLayer;
using MyMediaManager.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyMediaManager.MovieViews
{
    /// <summary>
    /// Interaction logic for ManageMovie.xaml
    /// </summary>
    public partial class ManageMovie : Window
    {
        public Movie _movie;
        public bool _isEdit = false;

        public ManageMovie()
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public ManageMovie(Movie movie) : this()
        {
            _movie = movie;
            _isEdit = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource movieViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("movieViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            //movieViewSource.Source = [generic data source]
            if(_isEdit)
            {
                nameTextBox.Text = _movie.Name;
                storageLocationTextBox.Text = _movie.StorageLocation;
                releaseDateDatePicker.SelectedDate = _movie.ReleaseDate;
                genreTextBox.Text = _movie.Genre;
                runTimeMinutesTextBox.Text = _movie.RunTimeMinutes.ToString();
                budgetTextBox.Text = _movie.Budget.ToString();
                revenueTextBox.Text = _movie.Revenue.ToString();
                homePageTextBox.Text = _movie.HomePage;
                overviewTextBox.Text = _movie.Overview;
                castDetailsTextBox.Text = _movie.CastDetails;
            }
            else
            {
                //do nothing at the moment, blank form
            }
        }

        private void DecimalValidationHandler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !DecimalHandler.IsTextDecimal(e.Text);
        }

        private void IntValidationHandler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IntHandler.IsTextInt(e.Text);
        }

        private void SubmitButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (!PassValidation())
            {
                return;
            }

            var db = new MyMediaDataAccess();

            bool success = false;

            if (_isEdit)
            {
                success = db.UpdateMovie(_movie.Id, nameTextBox.Text, storageLocationTextBox.Text, releaseDateDatePicker.SelectedDate.Value, genreTextBox.Text, Int32.Parse(runTimeMinutesTextBox.Text), Decimal.Parse(budgetTextBox.Text), Decimal.Parse(revenueTextBox.Text), homePageTextBox.Text, overviewTextBox.Text, castDetailsTextBox.Text);
            }
            else
            {
                success= db.InsertMovie(nameTextBox.Text, storageLocationTextBox.Text, releaseDateDatePicker.SelectedDate.Value, genreTextBox.Text, Int32.Parse(runTimeMinutesTextBox.Text), Decimal.Parse(budgetTextBox.Text), Decimal.Parse(revenueTextBox.Text), homePageTextBox.Text, overviewTextBox.Text, castDetailsTextBox.Text);
            }

            if (success)
                this.Close();
            else
            {
                //do something here
            }

            //_movie.Id, nameTextBox.Text, storageLocationTextBox.Text, releaseDateDatePicker.SelectedDate, genreTextBox.Text, runTimeMinutesTextBox.Text, budgetTextBox.Text, revenueTextBox.Text, homePageTextBox.Text, overviewTextBox.Text, castDetailsTextBox.Text
        }

        private void CancelButton_Clicked(object sender, RoutedEventArgs e)
        {
            // Does nothing specific yet besides close window
            this.Close();
        }

        //basic validation
        //add more as needed
        private bool PassValidation()
        {
            if (String.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Name cannot be empty/blank");
                return false;
            }
            if (String.IsNullOrEmpty(storageLocationTextBox.Text))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Storage Location cannot be empty/blank");
                return false;
            }

            return true;
        }
    }
}
