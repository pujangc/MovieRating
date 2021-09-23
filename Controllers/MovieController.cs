using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesRatingApp1.Models;

namespace MoviesRatingApp1.Controllers
{
    [Route("api/Movie")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MoviesRatingApp1Context _context;

        public MovieController(MoviesRatingApp1Context context)
        {
            _context = context;
        }

        // GET: api/Movie
        [Route("Movie/Movie")]
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await _context.Movie.ToListAsync();
        }

        [Route("MovieByCriteria/{title?}/{year?}/{genre?}")]
       // [HttpGet("{Title}/{Year}/{Genre}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovieByCriteria(String title, int year, String genre)
        {
            var movie = _context.Movie;
            if(title == "none" && year == 0 && genre == "none")
            {
                return NotFound("Please provide a search parameter!");
            }
            if((! String.IsNullOrEmpty(title)) && !String.Equals(title,"none"))
                await movie.Where(x => x.Title == title).ToListAsync();
            if(year!=0)
                await movie.Where(x => x.YearOfRelease == year).ToListAsync();
            if ((!String.IsNullOrEmpty(genre)) && !String.Equals(genre, "none"))
                await movie.Where(x => x.Genre == genre).ToListAsync();
            return movie;
        }

        // GET: api/TotalUserAverageRating
        [Route("TotalUserAverageRating")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetTotalUserAverageRating()
        {
            var movie = _context.Movie.OrderByDescending(x=>x.AverageRating).ThenBy(x=>x.Title).Take(5);
            return await movie.ToListAsync();
        }

        // GET: api/TotalUserAverageRating
     
        [Route("HighestRatingBySpecificUser/{id}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetHighestRatingBySpecificUser(int id)
        {
            var movie = _context.MovieRating.Where(x=>x.UserId == id)
            .OrderByDescending(x=>x.Rating).Take(5);
            var filteredMovies = _context.Movie;
            movie.ToList().ForEach(x=>
            {
                filteredMovies.Add(_context.Movie.Where(y=>y.MovieId == x.MovieId).FirstOrDefault());
            });
            return await filteredMovies.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.MovieId }, movie);
        }

        
        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }
    }
}
