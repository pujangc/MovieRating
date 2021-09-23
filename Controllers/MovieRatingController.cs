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
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRatingController : ControllerBase
    {
        private readonly MoviesRatingApp1Context _context;

        public MovieRatingController(MoviesRatingApp1Context context)
        {
            _context = context;
        }

        // GET: api/MovieRating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieRating>>> GetMovieRatingDetails()
        {
            return await _context.MovieRating.ToListAsync();
        }

        // GET: api/MovieRating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieRating>> GetMovieRating(int id)
        {
            var movieRating = await _context.MovieRating.FindAsync(id);

            if (movieRating == null)
            {
                return NotFound();
            }

            return movieRating;
        }

               
        [HttpPost]
        public async Task<ActionResult<MovieRating>> PostMovieRating(MovieRating movieRating)
        {
            _context.MovieRating.Add(movieRating);
            await _context.SaveChangesAsync();

            var _movie = _context.Movie.Where(x=>x.MovieId == movieRating.MovieId).FirstOrDefault();
            _movie.AverageRating=_context.MovieRating.Where(x=>x.MovieId == movieRating.MovieId).Select(x=>x.Rating).Average();
            _context.Movie.Update(_movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovieRating", new { id = movieRating.MovieRatingId }, movieRating);
        }

      
        private bool MovieRatingExists(int id)
        {
            return _context.MovieRating.Any(e => e.MovieRatingId == id);
        }
    }
}
