using Microsoft.EntityFrameworkCore;
using MoviesRatingApp1.Models;
namespace MoviesRatingApp1.Models
{
    public class MoviesRatingApp1Context : DbContext
    {
        public MoviesRatingApp1Context(DbContextOptions<MoviesRatingApp1Context> options)
        : base(options)
        {            
        }
        public DbSet<MovieRating> MovieRating{get;set;}
        public DbSet<MoviesRatingApp1.Models.Movie> Movie { get; set; }
        public DbSet<MoviesRatingApp1.Models.User> User { get; set; }
    }
}