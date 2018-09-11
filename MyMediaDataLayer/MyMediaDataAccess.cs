using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMediaDataLayer
{
    public class MyMediaDataAccess
    {
        public List<Movie> GetAllMovies()
        {
            using (var db = new MyMediaDBEntities())
            {
                var movies = db.Movies;

                if (movies != null)
                {
                    return movies.OrderBy(m => m.Name).ToList();
                }

                return null;
            }
        }

        public List<Movie> GetFilteredMovies(string name = null, string storageLocation = null, string genre = null)
        {
            using (var db = new MyMediaDBEntities())
            {
                IQueryable<Movie> query = db.Movies;

                if(!String.IsNullOrEmpty(name))
                {
                    query = query.Where(m => m.Name.Contains(name));
                }
                if (!String.IsNullOrEmpty(storageLocation))
                {
                    query = query.Where(m => m.StorageLocation.Contains(storageLocation));
                }
                if (!String.IsNullOrEmpty(genre))
                {
                    query = query.Where(m => m.Genre.Contains(genre));
                }

                if (query != null)
                {
                    return query.OrderBy(m => m.Name).ToList();
                }

                return null;
            }
        }

        public bool InsertMovie(string name, string storageLocation, DateTime releaseDate, string genre, int runTimeinMinutes, decimal budget, decimal revenue, string homePage, string overview, string castDetails)
        {
            using (var db = new MyMediaDBEntities())
            {
                var movie = new Movie()
                {
                    Name = name,
                    StorageLocation = storageLocation,
                    ReleaseDate = releaseDate,
                    Genre = genre,
                    RunTimeMinutes = runTimeinMinutes,
                    Budget = budget,
                    Revenue = revenue,
                    HomePage = homePage,
                    Overview = overview,
                    CastDetails = castDetails
                };

                db.Entry(movie).State = EntityState.Added;

                db.Movies.Add(movie);

                int result = db.SaveChanges();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateMovie(int id, string name, string storageLocation, DateTime releaseDate, string genre, int runTimeinMinutes, decimal budget, decimal revenue, string homePage, string overview, string castDetails)
        {
            using (var db = new MyMediaDBEntities())
            {
                var movie = new Movie { Id = id };

                movie.Name = name;
                movie.StorageLocation = storageLocation;
                movie.ReleaseDate = releaseDate;
                movie.Genre = genre;
                movie.RunTimeMinutes = runTimeinMinutes;
                movie.Budget = budget;
                movie.Revenue = revenue;
                movie.HomePage = homePage;
                movie.Overview = overview;
                movie.CastDetails = castDetails;

                db.Entry(movie).State = EntityState.Modified;

                int result = db.SaveChanges();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteMovie(int id)
        {
            using (var db = new MyMediaDBEntities())
            {
                var movie = new Movie { Id = id };

                db.Entry(movie).State = EntityState.Deleted;

                int result = db.SaveChanges();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
